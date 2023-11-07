using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;

public class GameConnection : MonoBehaviourPunCallbacks
{
    [SerializeField] private string roomName = "PUCC";
    [SerializeField] private string[] playerNames;
    [SerializeField] private string playerNickname;
    private GameObject playerObject;
    public SpawnSystem spawnSystem;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI waveSpawnText;
    [SerializeField] private TextMeshProUGUI waveSpawnTimeText;
    [SerializeField] private TextMeshProUGUI spawnerLives;
    void Start()
    {
        //Conecta no photon
        spawnSystem = GetComponent<SpawnSystem>();
#if UNITY_EDITOR
        //Debug.Log("Conectando no Servidor...");
#endif
        if (string.IsNullOrEmpty(playerNickname))
        {
            playerNickname = System.Environment.UserName;
        }
        //PhotonNetwork.NickName = playerNames[Random.Range(0,playerNames.Length-1)] + Random.Range(0, 10);
        PhotonNetwork.ConnectUsingSettings();

        //waveSpawnText = GameObject.Find("Waves").GetComponent<TextMeshProUGUI>();
        //waveSpawnTimeText=GameObject.Find("WaveTime").GetComponent<TextMeshProUGUI>();
        //spawnerLives=GameObject.Find("Life").GetComponent<TextMeshProUGUI>();

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
#if UNITY_EDITOR
        // Debug.Log("Entrou no Lobby!");
#endif
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3; 
        //if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null); 
        }
        //else
        
        GameManager.Debuger("Qtd de salas no Lobby: " + PhotonNetwork.CountOfRooms);
    }
    public override void OnJoinedRoom()
    {
        GameManager.Debuger("Entrei na sala " + PhotonNetwork.CurrentRoom.Name);
        base.OnJoinedRoom();
        GameManager.Debuger("Qtd Player na sala " + PhotonNetwork.PlayerList.Length);
        Vector3 position = new Vector3(0, 0.79f, 0);//79f
        Quaternion rotation = Quaternion.Euler(0, 90, 0);//Vector3.up * Random.Range(0, 360.0f));
        playerObject = PhotonNetwork.Instantiate("PlayerCharacter", position, rotation);
        SpawnSystem.numPlayers++;
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {

            GameManager.Debuger("Eu sou o host!!");
            waveSpawnText.text = "Pressione G para Começar\n Pressione X para Sair";
        }
        else
        {
            spawnSystem.enabledS = false;
            GameManager.Debuger("Não sou o Host da sala!!");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameManager.Debuger("Player entrou na sala " + newPlayer.NickName);
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameManager.Debuger("Player saiu sala " + otherPlayer.NickName);
        base.OnPlayerLeftRoom(otherPlayer);
        playerObject.GetComponent<_Character_Behaviour>().DestroyInstantedObjects();
        //SpawnSystem.numPlayers--;
        base.OnLeftLobby();
        PhotonNetwork.LeaveLobby();
    }
    public void TakeServerName(string server)
    {
        roomName = server.ToUpper();
    }
    public void TakeNickName(string nickName)
    {
        playerNickname = nickName;
    }
   
}
