using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class GameConnection : MonoBehaviourPunCallbacks
{
    [SerializeField] private string roomName = "PUCC";
    [SerializeField] private string[] playerNames;
    private GameObject playerObject;
    public SpawnSystem spawnSystem;
    void Start()
    {
        //Conecta no photon
        spawnSystem = GetComponent<SpawnSystem>();
#if UNITY_EDITOR
        //Debug.Log("Conectando no Servidor...");
#endif
        PhotonNetwork.NickName = playerNames[Random.Range(0,playerNames.Length-1)] + Random.Range(0, 10);
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
#if UNITY_EDITOR
        //Debug.Log("Conectado no Servidor!");
#endif
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
#if UNITY_EDITOR
       // Debug.Log("Entrou no Lobby!");
#endif
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers= 20;
#if UNITY_EDITOR
        //Debug.Log("Entrando na sala "+ roomName);
#endif
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
    }
    public override void OnJoinedRoom()
    {
#if UNITY_EDITOR
        //Debug.Log("Entrei na sala " + PhotonNetwork.CurrentRoom.Name);
#endif
        base.OnJoinedRoom();
        Vector3 position = new Vector3(0, 0.79f, 0);//79f
        Quaternion rotation = Quaternion.Euler(0, 90, 0);//Vector3.up * Random.Range(0, 360.0f));
        playerObject = PhotonNetwork.Instantiate("PlayerCharacter", position, rotation);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
#if UNITY_EDITOR
            //Debug.Log("Eu sou o host!!");
#endif
            spawnSystem.StartSpawn();
        }
        else
        {
            spawnSystem.enabledS = false;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
#if UNITY_EDITOR
        Debug.Log("Player entrou na sala " + newPlayer.NickName);
#endif
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
#if UNITY_EDITOR
        Debug.Log("Player saiu sala " + otherPlayer.NickName);
#endif
        base.OnPlayerLeftRoom(otherPlayer);
        playerObject.GetComponent<_Character_Behaviour>().DestroyInstantedObjects();
    }
}
