using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.Log(PlayerControl.instance.rb.velocity.magnitude);
        Debug.Log(PlayerControl.instance.rb.velocity);
        isMoving = Mathf.Abs(PlayerControl.instance.rb.velocity.magnitude) > 0.1f;

        if (isMoving)
        {
            animator.SetFloat("X", PlayerControl.instance.rb.velocityX);
            animator.SetFloat("Y", PlayerControl.instance.rb.velocityY);
        }

        animator.SetBool("isMoving", isMoving);
    }
}
