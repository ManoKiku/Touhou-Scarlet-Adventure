using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject signGuest;
    [SerializeField]
    private GameObject logOut;

    private void OnEnable() {
        if(!PlayerPrefs.HasKey("AccessToken"))
        {
            Debug.Log($"Token: {PlayerPrefs.GetString("AccessToken")}");
            signGuest.SetActive(true);
        }
        else
        {
            logOut.SetActive(true);
        }
    }
}
