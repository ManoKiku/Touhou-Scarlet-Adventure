using UnityEngine;

public class EntityControl : MonoBehaviour
{
    public Rigidbody2D rb { get; protected set; }
    public bool isActive = true;
    public Vector2 axis;

    protected Vector2 lastNoZeroAxis;

    [Header("Entity stats")]
    [SerializeField]
    protected float speed;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        if(axis != Vector2.zero) {
            lastNoZeroAxis = axis;
        }

        if(DialogueManager.Instance.isDialogueActive)
        {
            axis = Vector2.zero;
        }

        rb.velocity = axis.normalized * speed;
    }
}
