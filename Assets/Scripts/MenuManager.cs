using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    RectTransform mainGameRect;

    public RectTransform MainGameRect
    {
        get
        {
            return mainGameRect;
        }
    }

    // (Optional) Prevent non-singleton constructor use.
    protected MenuManager() { }

    private void Awake()
    {
        
    }

    public Vector2 GetCanvasScale()
    {
        return canvas.pixelRect.size;
    }
}