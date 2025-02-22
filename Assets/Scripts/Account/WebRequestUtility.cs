using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Networking;
using UnityEngine;

public static class WebRequestUtility
{
    public static IEnumerator GetRequest(string url, System.Action<string> onSuccess, System.Action<string> onError, Dictionary<string, string> headers = null)
    {
        headers = headers ?? new Dictionary<string, string>();

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            foreach(var i in headers)
            {
                webRequest.SetRequestHeader(i.Key, i.Value);
            }
            webRequest.SetRequestHeader("User-Agent", "Unity");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(webRequest.error);
            }
            else
            {
                onSuccess?.Invoke(webRequest.downloadHandler.text);
            }
        }
    }

    public static IEnumerator PostRequest(string url, string jsonBody, System.Action<string> onSuccess, System.Action<string> onError, Dictionary<string, string> headers = null)
    {
        headers = headers ?? new Dictionary<string, string>();

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("User-Agent", "Unity");

            foreach(var i in headers)
            {
                webRequest.SetRequestHeader(i.Key, i.Value);
            }

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(webRequest.error);
            }
            else
            {
                onSuccess?.Invoke(webRequest.downloadHandler.text);
            }
        }
    }
}