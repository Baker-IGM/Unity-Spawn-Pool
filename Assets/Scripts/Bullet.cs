using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : RectGameObject
{
    Rigidbody2D rBody;

    [SerializeField]
    float speed;

	// Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        rBody = GetComponent<Rigidbody2D>();

        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!IsWithinBounds(rect.anchoredPosition))
        {
            if (!BulletManager.IsShuttingDown())
                BulletManager.Instance.ReturnBullet(transform);
        }
	}

    public void Fire(Vector2 position, Quaternion direction)
    {
        rect.anchoredPosition3D = new Vector3(position.x, position.y, 0);

        rect.rotation = direction;

        gameObject.SetActive(true);

        rBody.AddForce(rect.up * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  Check what layer the bullet is
        switch(gameObject.layer)
        {
            case (int)BulletLayers.EnemyBullet:
                GameManager.Instance.ResetScore();
                break;
            case (int)BulletLayers.PlayerBullet:
                GameManager.Instance.AddPoints(1);
                break;
        }
    }

    bool IsWithinBounds(Vector2 pos)
    {
        if (pos.x - rect.rect.width / 2f > MenuManager.Instance.MainGameRect.rect.width / 2f ||
            pos.x + rect.rect.width / 2f < MenuManager.Instance.MainGameRect.rect.width / -2f ||
            pos.y - rect.rect.height / 2f > MenuManager.Instance.MainGameRect.rect.height / 2f ||
            pos.y + rect.rect.height / 2f < MenuManager.Instance.MainGameRect.rect.height / -2f)
        {
            return false;
        }

        return true;
    }
}