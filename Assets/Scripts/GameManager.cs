using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Player player;

    // (Optional) Prevent non-singleton constructor use.
    protected GameManager() { }

    private void Awake()
    {
        
    }

    public Vector2 GetPlayerLocation()
    {
        return player.Position;
    }
}