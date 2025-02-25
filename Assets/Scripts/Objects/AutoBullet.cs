using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoBullet : MonoBehaviour
{
    [SerializeField]
    private float autoAimRadius;
    [SerializeField]
    private float additionalForce;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, autoAimRadius)
                                    .Where(x => x.CompareTag("Enemy")).ToArray();
                               
        if(hitColliders.Length == 0)
            return;
        
        Vector2 force = (hitColliders[0].transform.position - transform.position).normalized * additionalForce;
        rb.AddForce(force);
    }
}
