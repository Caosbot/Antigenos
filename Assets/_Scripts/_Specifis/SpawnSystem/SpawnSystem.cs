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
    [SerializeField] private TextMeshProUGUI waveSpawnText;
    [SerializeField] private TextMeshProUGUI waveSpawnTimeText;
    [SerializeField] private TextMeshProUGUI spawnerLives;
    public static int spawnedEnemies;

    private AntigenQueue<_EnemyData> enemyQueue = new AntigenQueue<_EnemyData>(30);
    private int waveCounter = 0;
    public int serializeSpawnLifes = 15;
    public static int spawnLife = 0;
    public static int spawnLifes = 15;
    public static bool ended;

    private bool wavePaused;
    private bool waveShouldPause;

    private List<_Enemy_Behaviour> spawnedEnemiesList;

    private void Awake()
    {
        spawnLifes = serializeSpawnLifes;
        spawnLife = spawnLifes;
        spawnedEnemiesList = new List<_Enemy_Behaviour>(100);
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (ended)
        {
            ended = false;
            spawnLifes = serializeSpawnLifes;
            spawnLife = spawnLifes;
            StartCoroutine(DelayRestart());

        }
        spawnerLives.text = spawnLife.ToString();
    }
    private IEnumerator DelayRestart()
    {
        StartCoroutine(WaveTextTimer(50));
        yield return new WaitForSeconds(20);
        foreach(_Enemy_Behaviour e in spawnedEnemiesList)
        {
            if(e!= null)
            e.TakeDamage(10000);
        }
        yield return new WaitForSeconds(40);
        waveCounter = 0;
        spawnLifes = serializeSpawnLifes;
        spawnLife = spawnLifes;
        StartCoroutine(SpawnEnemyWaves());
    }
    private IEnumerator SpawnEnemyWaves()
    {
        StartCoroutine(WaveTextTimer(20));
        yield return new WaitForSeconds(20);
        QueueWave();
        while(waveCounter <= enemyWaves.Length)
        {
            yield return 0;
            _EnemyData[] tempEnemyData = enemyQueue.GetArray();
            foreach (_EnemyData e in tempEnemyData)
            {
                if(e != null)
                {
                    yield return new WaitForSeconds(e.spawnRate+Random.Range(0,0.7f));
                    SpawnEnemy(enemyQueue.GetFirstValue().prefabLocation);
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
#if UNITY_EDITOR
            Debug.Log("END GAME");
#endif
            if (infinite)
            {
                waveCounter = 0;
                StopAllCoroutines();
                StartCoroutine(DelayRestart());
                return;
            }
            waveCounter++;
            ENDGAME();
            return;
        }
        waveSpawnText.text = enemyWaves[waveCounter].wave_Name;
        StartCoroutine(WaveTextTimer(enemyWaves[waveCounter].waveSpawnRate));
        waveShouldPause = enemyWaves[waveCounter].shouldPauseOnEnded;
        foreach(EnemiesCount eCounter in enemyWaves[waveCounter].enemyList)
        {
            int tempInt = 0;
            while(tempInt < eCounter.spawnCount+PhotonNetwork.CountOfPlayersInRooms)
            {
                enemyQueue.Queue(eCounter.enemyData);
                tempInt++;
            }
        }
        //Debug.Log("A");
        waveCounter++;
    }
    private IEnumerator WaveTextTimer(float time)
    {
        waveSpawnTimeText.text = time.ToString();
        float currentTime = time;
        while (currentTime != 0)
        {
            yield return new WaitForSeconds(time/time);
            currentTime -= 1;
            waveSpawnTimeText.text = currentTime.ToString();
        }
        yield return new WaitForSeconds(0.2f);
        waveSpawnTimeText.text = "";
        waveSpawnText.text = "";
    }
    public static void ENDGAME()
    {   
    }
    private void SpawnEnemy(string location)
    {
        GameObject instance = PhotonNetwork.Instantiate(location, spawnLocations[Random.Range(0,spawnLocations.Length)].position, new Quaternion(0, 0, 0, 0));
        spawnedEnemiesList.Add(instance.GetComponent<_Enemy_Behaviour>());
    }
    public void StartSpawn()
    {
        {
#if UNITY_EDITOR
            //Debug.Log("Eu sou o host!!");
#endif
            StartCoroutine(SpawnEnemyWaves());
        }
    }
    public static void SpawnTakeDamage()
    {
        spawnLife--;
        if(spawnLife == 0)
        {
            Debug.Log("Missão Falhou");
            ended = true;
            foreach(_Enemy_Behaviour g in FindObjectsOfType<_Enemy_Behaviour>())
            {
                g.enemy_Animation.PlayDesiredAnimation("Dance");
            }
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
