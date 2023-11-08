using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    public GameObject[] allRooms;
    public static List<string> roomNames = new List<string>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0;i < allRooms.Length; i++)
        {
            if (allRooms[i] != null)
            {
                GameManager.Debuger("Sem sala!");
                Destroy(allRooms[i]);
            }
        }

        allRooms=new GameObject[roomList.Count];

        for (int i = 0;i< roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount>=1)
            {
                //GameManager.Debuger("RoomList");
                GameManager.Debuger("RoomList: "+roomList[i].Name);
                roomNames.Add(roomList[i].Name);
                GameObject Room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                Room.GetComponent<RoomT>().nameT.text = roomList[i].Name;

                allRooms[i] = Room;
            }
           /* GameObject gameItem = Instantiate<GameObject>(_itemPrefab, _itemContent);
            Button button = gameItem.GetComponent<Button>();
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = objs[i].referen;*/
        }
        
    }
}
