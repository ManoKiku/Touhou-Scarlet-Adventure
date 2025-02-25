using System.Collections;
using System.Collections.Generic;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    [SerializeField]
    GameObject signInMenu;
    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    private TMP_InputField username;
    [SerializeField]
    private TMP_InputField password;
    [SerializeField]
    private Text errorBox;

    private void OnEnable() {
        username.text = "";
        password.text = "";
        errorBox.text = "";
    }

    private void Awake() {
        if(PlayerPrefs.HasKey("Username"))
        {
            mainMenu.SetActive(true);
            signInMenu.SetActive(false);
        }
    }

    public void LogIn()
    {
        errorBox.text = "";
        if(password.text == string.Empty || username.text == string.Empty)
        {
            errorBox.text = "Password and username are required!";
            return;
        }
        
        string nameBuff = username.text;
        StartCoroutine(WebRequestUtility.GetRequest($"http://touhou-adventure.mooo.com/api/sign-in?username={username.text}&password={password.text}",
        (answer) => {
            Debug.Log(answer);
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(answer);


            if (response["status"] == "success")
            {
                Debug.Log("Login successful!");
                mainMenu.SetActive(true);
                signInMenu.SetActive(false);
                PlayerPrefs.SetString("AccessToken", response["access_token"]);
                PlayerPrefs.SetString("RefreshToken", response["refresh_token"]);
                PlayerPrefs.SetString("Username", nameBuff);
            }
            else
            {
                Debug.Log("Error: " + response["message"]);
                errorBox.text = response["message"];
            }
        },
        (answer) =>{
            Debug.Log(answer);
            errorBox.text = "Problems with connection. Please, try again later";
        }));
    }
}