using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    int startingBulletCount;

    int count;

    const string k_BULLET_NAME = "Bullet_{0:d4}";

    // (Optional) Prevent non-singleton constructor use.
    protected BulletManager() { }

    private void Awake()
    {
        for(int i = 0; i < startingBulletCount; ++i)
        {
            CreateBullet();
        }
    }

    void CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform, false);
        newBullet.transform.localPosition = Vector3.zero;
        newBullet.transform.rotation = Quaternion.identity;
        newBullet.transform.localScale = Vector3.one;

        newBullet.name = string.Format(k_BULLET_NAME, count);

        newBullet.SetActive(false);

        newBullet.transform.SetAsFirstSibling();

        ++count;
    }

    public Bullet GetBullet()
    {
        if(transform.childCount <= 0 || transform.GetChild(0).gameObject.activeSelf)
        {
            CreateBullet();
        }

        Transform selectedBullet = transform.GetChild(0);
        selectedBullet.SetAsLastSibling();

        return selectedBullet.GetComponent<Bullet>();
    }

    public void ReturnBullet(Transform bulletTransform)
    {
        bulletTransform.gameObject.SetActive(false);

        transform.SetAsLastSibling();
    }
}