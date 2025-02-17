using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deathMenu;

    private void Start()
    {
        PlayerStatus.instance.onDead += EndGame;
    }

    public void SaveResult() {
        // TO DO: Implement save mechanic using web API and token system
    }

    public void EndGame() {
        deathMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
