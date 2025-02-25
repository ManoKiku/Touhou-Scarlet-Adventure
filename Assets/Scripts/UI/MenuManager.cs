using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu objects")]
    [SerializeField]
    private GameObject menuCanvas;
    [SerializeField]
    private GameObject menuActiveButton;
    [SerializeField]
    private GameObject areYouSureCanvas;

    [Header("Event system")]
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject dialogueButton;

    bool isPaused = false;

    private void Awake()
    {
        menuCanvas.SetActive(false);
    }

    private void Start()
    {
        GameInput.instance.action.UI.MenuOpenClose.performed += HandleMenu;
    }

    void HandleMenu(InputAction.CallbackContext e) {
        if (!isPaused) 
        {
            Pause();
        }
        else 
        {
            UnPause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;

        OpenMenu();
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        eventSystem.SetSelectedGameObject(dialogueButton);
        CloseAllMenus();
    }
    void OpenMenu()
    {
        eventSystem.SetSelectedGameObject(menuActiveButton);
        menuCanvas.SetActive(true);
        areYouSureCanvas.SetActive(false);
    }

    void CloseAllMenus()
    {
        menuCanvas.SetActive(false);
        areYouSureCanvas.SetActive(false);
    }
}
