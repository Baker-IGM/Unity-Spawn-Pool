using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StatsScriptableObject", order = 1)]
public class StatsScriptableObejct : ScriptableObject
{
    public string FPSFormat = "{0:0.} FPS";
    public string GameTimeFormat = "Total Game Time: {0:#.0}";
    public string CreatedFormat = "Objects Created: {0}";
    public string SpawnedFormat = "Spawned Created: {0}";
    public string ActiveFormat = "Active Objects: {0}";


    public int UpdatesPerSecond;

    public int NormalFPSLimit = 50;
    public Color NormalFPSColor;

    public int WarningFPSLimit = 20;
    public Color WarningFPSColor;

    public Color CriticalFPSColor;
}
