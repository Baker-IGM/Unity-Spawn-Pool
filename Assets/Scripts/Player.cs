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
    const string k_FIRE_AXIS = "Fire1";

    [SerializeField]
    float fireCoolDown;
    float fireTimer;
    
    Transform NewBullet;
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
        //      if (fireTimer > 0)
        //      {
        //          fireTimer -= Time.deltaTime;
        //      }

        //if(fireTimer <= 0 && Input.GetAxis(k_FIRE_AXIS) > 0)
        //      {
        //          BulletManager.Instance.GetBullet().Fire(transform.position, transform.rotation);

        //          fireTimer = fireCoolDown;
        //      }
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
        rect.anchoredPosition += delta;

        position = rect.anchoredPosition;
    }
    #endregion

    public void OnLook(InputValue value)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MenuManager.Instance.MainGameRect, value.Get<Vector2>(), Camera.main, out lookAtVect);

        lookAtScript.TargetPosition = lookAtVect;
    }

    public void OnFire()
    {
        Bullet bullet = BulletManager.Instance.GetBullet();

        bullet.transform.SetParent(MenuManager.Instance.MainGameRect);

        bullet.Fire(position, rect.rotation);
    }
}