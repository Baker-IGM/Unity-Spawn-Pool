using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LookAt))]
public class Player : RectGameObject
{
    #region Look at vars
    LookAt lookAtScript;

    Vector2 lookAtVect;
    #endregion

    #region Firing vars
    bool isFiring = false;

    [SerializeField]
    float fireCoolDown;
    float fireTimer;
    
    Bullet NewBullet;
    #endregion

    #region Movement vars
    [SerializeField]
    float moveScalar = 1;

    Vector2 moveDelta;
    #endregion

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        lookAtScript = GetComponent<LookAt>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Firing
        //  Check if fire button is held down
        if(isFiring)
        {
            //  Decrease auto fire timer
            if (fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }

            //  Check if can refire
            if (fireTimer < 0)
            {
                Fire();

                fireTimer = fireCoolDown;
            }
        }
        
        #endregion

        //
        //      Player movement
        //
        MovePlayer(moveDelta);
    }

    #region Movment Logic
    public void OnMove(InputValue value)
    {
        moveDelta = value.Get<Vector2>();

        moveDelta *= moveScalar;

        rect.anchoredPosition += moveDelta;
    }

    void MovePlayer(Vector2 delta)
    {
        //  Check if player would move outside the bounds
        Vector2 newPos = ClampMoveToBounds(rect.anchoredPosition + delta);

        rect.anchoredPosition = newPos;

        position = rect.anchoredPosition;
    }

    Vector2 ClampMoveToBounds(Vector2 pos)
    {
        //  Check Right
        if(pos.x + rect.rect.width / 2f > MenuManager.Instance.MainGameRect.rect.width / 2f)
        {
            pos.x = (MenuManager.Instance.MainGameRect.rect.width / 2f) - (rect.rect.width / 2f);
        }

        //  Check Left
        if (pos.x - rect.rect.width / 2f < MenuManager.Instance.MainGameRect.rect.width / -2f)
        {
            pos.x = (MenuManager.Instance.MainGameRect.rect.width / -2f) + (rect.rect.width / 2f);
        }

        //  Check Top
        if (pos.y + rect.rect.height / 2f > MenuManager.Instance.MainGameRect.rect.height / 2f)
        {
            pos.y = (MenuManager.Instance.MainGameRect.rect.height / 2f) - (rect.rect.height / 2f);
        }

        //  Check Bottom
        if (pos.y - rect.rect.height / 2f < MenuManager.Instance.MainGameRect.rect.height / -2f)
        {
            pos.y = (MenuManager.Instance.MainGameRect.rect.height / -2f) + (rect.rect.height / 2f);
        }

        return pos;
    }
    #endregion

    public void OnLook(InputValue value)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MenuManager.Instance.MainGameRect, value.Get<Vector2>(), Camera.main, out lookAtVect);

        lookAtScript.TargetPosition = lookAtVect;
    }

    #region Fire Logic
    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;

        if (isFiring)
        {
            Fire();

            fireTimer = fireCoolDown;
        }
        else
        {
            fireTimer = 0;
        }
    }

    void Fire()
    {
        NewBullet = BulletManager.Instance.GetBullet();

        NewBullet.gameObject.layer = (int)BulletType.PlayerBullet;

        NewBullet.transform.SetParent(MenuManager.Instance.MainGameRect);

        NewBullet.Fire(position, rect.rotation);
    }
    #endregion
}