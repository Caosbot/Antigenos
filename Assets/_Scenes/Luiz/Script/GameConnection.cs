using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class GameConnection : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public string roomName = "PUCC";
    void Start()
    {
        //Conecta no photon
        Debug.Log("Conectando no Servidor...");
        PhotonNetwork.NickName = "Xarope " + Random.Range(0, 1000);
        PhotonNetwork.ConnectUsingSettings();


    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Conectado no Servidor!");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Entrou no Lobby!");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers= 20;

        Debug.Log("Entrando na sala "+ roomName);
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Entrei na sala " + PhotonNetwork.CurrentRoom.Name);
        base.OnJoinedRoom();

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entrou na sala " + newPlayer.NickName);
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player saiu sala " + otherPlayer.NickName);
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
