using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AIPath path;
    private Transform target;

    [SerializeField] 
    private float moveSpeed;
    [SerializeField] 
    private float stopDistance;
    [SerializeField] 
    private float viewDistance;
    [SerializeField] 
    private float walkingRadius;
    
    

    private void Start()
    {
        path = GetComponent<AIPath>();
        path.enableRotation = false;
        path.radius = 5;
        target = PlayerControl.instance.transform;
        StartCoroutine(GenerateCoordinates());
    }


    private IEnumerator GenerateCoordinates() {
        while (true) 
        {
            yield return new WaitForSeconds(0.5f);
            path.maxSpeed = moveSpeed;

            float distanceToTarger = Vector2.Distance(transform.position, target.position); 
            
            if(distanceToTarger <= viewDistance && stopDistance <= distanceToTarger)
            {
                path.destination = target.position;
            } 
            else 
            {
                path.destination = transform.position +  new Vector3(Random.Range(-walkingRadius * 100, walkingRadius * 100), Random.Range(-walkingRadius * 100, walkingRadius * 100), 0) / 100;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}