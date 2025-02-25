using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    [Header("Main stats")]
    public int hp;
    public int bombAmount;
    public int powerAmount;
    private bool isInvincible = false;

    [Header("Private var's")]
    [SerializeField]
    private float bombRadius = 3;


    private void Awake() {
        instance = instance ?? this;
    }

    public void TakeHP() {
        if(isInvincible) {
            return;
        }

        StartCoroutine(InvincibleSet(3));

        hp--;
        powerAmount = powerAmount * 5 / 6;
    }

    public void UseBomb() {
        if(bombAmount <= 0) {
            return;
        }

        List<Collider2D> objects = Physics2D.OverlapCircleAll(transform.position, bombRadius).ToList<Collider2D>();
        foreach(var obj in objects) {
            if(obj.CompareTag("Bullet")) {
                Destroy(obj.gameObject);
            }
        }
 
        bombAmount--;
    }

    private IEnumerator InvincibleSet(float time) {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet")) {
            Debug.Log("Attacked!");
            TakeHP();
        }    
    }
}
