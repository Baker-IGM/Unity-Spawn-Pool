using UnityEngine;


[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Collider2D))]
public abstract class RectGameObject : MonoBehaviour
{
    protected RectTransform rect;
    protected Collider2D collider;

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
    }

    public abstract void OnHit(int otherLayer);
}
