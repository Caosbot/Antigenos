using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool bDebugerOnOFF;
    public void Debuger(string texto)
    {
#if UNITY_EDITOR

        Debug.Log(texto);
#endif
    }
    public void Grupo()
    {

    }
}
