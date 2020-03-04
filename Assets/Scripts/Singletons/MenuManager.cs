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

    [SerializeField]
    TMP_Text fpsLabel;

    [SerializeField]
    TMP_Text gameTimeLabel;

    [SerializeField]
    TMP_Text createdLabel;

    [SerializeField]
    TMP_Text spawnedLabel;

    [SerializeField]
    TMP_Text activeLabel;

    // (Optional) Prevent non-singleton constructor use.
    protected MenuManager() { }

    private void Awake()
    {
        InputManager.Instance.SetCallbacksForUI(this);
    }

    private void Update()
    {
        scoreLabel.text = GameManager.Instance.Score.ToString();

        fpsLabel.text = string.Format("{0:0.} FPS", StatsManager.Instance.FramesPerSecond);

        gameTimeLabel.text = string.Format("Total Game Time: {0:#.0}", Time.time);

        createdLabel.text = string.Format("Objects Created: {0}", StatsManager.Instance.CreatedObjects);

        spawnedLabel.text = string.Format("Spawned Created: {0}", StatsManager.Instance.SpawnedObjects);

        activeLabel.text = string.Format("Active Objects: {0}", StatsManager.Instance.ActiveObjects);
    }

    public void OnDebug(InputAction.CallbackContext context)
    {
        debugPanel.gameObject.SetActive(!debugPanel.gameObject.activeSelf);
    }
}