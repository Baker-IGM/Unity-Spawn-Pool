using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletManagerScriptableObject", order = 1)]
public class BulletManagerScriptableObejct : ScriptableObject
{
    public int startingBulletCount;

    public string bulletName = "Bullet_{0:d4}";

    public Color playerBulletColor;

    public Color enemyBulletColor;
}
