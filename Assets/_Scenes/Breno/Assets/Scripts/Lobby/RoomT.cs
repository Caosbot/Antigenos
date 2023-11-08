using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class RoomT : MonoBehaviour
{
    public TextMeshProUGUI nameT;
    //public Text Name;

    public void JoinRoom()
    {
        //GameManager.Debuger(name.text);
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoomInList(nameT.text);

    }

}
