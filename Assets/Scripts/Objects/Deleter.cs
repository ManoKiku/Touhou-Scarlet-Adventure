using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Contains("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
