using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SlowAndLockBullet : MonoBehaviour
{
    private Vector3 lockTarget;
    private Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField]
    private float fadeTime = 1f;
    [SerializeField]
    private Sprite targetedSprite;
    [SerializeField]
    private float additionalAngle = 90f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SlowBullet());
    }

    IEnumerator SlowBullet()
    {
        Vector2 speed = rb.velocity;
        Debug.Log(speed);
        
        for(float i = 0; i < fadeTime; i += 0.01f)
        {
            rb.velocity = speed * (1 - (i / fadeTime));
            yield return new WaitForSeconds(0.01f);
        }

        lockTarget = PlayerControl.instance.transform.position;
        gameObject.GetComponent<SpriteRenderer>().sprite = targetedSprite;
        rb.velocity = (lockTarget - transform.position).normalized * speed.magnitude;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + additionalAngle));
    }
}
