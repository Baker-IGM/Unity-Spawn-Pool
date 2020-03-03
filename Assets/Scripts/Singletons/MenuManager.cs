using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class MenuManager : Singleton<MenuManager>, ShootieMcShootie.IUIActions
{
    [SerializeField]
    RectTransform mainGameRect;

    [SerializeField]
    TMP_Text scoreLabel;

    [SerializeField]
    RectTransform debugPanel;

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
        InputManager.Instance.SetCallbacksForUI(this);
    }

    private void Update()
    {
        scoreLabel.text = GameManager.Instance.Score.ToString();
    }

    public void OnDebug(InputAction.CallbackContext context)
    {
        debugPanel.gameObject.SetActive(!debugPanel.gameObject.activeSelf);
    }
}