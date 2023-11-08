using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEditor.XR;
using UnityEngine.SceneManagement;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_Create;
    public TMP_InputField input_Join;

    [SerializeField] private string[] playerNames;
    [SerializeField] private string playerNickname;
    private GameObject playerObject;
    public SpawnSystem spawnSystem;
    public RoomOptions roomOptions;

    public string roomName = string.Empty;

    void Start()
    {
        //Conecta no photon
        spawnSystem = GetComponent<SpawnSystem>();
        GameManager.Debuger("Conectando no Servidor...");
        if (string.IsNullOrEmpty(playerNickname))
        {
            playerNickname = System.Environment.UserName;
        }
        PhotonNetwork.ConnectUsingSettings();//
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        GameManager.Debuger("Entrou no Lobby!");

    }
    public void CreateRoom()
    {
        if (input_Create.text == string.Empty)
            roomName = "CORACAO";
        else
            roomName = input_Create.text.ToUpper();

        GameManager.Debuger("Criado sala em CreateRoom: " + roomName);
        //PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
        // roomOptions = new RoomOptions();
        // roomOptions.MaxPlayers = 3;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions,null);
        //PhotonNetwork.CreateRoom(roomName);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);

    }
    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }
    public void OnJoinRoom()
    {
        //PhotonNetwork.LoadLevel("GamePlay");
    }
    public void LoadScene()
    {
        string sceneName = "1.0_Phase";
        SceneManager.LoadScene(sceneName);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameManager.Debuger("Player entrou na sala " + newPlayer.NickName);
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {

            GameManager.Debuger("Eu sou o host!!");
            //waveSpawnText.text = "Pressione G para Começar\n Pressione X para Sair"; --------------------------------------------------
        }
        else
        {
            spawnSystem.enabledS = false;
            GameManager.Debuger("Não sou o Host da sala!!");
        }

        Vector3 position = new Vector3(0, 0.79f, 0);//79f
        Quaternion rotation = Quaternion.Euler(0, 90, 0);//Vector3.up * Random.Range(0, 360.0f));
        GameObject playerObject = PhotonNetwork.Instantiate("PlayerCharacter", position, rotation);
        DontDestroyOnLoad(playerObject);
        //playerObject.GetComponent<PhotonView>().TransferOwnership(newPlayer);
        LoadScene();
    }
}
