using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public bool isActive = true;
    public Vector2 axis;

    [SerializeField]
    private float speed;


    private void Awake()
    {
        Time.timeScale = 1.0f;
        instance = this;
        
        rb = GetComponent<Rigidbody2D>();
        GameInput.instance.action.Player.Use.performed += HandleUse;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(isActive)
        {
            axis = GameInput.instance.GetMovementVector();
        }
        if(DialogueManager.Instance.isDialogueActive)
        {
            axis = Vector2.zero;
        }

        rb.velocity = axis.normalized * speed;
    }

    private void HandleUse(InputAction.CallbackContext e) {
        if(isActive) {
            PlayerStatus.instance.UseBomb();
        }
    }

    private void HandleAttack(InputAction.CallbackContext e) 
    {
        // TO DO: Implement attack mechanic
    }
}
