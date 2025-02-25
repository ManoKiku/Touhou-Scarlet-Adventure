using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Main stats")]
    public int hp;
    public int scoreOnDead;

    [SerializeField]
    private GameObject[] bonuses;
    [SerializeField]
    private GameObject attackedEffect;

    public void TakeHP(int takeHp = 1) {
        hp -= takeHp;
        Destroy(Instantiate(attackedEffect, transform.position, new Quaternion()), 1);

        if(hp <= 0) {
            PlayerStatus.instance.score += scoreOnDead;
            Instantiate(bonuses[Random.Range(0, bonuses.Length)], transform.position, new Quaternion());
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerBullet")) {
            Destroy(other.gameObject);
            TakeHP();
        }    
    }
}
