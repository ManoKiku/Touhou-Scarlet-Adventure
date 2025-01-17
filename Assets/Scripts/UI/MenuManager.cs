using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Menu objects")]
    [SerializeField]
    private GameObject menuCanvas;
    [SerializeField]
    private GameObject areYouSureCanvas;

    bool isPaused = false;

    private void Awake()
    {
        menuCanvas.SetActive(false);
    }
    void Update()
    {
        if (!GameInput.instance.GetMenuInput())
        {
            return;
        }

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

        CloseAllMenus();
    }
    void OpenMenu()
    {
        menuCanvas.SetActive(true);
        areYouSureCanvas.SetActive(false);
    }

    void CloseAllMenus()
    {
        menuCanvas.SetActive(false);
        areYouSureCanvas.SetActive(false);
    }
}
