using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestPlayer : MonoBehaviour
{
    public void SetUsername(string name)
    {
        PlayerPrefs.SetString("Username", "g");
    }
}
