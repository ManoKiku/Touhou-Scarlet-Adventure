using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignInHide : MonoBehaviour
{
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
}
