using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : Singleton<MenuManager>
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
        
    }

    private void Update()
    {
        scoreLabel.text = GameManager.Instance.Score.ToString();
    }

    void OnDebug()
    {
        debugPanel.gameObject.SetActive(!debugPanel.gameObject.activeSelf);
    }
}