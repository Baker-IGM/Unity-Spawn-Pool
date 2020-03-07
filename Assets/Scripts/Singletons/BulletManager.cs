using UnityEngine;

public enum BulletType
{
    PlayerBullet = 10,
    EnemyBullet = 11
}

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField]
    BulletManagerScriptableObejct data;

    int createdCount;

    // (Optional) Prevent non-singleton constructor use.
    protected BulletManager() { }

    private void Awake()
    {
        for(int i = 0; i < data.startingBulletCount; ++i)
        {
            CreateBullet();
        }
    }

    void CreateBullet()
    {
        GameObject newBullet = Instantiate(data.BulletPrefab, transform, false);

        newBullet.transform.rotation = Quaternion.identity;
        newBullet.transform.localScale = Vector3.one;

        newBullet.name = string.Format(data.bulletName, createdCount);

        newBullet.SetActive(false);

        newBullet.transform.SetAsFirstSibling();

        ++createdCount;
        ++StatsManager.Instance.CreatedObjects;
    }

    public Bullet GetBullet()
    {
        if(transform.childCount <= 0 || transform.GetChild(0).gameObject.activeSelf)
        {
            CreateBullet();
        }

        Transform selectedBullet = transform.GetChild(0);
        selectedBullet.SetAsLastSibling();

        ++StatsManager.Instance.SpawnedObjects;

        return selectedBullet.GetComponent<Bullet>();
    }

    public void ReturnBullet(Bullet bullet)
    {
#if DESTROY
        Destroy(bullet.gameObject);
#else
        //  Set to default layer
        bullet.gameObject.layer = 0;

        bullet.SetColor(Color.white);

        bullet.transform.SetParent(transform, false);

        bullet.gameObject.SetActive(false);

        bullet.transform.SetAsLastSibling();
#endif
    }

    public Color GetColor(BulletType type)
    {
        switch(type)
        {
            case BulletType.EnemyBullet:
                return data.enemyBulletColor;
            case BulletType.PlayerBullet:
                return data.playerBulletColor;
            default:
                return Color.white;
        }
    }
}