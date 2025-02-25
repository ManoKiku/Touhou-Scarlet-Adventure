using System.Collections.Generic;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;
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
    [SerializeField]
    private Animator fade;

    private void OnEnable() {
        username.text = "";
        password.text = "";
        errorBox.text = "";
    }

    private void Awake() {
        Time.timeScale = 1;

        if(!PlayerPrefs.HasKey("Username"))
        {
            fade.Play("Hide");
            return;
        }

        string name = PlayerPrefs.GetString("Username");

        if(name == "g") 
        {
            fade.Play("Hide");
            mainMenu.SetActive(true);
            signInMenu.SetActive(false);
            return;
        }

        var headers = new Dictionary<string, string>
        {
            { "Authorization", PlayerPrefs.GetString("AccessToken") }
        };

        StartCoroutine(WebRequestUtility.GetRequest($"http://touhou-adventure.mooo.com/api/validate-token",
        (answer) =>
        {
            Debug.Log(answer);
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(answer);
            fade.Play("Hide");

            if (response["status"] == "success")
            {
                Debug.Log("Token is active");
                mainMenu.SetActive(true);
                signInMenu.SetActive(false);
            }
            else
            {
                Debug.Log("Error: " + response["message"]);
                errorBox.text = "Session is expired! Login one more time";
            }
        },
        (answer) =>
        {
            errorBox.text = "Problems connecting to the server! Try later or contact the administration!";
            fade.Play("Hide");
        }, headers));
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
            fade.Play("Hide");

            if (response["status"] == "success")
            {
                Debug.Log("Login successful!");
                PlayerPrefs.SetString("AccessToken", response["access_token"]);
                PlayerPrefs.SetString("Username", nameBuff);
                mainMenu.SetActive(true);
                signInMenu.SetActive(false);
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
            fade.Play("Hide");
        }));
    }
}