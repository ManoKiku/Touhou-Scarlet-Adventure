using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }
    private PlayerInputActions action;

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
