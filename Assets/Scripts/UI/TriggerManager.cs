using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnerManager;

    public void StartGame() 
    {
        spawnerManager.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) {
            StartGame();
        }
    }
}
