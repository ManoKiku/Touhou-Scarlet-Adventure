using System.Linq;
using UnityEngine;

[System.Serializable]
public struct SpawnableObject
{
    public GameObject gameObject;
    public int chance;
}

public class EnemyStatus : MonoBehaviour
{
    [Header("Main stats")]
    public int hp;
    public int scoreOnDead;

    [SerializeField]
    private SpawnableObject[] bonuses;
    [SerializeField]
    private GameObject attackedEffect;

    public void TakeHP(int takeHp = 1) {
        hp -= takeHp;
        Destroy(Instantiate(attackedEffect, transform.position, new Quaternion()), 1);

        if(hp <= 0) {
            PlayerStatus.instance.score += scoreOnDead;
            GameObject buff = GetRandomObject(bonuses);
            if(buff != null)
            {
                Instantiate(buff, transform.position, new Quaternion());
            }
            Destroy(this.gameObject);
        }
    }

    GameObject GetRandomObject(SpawnableObject[] spawnableObjects)
    {
        int totalChance = spawnableObjects.Select(x => x.chance).Sum();

        if (spawnableObjects.Length == 0 || totalChance <= 0)
        {
            Debug.LogError("Invalid spawn configuration!");
            return null;
        }

        int randomNumber = Random.Range(0, totalChance);
        int runningTotal = 0;

        foreach (SpawnableObject obj in spawnableObjects)
        {
            runningTotal += obj.chance;
            if (randomNumber >= runningTotal - obj.chance && randomNumber < runningTotal)
            {
                return obj.gameObject;
            }
        }

        return spawnableObjects[0].gameObject;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerBullet")) {
            Destroy(other.gameObject);
            TakeHP();
        }    
    }
}
