using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LookAt))]
public class Player : RectGameObject, ShootieMcShootie.IPlayerActions
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

    [SerializeField]
    ContactFilter2D contactFilter;
    List<Collider2D> hitList = new List<Collider2D>();

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        lookAtScript = GetComponent<LookAt>();

        InputManager.Instance.SetCallbacks(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

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

        #region Check if hit Enemies
        collider.OverlapCollider(contactFilter, hitList);

        if(hitList.Count > 0)
        {
            GameManager.Instance.ResetScore();

            foreach (Collider2D hitCollider in hitList)
            {
                hitCollider.GetComponent<Enemy>().OnHit((int)Layers.Player);
            }
        }
        #endregion
    }

    #region Movment Logic
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDelta = context.ReadValue<Vector2>();

        moveDelta *= moveScalar;

        rect.anchoredPosition += moveDelta;
    }

    void MovePlayer(Vector2 delta)
    {
        //  Check if player would move outside the bounds
        Vector2 newPos = ClampMoveToBounds(rect.anchoredPosition + delta);

        rect.anchoredPosition = newPos;
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

    public void OnLook(InputAction.CallbackContext context)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MenuManager.Instance.MainGameRect, context.ReadValue<Vector2>(), Camera.main, out lookAtVect);

        lookAtScript.TargetPosition = lookAtVect;
    }

    #region Fire Logic
    public void OnFire(InputAction.CallbackContext context)
    {
        isFiring = context.control.IsPressed();

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

    public override void OnHit(int otherLayer)
    {
        GameManager.Instance.ResetScore();
    }
}