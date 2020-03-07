using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Image))]
public class Bullet : RectGameObject
{
    Rigidbody2D rBody;

    [SerializeField]
    float speed;

    Image sprite;

	// Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        rBody = GetComponent<Rigidbody2D>();

        rect = GetComponent<RectTransform>();

        sprite = GetComponent<Image>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!IsVisible(rect.anchoredPosition))
        {
            CleanUp();
        }
	}

    public void Fire(Vector2 position, Quaternion direction)
    {
        rect.anchoredPosition3D = new Vector3(position.x, position.y, 0);

        rect.rotation = direction;

        SetColor(BulletManager.Instance.GetColor((BulletType)gameObject.layer));

        gameObject.SetActive(true);

        rBody.AddForce(rect.up * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<RectGameObject>().OnHit(gameObject.layer);

        CleanUp();
    }

    bool IsVisible(Vector2 pos)
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

    public void SetColor(Color color)
    {
        sprite.color = color;
    }

    public override void OnHit(int otherLayer)
    {
        CleanUp();
    }

    public override void CleanUp()
    {
        base.CleanUp();

        if (!BulletManager.IsShuttingDown())
        {
            BulletManager.Instance.ReturnBullet(this);
        }
    }
}