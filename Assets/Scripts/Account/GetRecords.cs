using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GetRecords : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Text prefab;
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private Toggle unique, username;


    private void OnEnable() {
        if(!PlayerPrefs.HasKey("AccessToken")) {
            username.interactable = false;
            username.isOn = false;
        }
        else{
            username.interactable = true;
        }
        GetRecordsTable(); 
    }

    public void GetRecordsTable()
    {
        string apiUrl = $"http://touhou-adventure.mooo.com/api/get-records?unique={Convert.ToInt32(unique.isOn)}";
        if(username.isOn)
        {
            apiUrl += $"&username={PlayerPrefs.GetString("Username")}";
        }



        Text[] records = parent.gameObject.GetComponentsInChildren<Text>();
        foreach(Text record in records)
        {
            Destroy(record);
        }

        StartCoroutine(WebRequestUtility.GetRequest(apiUrl,
        (answer) => {
            Debug.Log(answer);
            var response = JObject.Parse(answer);

            if(response["status"].ToString() == "error")
            {
                Debug.Log("Error while getting records!");
                return;
            }
            int count = 1;
            foreach(var item in response["records"])
            {
                Text buff = Instantiate(prefab, parent);
                buff.text = $"#{count} {item["username"].ToString()}";
                buff = Instantiate(prefab, parent);
                buff.text = item["score"].ToString();
                count++;
            }
            rectTransform.position = new Vector2(0, -response["records"].Count() * 1500);
            rectTransform.sizeDelta = new Vector2(200, response["records"].Count() * 25);
        },
        (answer) =>{
            Debug.Log(answer);
        }));
        
    }
}
