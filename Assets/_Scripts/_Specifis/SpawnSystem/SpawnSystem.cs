using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class SpawnSystem : MonoBehaviourPunCallbacks
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

    private AntigenQueue<_EnemyData> enemyQueue = new AntigenQueue<_EnemyData>(30);
    private int waveCounter = 0;
    public int serializeSpawnLifes = 15;
    public static int spawnLife = 0;
    public static int spawnLifes = 15;

    private bool wavePaused;
    private bool waveShouldPause;

    private void Awake()
    {
        spawnLifes = serializeSpawnLifes;
        spawnLife = spawnLifes;
    }
    private void Start()
    {
    }
    private IEnumerator SpawnEnemyWaves()
    {
        QueueWave();
        while(waveCounter <= enemyWaves.Length)
        {
            yield return 0;
            _EnemyData[] tempEnemyData = enemyQueue.GetArray();
            foreach (_EnemyData e in tempEnemyData)
            {
                if(e != null)
                {
                    yield return new WaitForSeconds(e.spawnRate);
                    SpawnEnemy(enemyQueue.GetFirstValue().prefabLocation);
                    Debug.Log(enemyQueue.GetFirstValue()); //Substituir por l�gica de spawn
                    enemyQueue.Unqueu();
                }
            }
            if (waveShouldPause) wavePaused = true;
            while (wavePaused)
            {
                yield return 0;
            }
            QueueWave();
        }
    }
    private void QueueWave()
    {
        if(waveCounter >= enemyWaves.Length)
        {
            Debug.Log("END GAME");
            ENDGAME();
            waveCounter++;
            return;
        }
        waveShouldPause = enemyWaves[waveCounter].shouldPauseOnEnded;
        foreach(EnemiesCount eCounter in enemyWaves[waveCounter].enemyList)
        {
            int tempInt = 0;
            while(tempInt < eCounter.spawnCount)
            {
                enemyQueue.Queue(eCounter.enemyData);
                tempInt++;
            }
        }
        waveCounter++;
    }
    public static void ENDGAME()
    {
        
    }
    private void SpawnEnemy(string location)
    {
        GameObject instance = PhotonNetwork.Instantiate(location, spawnLocations[0].position, new Quaternion(0, 0, 0, 0));
    }
    public void StartSpawn()
    {
        {
            Debug.Log("Eu sou o host!!");
            StartCoroutine(SpawnEnemyWaves());
        }
    }
    public static void SpawnTakeDamage()
    {
        spawnLife--;
        if(spawnLife == 0)
        {
#if UNITY_EDITOR
            Debug.Log("Miss�o Falhou");
#endif
        }
    }
}

[System.Serializable]
public class EnemiesCount
{
    public _EnemyData enemyData;
    public int spawnCount = 1;
}

[System.Serializable]
public class EnemySpawnWave
{
    public string wave_Name;
    public float waveSpawnRate = 10f;
    public bool shouldPauseOnEnded = false;
    public EnemiesCount[] enemyList;
}

[System.Serializable]
public class Test
{
    public string aaaa;
}
