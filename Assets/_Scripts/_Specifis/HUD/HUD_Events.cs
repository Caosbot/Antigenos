using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Events : MonoBehaviour
{
    private string roomName;
    private string playerName = "Antigen";
    private GameConnection gameConnection;
    private void Awake()
    {
        gameConnection = FindObjectOfType<GameConnection>();
    }
    public void ChangePlayerName(string name)
    {
        playerName = name;
    }
    public void ChangeRoomName(string name)
    {
        roomName = name;
    }
    public void UpdatePlayerList()
    {
        
    }
}
