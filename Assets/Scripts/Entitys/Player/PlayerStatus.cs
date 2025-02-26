using System;
using System.Collections;
using UnityEngine;

public class PlayerStatus : EntityStatus
{
    public static PlayerStatus instance;

    [Header("Player var's")]
    public int bombAmount;
    public int powerAmount;
    public int score = 0;
    
    public Action onDead;

    private void Awake() {
        instance = this;
    }

    public override void TakeHP(int amount = 1) {
        if(isInvincible) {
            return;
        }

        StartCoroutine(InvincibleSet(3));

        hp -= amount;
        powerAmount = powerAmount * 5 / 6;

        if(hp <= 0) {
            onDead?.Invoke();
        }
    }

    private IEnumerator InvincibleSet(float time) {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet")) {
            Debug.Log("Attacked!");
            Destroy(Instantiate(attackedEffect, other.transform.position, new Quaternion()), 1);
            Destroy(other.gameObject);
            TakeHP();
        }    
    }
}
