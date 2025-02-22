using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOut : MonoBehaviour
{
    
    [SerializeField]
    GameObject signInMenu;
    [SerializeField]
    GameObject mainMenu;

    public void LogOutOfAccount()
    {
        Dictionary<string, string> keys = new Dictionary<string, string>();
        keys.Add("Authorization", PlayerPrefs.GetString("AccessToken"));

        StartCoroutine(WebRequestUtility.GetRequest("http://touhou-adventure.mooo.com/api/logout", (i) => Debug.Log(i), (i) => PlayerPrefs.DeleteKey("AccessToken"), keys));

        PlayerPrefs.DeleteKey("Username");
        PlayerPrefs.DeleteKey("RefreshToken");
        mainMenu.SetActive(false);
        signInMenu.SetActive(true);
    }
}
