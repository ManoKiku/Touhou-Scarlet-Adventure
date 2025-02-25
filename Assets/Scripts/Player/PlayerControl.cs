using System;
using UnityEngine;

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
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();

        if(!isActive) {
            return;
        }

        HandleUse();

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

    private void HandleUse() {
        if(GameInput.instance.GetUseInput()) {
            PlayerStatus.instance.UseBomb();
        }
    }
}
