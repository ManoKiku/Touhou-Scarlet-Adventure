using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private Text powerText;
    [SerializeField]
    private Text bombText;
    private void Update() {
        hpText.text = $"HP: {PlayerStatus.instance.hp}/5";
        powerText.text = $"P: {PlayerStatus.instance.powerAmount}/128";
        bombText.text = $"B: {PlayerStatus.instance.bombAmount}/5";

    }
}
