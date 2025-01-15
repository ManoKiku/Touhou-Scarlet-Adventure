using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance { get; private set; }

    public Rigidbody2D rb { get; private set; }

    [SerializeField]
    private float speed;

    private void Awake() 
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 axis = GameInput.instance.GetMovementVector();
        rb.velocity = axis.normalized * speed;
    }
}
