using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class GameConnection : MonoBehaviourPunCallbacks
{
    [SerializeField] private string roomName = "PUCC";
    [SerializeField] private string[] playerNames;
    private GameObject playerObject;
    void Start()
    {
        //Conecta no photon
        Debug.Log("Conectando no Servidor...");
        PhotonNetwork.NickName = playerNames[Random.Range(0,playerNames.Length-1)] + Random.Range(0, 10);
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
        Vector3 position = new Vector3(0,0.79f,0);
        Quaternion rotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360.0f));
        playerObject = PhotonNetwork.Instantiate("PlayerCharacter", position, rotation);
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
        playerObject.GetComponent<_Character_Behaviour>().DestroyInstantedObjects();
    }
}
