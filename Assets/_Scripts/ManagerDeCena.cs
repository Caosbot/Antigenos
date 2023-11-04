using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDeCena : MonoBehaviour
{
    public GameObject[] objectsToDestroy;
   void TrocarScene()
    {
        foreach(GameObject g in objectsToDestroy)
        {
            Destroy(g);
        }
        //Trocar cena
    }
}
