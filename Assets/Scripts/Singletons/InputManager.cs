using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    ShootieMcShootie controls;

    // (Optional) Prevent non-singleton constructor use.
    protected InputManager() { }

    private void Awake()
    {
        if (controls == null)
        {
            controls = new ShootieMcShootie();
        }
    }

    private void Update()
    {
        
    }

    public void SetCallbacksForPlayer(ShootieMcShootie.IPlayerActions instance)
    {
        controls.Player.SetCallbacks(instance);

        controls.Player.Enable();
    }

    public void SetCallbacksForUI(ShootieMcShootie.IUIActions instance)
    {
        controls.UI.SetCallbacks(instance);

        controls.UI.Enable();
    }
}