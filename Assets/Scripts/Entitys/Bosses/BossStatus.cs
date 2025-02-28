using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
    protected Animator animator;

    [Header("Boss status")]
    public List<BossStage> bossStages;
    public bool isInvincible = false;

    [Header("Bonuses on dead")]
    [SerializeField]
    protected int addScore;
    [SerializeField]
    protected int addHp;
    [SerializeField]
    protected int addBomb;

    [Header("Private var's")]
    [SerializeField]
    protected GameObject attackedEffect;

    private void Awake() {
        GameObject[] toDelete = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(var i in toDelete)
        {
            Destroy(i);
        }

        toDelete = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach(var i in toDelete)
        {
            Destroy(i);
        }

        instance = this;
        animator = GetComponent<Animator>();
        DialogueManager.Instance.onDialogueEnd = () => animator.SetTrigger("Start");    
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
            animator.SetTrigger(buff.animationTrigger);
            bossStages.RemoveAt(0);
        }
        if(bossStages.Count() == 0)
        {
            PlayerStatus.instance.score += addScore;
            PlayerStatus.instance.bombAmount += addBomb;
            PlayerStatus.instance.hp += addHp;

            OnDead();
            return;
        }
    }

    public abstract void OnDead();

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerBullet"))
        {
            TakeHP();
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy() {
        DialogueManager.Instance.onDialogueEnd -= () => animator.SetTrigger("Start");    
    }
}
