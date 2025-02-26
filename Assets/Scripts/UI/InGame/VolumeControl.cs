using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    private void Awake() {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicPreference");
    }
}
