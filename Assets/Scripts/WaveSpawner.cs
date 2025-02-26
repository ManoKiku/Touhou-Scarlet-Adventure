using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EnemyWaveProgression
{
    public double a; // Additional offset
    public double b; // Scaling factor
    public double c; // Controls the frequency of the sine wave
    public double d; // Scaling for linear component

    public double CalculateWaveValue(double x)
    {
        double sineComponent = Math.Sin(x / c);
        double linearComponent = d * x;
        double waveValue = a + b * (sineComponent + linearComponent);

        return waveValue;
    }
}

 
[Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int cost;
}
 
public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public static Action OnWaveChange;
    public bool isActive = true;

   [Header("Wave settings")]
    public int currWave = 1;
    private int waveValue;
    public float spawnInterval;
    public float additionalWaveTime = 10;

    public EnemyWaveProgression progression;
    public List<WaveEnemy> enemies = new List<WaveEnemy>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();
 
    [Header("Spawn locations")]
    [SerializeField]
    private Transform[] spawnLocation;
    public int spawnIndex {get; private set;}
    public int waveDuration {get; private set;}
    public float waveTimer {get; private set;}
    public float spawnTimer {get; private set; }
 
    void Start()
    {
        instance = this;
        GenerateWave();
    }
 
    void FixedUpdate()
    {
        if(!isActive) {
            return;
        }

        if(spawnTimer <=0)
        {
            if(enemiesToSpawn.Count >0)
            {
                spawnIndex = UnityEngine.Random.Range(0, spawnLocation.Length);
                GameObject enemy = Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position,Quaternion.identity); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        spawnedEnemies = spawnedEnemies.Where(x => x != null).ToList();
 
        if(waveTimer<=0 && spawnedEnemies.Count <=0)
        {
            currWave++;
            OnWaveChange.Invoke();
            if(!isActive){
                return;
            }
            GenerateWave();
        }
    }
 
    public void GenerateWave()
    {
        waveValue = (int)progression.CalculateWaveValue(currWave);
        Debug.Log(waveValue);
        GenerateEnemies();
        waveDuration = (int)(enemiesToSpawn.Count() * spawnInterval + additionalWaveTime);

        waveTimer = waveDuration; // wave duration is read only
        spawnTimer = spawnInterval;
    }
 
    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.
 
        // repeat... 
 
        //  -> if we have no points left, leave the loop
 
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue>0 || generatedEnemies.Count <50)
        {
            int randEnemyId = UnityEngine.Random.Range(0, enemies.Count);
            
            int randEnemyCost = enemies[randEnemyId].cost;
 
            if(waveValue-randEnemyCost>=0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue<=0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
  
}