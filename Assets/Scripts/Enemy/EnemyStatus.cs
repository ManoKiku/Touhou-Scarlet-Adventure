using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Main stats")]
    public int hp;
    public int scoreOnDead;

    public void TakeHP() {
        hp--;

        if(hp <= 0) {
            PlayerStatus.instance.score += scoreOnDead;
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
