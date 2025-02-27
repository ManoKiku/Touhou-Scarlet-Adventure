using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    [SerializeField]
    private EntityControl entityControl;
    public Animator animator;
    private bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isMoving = Mathf.Abs(entityControl.rb.velocity.magnitude) > 0.1f;

        if (isMoving)
        {
            animator.SetFloat("X", entityControl.rb.velocityX);
            animator.SetFloat("Y", entityControl.rb.velocityY);
        }

        animator.SetBool("isMoving", isMoving);
    }
}
