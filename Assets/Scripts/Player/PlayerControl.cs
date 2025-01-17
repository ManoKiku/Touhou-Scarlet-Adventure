using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public bool isActive = true;

    [SerializeField]
    private float speed;
    
    private void Awake() 
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isActive || DialogueManager.Instance.isDialogueActive) 
        {
            rb.velocity = Vector2.zero;
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 axis = GameInput.instance.GetMovementVector();
        rb.velocity = axis.normalized * speed;
    }
}
