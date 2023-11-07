using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEditor.XR;

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
        // roomOptions = new RoomOptions();
        // roomOptions.MaxPlayers = 3;
         PhotonNetwork.JoinOrCreateRoom(input_Create.text.ToUpper(),roomOptions,null);
        //PhotonNetwork.CreateRoom(roomName);
        GameManager.Debuger("Criado sala em CreateRoom: "+ roomName);


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
}
