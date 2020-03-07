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

    public bool SetCallbacks(Object instance)
    {
        bool foundInterface = false;

        if (instance is ShootieMcShootie.IPlayerActions)
        {
            controls.Player.SetCallbacks((ShootieMcShootie.IPlayerActions)instance);

            controls.Player.Enable();

            foundInterface = true;
        }

        if (instance is ShootieMcShootie.IUIActions)
        {
            controls.UI.SetCallbacks((ShootieMcShootie.IUIActions)instance);

            controls.UI.Enable();

            foundInterface = true;
        }

        return foundInterface;
    }
}