using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSystem : MonoBehaviour
{
    [Header("Enemy Wave List")]
    [SerializeField] private int maxEnemiesInScene = 16;
    [SerializeField] private EnemySpawnWave[] enemyWaves;
    public bool infinite = false;
    [Header("Enemy Spawn Locations")]
    [SerializeField] private Transform[] spawnLocations;

    [Header("Text")]
    [SerializeField] private GameObject waveSpawnText;
    public static int spawnedEnemies;

    private AntigenQueue<_EnemyData> enemyQueue = new AntigenQueue<_EnemyData>(21);
    private int waveCounter = 0;

    private void OnValidate()
    {
        foreach(EnemySpawnWave e in enemyWaves)
        {
            foreach(EnemyWave a in e.enemyList)
            {
                if(a.spawnCount <= 0)
                {
                    a.spawnCount = 1;
                }
            }
        }
        if(waveSpawnText != null)
        {
            if (waveSpawnText.GetComponent<TextMeshProUGUI>() == null)
            {
                waveSpawnText = null;
            }
        }
    }
    private void Awake()
    {
        QueueWave();
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }
    private IEnumerator SpawnEnemyWaves()
    {
        while (!enemyQueue.IsEmpty())
        {
            yield return new WaitForSeconds(enemyQueue.GetFirstValue().spawnRate);
            //Debug.Log(enemyQueue.GetFirstValue().name);
            Debug.Log("A");
            //Instantiate Enemy
            SpawnSystem.spawnedEnemies++;
            enemyQueue.Unqueu();
            /*if(enemyQueue.IsEmpty() || enemyQueue.GetFirstValue()==null)
            {
                yield return new WaitForSeconds(enemyWaves[waveCounter].waveSpawnRate);
                QueueWave();
                Debug.Log("Fim da wave");
            }*/
        }
        Debug.Log("FimDeJogo");
        yield return 0;
    }
    private void QueueWave()
    {
        if(waveCounter+1 > enemyWaves.Length)
        {
            if (infinite)
            {
                waveCounter = 0;
            }
            else
            {
                return;
            }
        }
        foreach (EnemyWave a in enemyWaves[waveCounter].enemyList)
        {
            int tempInt = 0;
            while (tempInt < a.spawnCount)
            {
                Debug.Log(tempInt);
                enemyQueue.Queue(a.enemyData);
                tempInt++;
            }
        }
        waveCounter++;
        enemyQueue.DebugQueue();
    }
}

[System.Serializable]
public class EnemyWave
{
    public _EnemyData enemyData;
    public int spawnCount = 1;
}

[System.Serializable]
public class EnemySpawnWave
{
    public string wave_Name;
    public float waveSpawnRate = 10f;
    public EnemyWave[] enemyList;
}

[System.Serializable]
public class Test
{
    public string aaaa;
}
