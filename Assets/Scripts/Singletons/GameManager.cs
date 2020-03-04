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

    [SerializeField]
    int score;

    public int Score
    {
        get
        {
            return score;
        }
    }

   

    // (Optional) Prevent non-singleton constructor use.
    protected GameManager() { }

    public Vector2 GetPlayerLocation()
    {
        return player.Position;
    }

    public void AddPoints(int value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
    }
}