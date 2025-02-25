using System.Collections.Generic;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField username;
    [SerializeField]
    private TMP_InputField password;
    [SerializeField]
    private TMP_InputField isPassword;
    [SerializeField]
    private Text errorBox;

    private void OnEnable() {
        errorBox.text = "";
        username.text = "";
        password.text = "";
        isPassword.text = "";
    }

    public void Registation()
    {
        errorBox.color = Color.red;
        errorBox.text = "";
        if(password.text == string.Empty || username.text == string.Empty)
        {
            errorBox.text = "Password and username are required!";
            return;
        }
        if(username.text.Length < 4)
        {
            errorBox.text = "Username must be 4 characters or more";
            return;
        }
        if(password.text.Length < 8)
        {
            errorBox.text = "Password must be 8 characters or more";
            return;
        }
        if(password.text != isPassword.text)
        {
            errorBox.text = "Password's are not the same!";
            return;
        }


        StartCoroutine(WebRequestUtility.GetRequest($"http://touhou-adventure.mooo.com/api/sign-up?username={username.text}&password={password.text}",
        (answer) => {
            Debug.Log(answer);
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(answer);

            if (response["status"] == "success")
            {
                Debug.Log("Login successful!");
                errorBox.text = response["message"];
                errorBox.color = Color.green;
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