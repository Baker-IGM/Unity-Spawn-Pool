using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField]
    int startEnemyCount;

    [SerializeField]
    GameObject enemyPrefab;

    int count;

    // (Optional) Prevent non-singleton constructor use.
    protected EnemyManager() { }

    private void Awake()
    {
        for(int i = 0; i < startEnemyCount; ++i)
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform, false);

        newEnemy.transform.rotation = Quaternion.identity;
        newEnemy.transform.localScale = Vector3.one;

        newEnemy.name = string.Format("Enemy_{0}", count);

        newEnemy.SetActive(false);

        newEnemy.transform.SetAsFirstSibling();

        ++count;
        ++StatsManager.Instance.CreatedObjects;
    }

    public Enemy GetEnemy()
    {
        if(transform.childCount <= 0 || transform.GetChild(0).gameObject.activeSelf)
        {
            CreateEnemy();
        }

        Transform selectedEnemy = transform.GetChild(0);
        selectedEnemy.SetAsLastSibling();

        ++StatsManager.Instance.SpawnedObjects;
        ++StatsManager.Instance.ActiveObjects;

        return selectedEnemy.GetComponent<Enemy>();
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.transform.SetParent(transform, false);

        enemy.gameObject.SetActive(false);

        enemy.transform.SetAsLastSibling();

        --StatsManager.Instance.ActiveObjects;
    }
}