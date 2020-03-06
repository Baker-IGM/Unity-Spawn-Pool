using UnityEngine;

enum Layers
{
    Player = 8,
    Enemy = 10
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Player player;

    int score;
    public int Score
    {
        get
        {
            return score;
        }
    }

    [SerializeField]
    float enemySpawnRate;
    float enemySpawnTimer;

    // (Optional) Prevent non-singleton constructor use.
    protected GameManager() { }

    private void Start()
    {
        enemySpawnTimer = enemySpawnRate;
    }

    private void Update()
    {
        enemySpawnTimer -= Time.deltaTime;

        if(enemySpawnTimer <= 0)
        {
            SpawnEnemy();

            enemySpawnTimer = enemySpawnRate;
        }
    }

    public Vector2 GetPlayerLocation()
    {
        return player.Position;
    }

    #region Score Logic
    public void AddPoints(int value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
    }
    #endregion

    void SpawnEnemy()
    {
        Enemy newEnemy = EnemyManager.Instance.GetEnemy();

        newEnemy.transform.SetParent(MenuManager.Instance.MainGameRect);

        Vector2 spawnLocation = Vector2.zero;

        newEnemy.Spawn(Random.Range(0, 4));
    }
}