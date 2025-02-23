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
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private WaveSpawner waveSpawner;

    private void Update() {
        hpText.text = $"HP {PlayerStatus.instance.hp}";
        powerText.text = $"PW {PlayerStatus.instance.powerAmount} - 128";
        bombText.text = $"BM {PlayerStatus.instance.bombAmount}";
        scoreText.text = $"Score {PlayerStatus.instance.score}";
        waveText.text = $"Wave {waveSpawner.currWave}";
    }
}
