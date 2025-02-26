using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnerManager; 
    [SerializeField]
    private GameObject mapColider; 

    private void Start() {
        DialogueManager.Instance.onDialogueEnd += StartGame;
    }

    public void StartGame() 
    {
        spawnerManager.SetActive(true);
        mapColider.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        DialogueManager.Instance.onDialogueEnd -= StartGame;
    }
}
