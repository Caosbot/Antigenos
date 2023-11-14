using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Linq;
using Photon.Pun.Demo.SlotRacer.Utils;
using UnityEngine.SocialPlatforms;
using static UnityEngine.UI.GridLayoutGroup;
using Photon.Pun.Demo.PunBasics;

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
    private int startTime = 5;
    private string tempText = string.Empty;

    private Queue<_EnemyData> enemyQueue = new Queue<_EnemyData>(30);
    private int waveCounter = 0;
    public int serializeSpawnLifes = 15;
    public static int spawnLife = 0;
    public static int spawnLifes = 15;///____________________________
    public static bool ended;
    public bool begin = false;

    private bool wavePaused;
    private bool waveShouldPause;
    private bool endText;
    

    private List<_Enemy_Behaviour> spawnedEnemiesList;

    public static GameObject[][] enemyGroup;
    public static int numPlayers = PhotonNetwork.PlayerList.Length;
    public static int maxColluna=5;
    private  int sizeArray=10;
    public int linha;

    PhotonView myPhotonView;


    public static bool canSpawn;
    public bool enabledS = true;

    private void Awake()
    {
        waveSpawnText = GameObject.Find("Waves").GetComponent<TextMeshProUGUI>();
        waveSpawnTimeText=GameObject.Find("WaveTime").GetComponent<TextMeshProUGUI>();
        spawnerLives=GameObject.Find("Life").GetComponent<TextMeshProUGUI>();
        //gameObject.AddComponent<PhotonView>();
        myPhotonView =  GetComponent<PhotonView>();
        canSpawn = true;
        spawnLifes = serializeSpawnLifes;
        spawnLife = spawnLifes;
        spawnedEnemiesList = new List<_Enemy_Behaviour>(100);
        linha = spawnLocations.Length + 1;
        //Debug.Log("Linha: " + linha);
        enemyGroup =new GameObject[linha][];
        for (int i = 0;i<linha;i++)
        {
            //Debug.Log("i: " + i);
            enemyGroup[i] = new GameObject[sizeArray];
            //Debug.Log("enemyGroup[i]"+ enemyGroup[i].Length);
        }
            
    }
    private void AlternateAwake()
    {
        waveSpawnText = GameObject.Find("Waves").GetComponent<TextMeshProUGUI>();
        waveSpawnTimeText = GameObject.Find("WaveTime").GetComponent<TextMeshProUGUI>();
        spawnerLives = GameObject.Find("Life").GetComponent<TextMeshProUGUI>();
        //gameObject.AddComponent<PhotonView>();
        //myPhotonView =  GetComponent<PhotonView>();
        canSpawn = true;
        spawnLifes = serializeSpawnLifes;
        spawnLife = spawnLifes;
        spawnedEnemiesList = new List<_Enemy_Behaviour>(100);
        linha = spawnLocations.Length + 1;
        //Debug.Log("Linha: " + linha);
        enemyGroup = new GameObject[linha][];
        for (int i = 0; i < linha; i++)
        {
            //Debug.Log("i: " + i);
            enemyGroup[i] = new GameObject[sizeArray];
            //Debug.Log("enemyGroup[i]"+ enemyGroup[i].Length);
        }
    }
    private void Start()
    {
        canSpawn = true;
        ended = false;
        EnemyNavMesh.fim = false;
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if(waveSpawnText != null)
                waveSpawnText.text = "Press G To Start\n Press X to Quit";
        }
    }
    private void AlternateStart()
    {
        canSpawn = true;
        ended = false;
        EnemyNavMesh.fim = false;
        GameManager.Debuger("Fim: "+ EnemyNavMesh.fim);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (waveSpawnText != null)
                waveSpawnText.text = "Press G To Start\n Press X to Quit";
        }
    }
    private void Update()
    {
        //SendMyMessage(spawnLife.ToString());
        if(spawnerLives != null)
        spawnerLives.text = spawnLife.ToString();
        //numPlayers = 3;
        //GameManager.Debuger("Begin: "+ begin);
        if (!begin)
        {
            ToMaster();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            Application.Quit();
        }
        if (numPlayers != PhotonNetwork.PlayerList.Length)
        {
            numPlayers = PhotonNetwork.PlayerList.Length;
            GameManager.Debuger("Qtd de Players: " + PhotonNetwork.PlayerList.Length);
        }


    }
    private void Ended()
    {
        if (ended)
        {
            //ended = false;
            spawnLifes = serializeSpawnLifes;
            spawnLife = spawnLifes;
            StartCoroutine(DelayRestart());
        }
    }
    private IEnumerator DelayRestart()
    {
        
        StartCoroutine(WaveTextTimer(60));
        yield return new WaitForSeconds(20);
        foreach(_Enemy_Behaviour e in spawnedEnemiesList)
        {
            if(e!= null)
            e.TakeDamage(10000);
        }
        
        yield return new WaitForSeconds(40);
        ended = false;
        waveCounter = 0;
        spawnLifes = serializeSpawnLifes;
        spawnLife = spawnLifes;
        StartCoroutine(SpawnEnemyWaves());
    }
    private IEnumerator SpawnEnemyWaves()
    {
        if (ended)
        {
            ended = false;
        }
        while (!enabledS)
        {
            yield return new WaitForSeconds(10);
        }
        ended = false;
        StartCoroutine(TextTimer(startTime));
        yield return new WaitForSeconds(startTime);
        QueueWave();
        /////////////////////////////
        
        while(waveCounter <= enemyWaves.Length || canSpawn)
        {
            yield return 0;
            _EnemyData[] tempEnemyData = enemyQueue.ToArray();
            foreach (_EnemyData e in tempEnemyData)
            {
                if(e != null)
                {
                    yield return new WaitForSeconds(e.spawnRate+Random.Range(0,0.7f));
                    if (canSpawn)
                    {
                        SpawnEnemy(enemyQueue.Dequeue().prefabLocation);
                        spawnedEnemies++;
                    }
                }
            }
            if (waveShouldPause) wavePaused = true;
            while (wavePaused)
            {
                yield return 0;
            }
            GameManager.Debuger("Esperando os inimigos morrerem");
            while(spawnedEnemies != 0)
            {
                yield return 0;
            }
            GameManager.Debuger("Inimigos morre�o, pr�xima wave vindo");
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

            waveSpawnText.text = "You Win this Battle... More Enemies are coming soom!";
            StartCoroutine(Menssagem());
            begin = false;
            if (infinite)
            {
                waveCounter = 0;
                //StopAllCoroutines();
                //StartCoroutine(DelayRestart());
                //return;
            }
            waveCounter++;
            ENDGAME();
            return;
        }
        tempText= enemyWaves[waveCounter].wave_Name;
        SendMyMessageToAll(tempText);
        waveSpawnText.text = tempText;//////////
        StartCoroutine(WaveTextTimer(enemyWaves[waveCounter].waveSpawnRate));
        waveShouldPause = enemyWaves[waveCounter].shouldPauseOnEnded;
        foreach(EnemiesCount eCounter in enemyWaves[waveCounter].enemyList)
        {
            int tempInt = 0;
            while(tempInt < eCounter.spawnCount+ PhotonNetwork.PlayerList.Length)
            {
                enemyQueue.Enqueue(eCounter.enemyData);
                BiggerArray();
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
            waveSpawnText.text = tempText;
        }
        yield return new WaitForSeconds(0.2f);
        waveSpawnTimeText.text = "";
        waveSpawnText.text = "";
    }
    private IEnumerator TextTimer(float time)
    {
        waveSpawnTimeText.text = time.ToString();
        float currentTime = time;
        while (currentTime != 0)
        {
            yield return new WaitForSeconds(time / time);
            currentTime -= 1;
            waveSpawnTimeText.text = currentTime.ToString();
            waveSpawnText.text = tempText;
        }
        yield return new WaitForSeconds(0.2f);
        waveSpawnTimeText.text = "";
    }
    public static void ENDGAME()
    {
        
        foreach(SpawnSystem s in FindObjectsOfType<SpawnSystem>())
        {
            s.RestartSpawn();
        }
            //ended = false;
    }
    public void RestartSpawn()
    {
        StopAllCoroutines();
        waveCounter = 0;
        begin = false;
        tempText = string.Empty;
        spawnLife = 0;
        spawnLifes = 15;
        ended = true;
        wavePaused = false;
        waveShouldPause = false;
        endText = false;
        canSpawn = false;
        enabledS = true;
        spawnedEnemies = 0;
        EnemyNavMesh.fim = false;
        Awake();
        Start();
        
        /*(//ended == false)
        {
            enemyQueue.Clear();
            waveCounter = 0;
            StopAllCoroutines();
            ended = true;
            spawnLifes = serializeSpawnLifes;
            spawnLife = spawnLifes;
            StartCoroutine(DelayRestart());
        }*/
    }
    private void SpawnEnemy(string location)
    {
        int rand = Random.Range(0, spawnLocations.Length);
        GameObject instance = PhotonNetwork.Instantiate(location, spawnLocations[rand].position, new Quaternion(0, 0, 0, 0));
        Group(instance, rand);
        spawnedEnemiesList.Add(instance.GetComponent<_Enemy_Behaviour>());
        
    }
    public void StartSpawn()
    {
        spawnLife = 15;
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
        if (spawnLife == 0)
        {
            ENDGAME();
            EnemyNavMesh.fim = true;
            foreach(_Enemy_Behaviour g in FindObjectsOfType<_Enemy_Behaviour>())
            {
                g.enemy_Animation.PlayDesiredAnimation("Dance");
            }
        }
    }
    [PunRPC]
    public void SpawnTakeDamageRPC()
    {

    }
    public void Group(GameObject atual,int local)
    {
        //float friendDistance = 5f;
        atual.GetComponent<_Enemy_Behaviour>().linha=local;
        //GameManager.Debuger("Grupo: " + local);
        //for (int i=0;i<enemyGroup[local].Length;i++)
        for (int i=0;i<maxColluna;i++)
        {
            if (enemyGroup[local][i] == null)
            {
                enemyGroup[local][i] = atual;
                atual.GetComponent<_Enemy_Behaviour>().coluna = i;
                //Debug.Log("Grupo: "+local+" Posi��o: "+i);
                return;
            }
        }
        if (enemyGroup[local].Length>=maxColluna)
        {
            //GameManager.Debuger("enemyGroup[local].Length: " + enemyGroup[local].Length+ "| maxColluna: "+ maxColluna);
            waveSpawnText.text = "The system is under attack!";
            if (endText)
            {
                endText = false;
                StartCoroutine(Menssagem());
            }
                
            //MesmaSpeed(local);
        }
    }
    public void MesmaSpeed(int local)
    {
        //GameManager.Debuger("MesmaSpeed");
        float max = 0;
        float min = 10;
        foreach (GameObject enemy in enemyGroup[local])
        {

            if (enemy.GetComponent<_Enemy_Behaviour>().speed>max)
            {
                max=enemy.GetComponent<_Enemy_Behaviour>().speed;
            }
            if (enemy.GetComponent<_Enemy_Behaviour>().speed < min)
            {
                min=enemy.GetComponent<_Enemy_Behaviour>().speed;
            }
        }
        foreach (GameObject enemy in enemyGroup[local])
        {
            enemy.GetComponent<_Enemy_Behaviour>().speed = max + min/2;
        }
    }
    public void BiggerArray()
    {
        if (numPlayers <= 1)
            maxColluna = 5;
        if (numPlayers == 2)
            maxColluna = 6;
        if (numPlayers >= 3)
            maxColluna = 8;
    }
    private IEnumerator Menssagem()
    {
        yield return new  WaitForSeconds(10);
        waveSpawnText.text = "";
        endText = true;
    }
    public void SendMyMessage(string message)
    {
        myPhotonView.RPC(nameof(ChangeText), RpcTarget.All, message);
    }
    [PunRPC]
    void ChangeText(string texto, PhotonMessageInfo info)
    {
        GameManager.Debuger("Entrou2");
        spawnerLives.text = texto;
    }
    [PunRPC]
    public void TextWaveSpawnText(string texto, PhotonMessageInfo info)
    {
        GameManager.Debuger("Entrou3");
        waveSpawnText.text=texto;
    }
    public void SendMyMessageToAll(string message)
    {
        if (myPhotonView != null)
            myPhotonView.RPC(nameof(TextWaveSpawnText), RpcTarget.All, message);
        else
            GameManager.Debuger("Vazio");
    }
    public void ToMaster()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.G) && begin == false)
            {
                StartSpawn();
                begin = true;
                EnemyNavMesh.fim=false;
                GameManager.Debuger("Fim: " + EnemyNavMesh.fim);
                waveSpawnText.text = "";
                foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (e != null)
                        e.GetComponent<_Enemy_Behaviour>().TakeDamage(10000);
                }
            }
            //.Owner
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
