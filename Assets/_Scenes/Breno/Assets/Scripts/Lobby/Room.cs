using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Room : MonoBehaviour
{
    public TextMeshProUGUI name;
    //public Text NameA;

    public void JoinRoom()
    {
        //GameManager.Debuger(name.text);
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoomInList(name.text);

    }

}
