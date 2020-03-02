﻿using UnityEngine;

public enum BulletType
{
    PlayerBullet = 10,
    EnemyBullet = 11
}
public class BulletManager : Singleton<BulletManager>
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    int startingBulletCount;
    int count;

    [SerializeField]
    Color PlayerBulletColor;
    [SerializeField]
    Color EnemyBulletColor;

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
        //newBullet.transform.localPosition = Vector3.zero;
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

    public void ReturnBullet(Bullet bullet)
    {
        //  Set to default layer
        bullet.gameObject.layer = 0;

        bullet.SetColor(Color.white);

        bullet.transform.SetParent(transform, false);

        bullet.gameObject.SetActive(false);

        bullet.transform.SetAsLastSibling();
    }

    public Color GetColor(BulletType type)
    {
        switch(type)
        {
            case BulletType.EnemyBullet:
                return EnemyBulletColor;
            case BulletType.PlayerBullet:
                return PlayerBulletColor;
            default:
                return Color.white;
        }
    }
}