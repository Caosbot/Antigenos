using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string nickName = "Antigen";
    public string roomName = "PUCC";
    void Start()
    {
        nickName += Random.Range(0, 10);
        Debug.Log("Connecting to server...");
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("InLobby");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        Debug.Log("Entering room " + roomName);
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("On room " + PhotonNetwork.CurrentRoom.Name);
        base.OnJoinedRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered " + newPlayer.NickName);
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer.NickName + " left the room");
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
