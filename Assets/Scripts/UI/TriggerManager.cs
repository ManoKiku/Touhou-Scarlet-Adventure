using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnerManager; 
    [SerializeField]
    private GameObject mapColider; 

    public void StartGame() 
    {
        spawnerManager.SetActive(true);
        mapColider.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) {
            StartGame();
        }
    }
}
