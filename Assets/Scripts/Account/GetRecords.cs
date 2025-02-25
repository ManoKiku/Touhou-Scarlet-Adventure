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

    private void Awake() {
        GetRecordsTable();
    }

    public void GetRecordsTable()
    {
        StartCoroutine(WebRequestUtility.GetRequest($"http://touhou-adventure.mooo.com/api/get-records",
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
        },
        (answer) =>{
            Debug.Log(answer);
        }));
    }
}
