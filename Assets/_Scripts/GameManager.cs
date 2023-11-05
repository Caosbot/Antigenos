using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float multiplicadorDeSensibilidade;
    [SerializeField]
    public static bool bDebugerOnOFF=true;
    public SpawnSystem spawnSystem;
    public static void Debuger(string texto)
    {
        if (bDebugerOnOFF)
        { 
#if UNITY_EDITOR

        Debug.Log(texto);
#endif
        }
    }
    public void Start()
    {
        multiplicadorDeSensibilidade = 0.5f;
    }

}
