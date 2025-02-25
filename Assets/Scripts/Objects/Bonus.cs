using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float additionalForce;
    [SerializeField]
    protected int addValue;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius)
                                    .Where(x => x.CompareTag("Player")).ToArray();
                               
        if(hitColliders.Length == 0)
            return;
        
        Vector2 force = (hitColliders[0].transform.position - transform.position).normalized * additionalForce;
        rb.AddForce(force);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            AddBonus();
            Destroy(gameObject);
        }
    }

    protected virtual void AddBonus()
    {
        PlayerStatus.instance.score += addValue;
    }
}
