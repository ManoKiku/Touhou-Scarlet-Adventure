using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SaveRecord : MonoBehaviour
{
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Text errorBox;

    private void OnEnable() {
        if(!PlayerPrefs.HasKey("AccessToken"))
        {
            Debug.Log($"Token: {PlayerPrefs.GetString("AccessToken")}");
            saveButton.interactable = false;
            errorBox.text = "To save the record you need to log in to your account!";
        }
    }

    public void SaveNewRecord()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", PlayerPrefs.GetString("AccessToken"));

        StartCoroutine(WebRequestUtility.GetRequest($"http://touhou-adventure.mooo.com/api/add-record?score={PlayerStatus.instance.score}",
        (answer) => {
            Debug.Log(answer);
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(answer);
            if(response["status"] == "error") {
                errorBox.text = "Problem's with adding record";
                return;
            }
            errorBox.color = Color.green;
            errorBox.text = response["message"];
            saveButton.interactable = false;
        },
        (answer) =>{
            Debug.Log(answer);
            errorBox.text = "Problems with connection. Please, try again later";
        }, headers));
    }
}
