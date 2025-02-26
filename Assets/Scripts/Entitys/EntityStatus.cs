using UnityEngine;

public class EntityStatus : MonoBehaviour
{
    [Header("Main stats")]
    public int hp;
    public bool isInvincible = false;

        
    [Header("Private var's")]
    [SerializeField]
    protected GameObject attackedEffect;

    public virtual void TakeHP(int amount = 1) {
        if(isInvincible) {
            return;
        }

        hp -= amount;

        if(hp <= 0) {
            Destroy(gameObject);
        }
    }
}
