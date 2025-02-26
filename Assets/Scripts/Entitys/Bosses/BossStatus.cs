using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BossStage
{
    public int hpAmount;
    public int maxHP;
    public string animationTrigger;
}

public abstract class BossStatus : MonoBehaviour
{
    public static BossStatus instance;
    private Animator animator;

    [Header("Boss status")]
    public List<BossStage> bossStages;
    public bool isInvincible = false;

    [Header("Private var's")]
    [SerializeField]
    protected GameObject attackedEffect;

    private void Awake() {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public virtual void TakeHP(int amount = 1)
    {
        if(isInvincible || bossStages.Count() == 0)
        {
            return;
        }

        Destroy(Instantiate(attackedEffect, transform.position, transform.rotation), 1f);

        BossStage buff = bossStages.First();

        buff.hpAmount -= amount;

        if(buff.hpAmount <= 0)
        {
            bossStages.RemoveAt(0);
        }
        if(bossStages.Count() == 0)
        {
            OnDead();
            return;
        }

        animator.SetTrigger(bossStages.First().animationTrigger);
    }

    public abstract void OnDead();

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerBullet"))
        {
            TakeHP();
            Destroy(other.gameObject);
        }
    }
}
