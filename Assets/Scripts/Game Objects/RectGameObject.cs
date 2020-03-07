using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Collider2D))]
public abstract class RectGameObject : MonoBehaviour
{
    protected RectTransform rect;
    protected new Collider2D collider;

    protected bool isVisible;

    protected Vector2 position;
    public Vector2 Position
    {
        get
        {
            return position;
        }
    }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();

        collider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        position = rect.anchoredPosition;

        if (!isVisible && IsOnScreen())
        {
            isVisible = true;
        }
        else if (!IsOnScreen())
        {
            CleanUp();
        }
    }

    public abstract void OnHit(int otherLayer);

    bool IsOnScreen()
    {
        if (position.x - rect.rect.width / 2f > MenuManager.Instance.MainGameRect.rect.width / 2f ||
            position.x + rect.rect.width / 2f < MenuManager.Instance.MainGameRect.rect.width / -2f ||
            position.y - rect.rect.height / 2f > MenuManager.Instance.MainGameRect.rect.height / 2f ||
            position.y + rect.rect.height / 2f < MenuManager.Instance.MainGameRect.rect.height / -2f)
        {
            return false;
        }

        return true;
    }

    public virtual void CleanUp()
    {
        isVisible = false;
    }
}
