using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject signGuest;
    [SerializeField]
    private GameObject logOut;
    [SerializeField]
    private GameObject activeButton;
    [SerializeField]
    private EventSystem input;

    private void OnEnable() {
        input.SetSelectedGameObject(activeButton);
        if(!PlayerPrefs.HasKey("AccessToken"))
        {
            signGuest.SetActive(true);
        }
        else
        {
            Debug.Log($"Token: {PlayerPrefs.GetString("AccessToken")}");
            logOut.SetActive(true);
        }
    }

    private void OnDisable() {
        signGuest.SetActive(false);
        logOut.SetActive(false);
    }
}
