using UnityEngine;

[RequireComponent (typeof(LookAt))]
public class Enemy : RectGameObject
{
    LookAt lookAtScript;

    MoveTowards moveTowards;

    [SerializeField]
    float speed;

    #region Firing vars
    bool isFiring = false;

    [SerializeField]
    float fireCoolDown;
    float fireTimer;

    Bullet NewBullet;
    #endregion

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        lookAtScript = GetComponent<LookAt>();

        moveTowards = GetComponent<MoveTowards>();
    }

    public void Spawn(int side)
    {
        switch (side)
        {
            //          Top
            case 0:
                position.y = -(MenuManager.Instance.MainGameRect.rect.height / 2f) - (rect.rect.height / 2f);

                position.x = Random.Range(-MenuManager.Instance.MainGameRect.rect.width / 2f, MenuManager.Instance.MainGameRect.rect.width / 2f);
                break;
            //          Bottom
            case 1:
                position.y = (MenuManager.Instance.MainGameRect.rect.height / 2f) + (rect.rect.height / 2f);

                position.x = Random.Range(-MenuManager.Instance.MainGameRect.rect.width / 2f, MenuManager.Instance.MainGameRect.rect.width / 2f);
                break;
            //          Left
            case 2:
                position.x = -(MenuManager.Instance.MainGameRect.rect.width / 2f) - (rect.rect.width / 2f);

                position.y = Random.Range(-MenuManager.Instance.MainGameRect.rect.height / 2f, MenuManager.Instance.MainGameRect.rect.height / 2f);
                break;
            //          Right
            default:
                position.x = (MenuManager.Instance.MainGameRect.rect.width / 2f) + (rect.rect.width / 2f);

                position.y = Random.Range(-MenuManager.Instance.MainGameRect.rect.height / 2f, MenuManager.Instance.MainGameRect.rect.height / 2f);
                break;
        }

        rect.anchoredPosition3D = new Vector3(position.x, position.y, 0);

        fireTimer = fireCoolDown;

        if (moveTowards != null)
        {
            moveTowards.SetTarget(-rect.anchoredPosition.normalized, speed);
        }

        gameObject.SetActive(true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        lookAtScript.TargetPosition = GameManager.Instance.GetPlayerLocation();

        #region Fire
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
        #endregion
    }

    void Fire()
    {
        NewBullet = BulletManager.Instance.GetBullet();

        NewBullet.gameObject.layer = (int)BulletType.EnemyBullet;

        NewBullet.transform.SetParent(MenuManager.Instance.MainGameRect);

        NewBullet.Fire(position, rect.rotation);
    }

    public override void OnHit(int otherLayer)
    {
        if((Layers)otherLayer != Layers.Player)
        {
            GameManager.Instance.AddPoints(1);
        }

        CleanUp();
    }

    //public override void CleanUp()
    public override void CleanUp()
    {
        base.CleanUp();
    
        if (!BulletManager.IsShuttingDown())
        {
            EnemyManager.Instance.ReturnEnemy(this);
        }
    }
}
