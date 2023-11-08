using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FINDEROutros : MonoBehaviour
{
    public Slider sS1;
    public Slider sA2;
    public void Start()
    {
        sS1.value = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().sensibility;
        sA2.value = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().audioVolume;
    }
    public void FindAndAtualizarAudio(Slider s)
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().sliderS = sA2;
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().ChangeVolume();
    }
    public void FindAndAtualizarSensibilidade(Slider s)
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().sliderSensibilidade = sS1;
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().ChangeSensibilidade();
    }
}
