using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }

    private PlayerInputActions action;

    private void Awake()
    {
        instance = instance ?? this;

        action = new PlayerInputActions();
        action.Enable();
    }
    public Vector2 GetMovementVector()
    {
        return action.Player.Move.ReadValue<Vector2>();
    }

    public bool GetMenuInput()
    {
        return action.UI.MenuOpenClose.WasPressedThisFrame();
    }

}
