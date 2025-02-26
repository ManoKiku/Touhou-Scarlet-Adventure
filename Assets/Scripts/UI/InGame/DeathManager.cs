using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deathMenu;
    [SerializeField]
    private Text deathScore;
    [SerializeField]
    private Animator playerAnimator;

    private void Start()
    {
        PlayerStatus.instance.onDead += EndGame;
    }

    public void EndGame() {
        PlayerControl.instance.isActive = false;
        PlayerControl.instance.axis = Vector2.zero;
        playerAnimator.SetTrigger("Death");
        StartCoroutine(DeathMenuWait());
    }

    public IEnumerator DeathMenuWait() {
        yield return new WaitForSeconds(2);
        deathMenu.SetActive(true);
        deathScore.text = $"Yo're dead\nScore {PlayerStatus.instance.score}";
        Time.timeScale = 0;
    }
}
