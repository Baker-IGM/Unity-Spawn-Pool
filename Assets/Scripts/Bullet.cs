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
        if (MenuManager.Instance.MainGameRect.rect.Contains(rect.rect.position))
        {
            if (!BulletManager.IsShuttingDown())
                BulletManager.Instance.ReturnBullet(transform);
        }
	}

    public void Fire(Vector2 position, Quaternion direction)
    {
        rect.anchoredPosition = position;

        rect.rotation = direction;

        gameObject.SetActive(true);

        rBody.AddForce(rect.up * speed, ForceMode2D.Impulse);
    }
}