using UnityEngine;

public class StatsManager : Singleton<StatsManager>
{
    [SerializeField]
    StatsScriptableObejct data;

    #region FPS
    int frameCount;
    float deltaTime, framesPerSecond;

    public float FramesPerSecond
    {
        get
        {
            return framesPerSecond;
        }
    }

    public Color FrameRateLabelColor
    {
        get
        {
            if(framesPerSecond >= data.NormalFPSLimit)
            {
                return data.NormalFPSColor;
            }
            else if(framesPerSecond >= data.WarningFPSLimit)
            {
                return data.NormalFPSColor;
            }
            else
            {
                return data.CriticalFPSColor;
            }
        }
    }
    #endregion

    int createdObjects, spawnedObjects, activeObjects;

    public int CreatedObjects
    {
        get
        {
            return createdObjects;
        }
        set
        {
            createdObjects = value;
        }
    }

    public int SpawnedObjects
    {
        get
        {
            return spawnedObjects;
        }
        set
        {
            spawnedObjects = value;
        }
    }

    public int ActiveObjects
    {
        get
        {
            return activeObjects;
        }
        set
        {
            activeObjects = value;
        }
    }

    // (Optional) Prevent non-singleton constructor use.
    protected StatsManager() { }

    private void Awake()
    {
        
    }

    public void Update()
    {
        #region FPS
        ++frameCount;
        deltaTime += Time.deltaTime;

        if (deltaTime > 1f / data.UpdatesPerSecond)
        {
            framesPerSecond = frameCount / deltaTime;

            frameCount = 0;
            deltaTime -= 1f / data.UpdatesPerSecond;
        }
        #endregion
    }
}