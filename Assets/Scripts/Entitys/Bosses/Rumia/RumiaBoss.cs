using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RumiaBoss : BossStatus
{
    public Dialogue onDeadDialogue;

    public override void OnDead()
    {
        Debug.Log("Dead!");
        GameObject[] toDelete = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(var i in toDelete)
        {
            Destroy(i);
        }

        gameObject.GetComponentInChildren<EntityVisual>().animator.SetTrigger("Death");
        DialogueManager.Instance.StartDialogue(onDeadDialogue);
        DialogueManager.Instance.onDialogueEnd += DestroyBoss;
    }

    public void DestroyBoss()
    {
        DialogueManager.Instance.onDialogueEnd -= DestroyBoss;
        StageController.instance.onDead.Play();
        StageController.instance.BossUI.SetActive(false);
        WaveSpawner.instance.isWorking = true;
        Destroy(gameObject);
    }
}
