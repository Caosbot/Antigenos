using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room : MonoBehaviour
{
    public TextMeshProUGUI name;
    //public Text Name;

    public void JoinRoom()
    {
        GameManager.Debuger(name.text);
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoomInList(name.text);
    }

}
