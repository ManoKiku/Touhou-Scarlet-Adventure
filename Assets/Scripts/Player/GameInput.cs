using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }

    public PlayerInputActions action;
    
    void OnEnable()
    {
        action.Enable();
    }

    void OnDisable()
    {
        action.Disable();
    }

    private void Awake()
    {
        instance = this;

        action = new PlayerInputActions();
        action.Enable();
    }
    public Vector2 GetMovementVector()
    {
        return action.Player.Move.ReadValue<Vector2>();
    }
}
