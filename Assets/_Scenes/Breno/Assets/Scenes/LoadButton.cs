using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{
    public string roomName = "1.0_Phase";
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(roomName);
    }
}
