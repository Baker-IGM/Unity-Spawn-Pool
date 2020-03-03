using UnityEngine;

[RequireComponent(typeof(LookAt))]
public class Enemy : RectGameObject
{
    LookAt lookAtScript;

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
    }

    private void OnEnable()
    {
        fireTimer = fireCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
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
}
