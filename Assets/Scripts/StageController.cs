using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.VFX;

[Serializable]
public class Boss
{
    public GameObject bossPrefab;
    public Vector3 spawnPoint;
    public PlayableDirector director;
    public int bossWave;
}

public class StageController : MonoBehaviour
{
    public static StageController instance;

    [Header("Bosse's settings")]
    public List<Boss> bosses;
    public GameObject BossUI;

    [Header("Player settings")]
    public PlayableDirector onDead;
     
     private void Awake() {
        instance = this;
        WaveSpawner.OnWaveChange += OnWaveChane;
     }

    public void OnWaveChane()
    {
        if(bosses.Count() == 0)
        {
            return;
        }
        if(WaveSpawner.instance.currWave != bosses.First().bossWave) {
            return;
        }

        StartBossFight();
    }

    public void StartBossFight()
    {
        BossUI.SetActive(true);
        WaveSpawner.instance.isWorking = false;

        Boss buff = bosses.First(); 
        Debug.Log(bosses.Count);

        Instantiate(buff.bossPrefab, buff.spawnPoint, new Quaternion());
        buff.director.Play();
        bosses.RemoveAt(0);
    }

    private void OnDestroy() {
        WaveSpawner.OnWaveChange -= OnWaveChane;
    }
}
