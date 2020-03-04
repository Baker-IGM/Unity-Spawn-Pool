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

    [SerializeField]
    StatsScriptableObejct statsData;

    // (Optional) Prevent non-singleton constructor use.
    protected MenuManager() { }

    private void Awake()
    {
        InputManager.Instance.SetCallbacksForUI(this);
    }

    private void Update()
    {
        scoreLabel.text = string.Format("Score - {0}", GameManager.Instance.Score);

        fpsLabel.text = string.Format(statsData.FPSFormat, StatsManager.Instance.FramesPerSecond);
        fpsLabel.color = StatsManager.Instance.FrameRateLabelColor;

        gameTimeLabel.text = string.Format(statsData.GameTimeFormat, Time.time);

        createdLabel.text = string.Format(statsData.CreatedFormat, StatsManager.Instance.CreatedObjects);

        spawnedLabel.text = string.Format(statsData.SpawnedFormat, StatsManager.Instance.SpawnedObjects);

        activeLabel.text = string.Format(statsData.ActiveFormat, StatsManager.Instance.ActiveObjects);
    }

    public void OnDebug(InputAction.CallbackContext context)
    {
        debugPanel.gameObject.SetActive(!debugPanel.gameObject.activeSelf);
    }
}