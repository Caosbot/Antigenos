using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float multiplicadorDeSensibilidade;
    public bool bDebugerOnOFF;
    public static void Debuger(string texto)
    {
#if UNITY_EDITOR

        Debug.Log(texto);
#endif
    }
    public void Start()
    {
        multiplicadorDeSensibilidade = 0.5f;
    }
    public void Grupo()
    {

    }
}
