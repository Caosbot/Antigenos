using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float audioVolume = 1;
    public float sensibility = 1;
    [SerializeField] public Slider sliderS;
    [SerializeField] public Slider sliderSensibilidade;
    public GameObject g;
    public GameObject musica;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(musica);
        //DontDestroyOnLoad(g);
    }
    private void Update()
    {
        AudioListener.volume = audioVolume;
        
    }
    public void ChangeVolume()
    {
        Debug.Log("A");
        if(sliderS!=null)
        audioVolume = sliderS.value;
    }
    public void ChangeSensibilidade()
    {
        if(sliderSensibilidade != null)
        {
            Debug.Log("Mudando Sensibilidade");
            sensibility = sliderSensibilidade.value;
            CameraRotator_Component.generalMultiplier = sensibility;
        }
    }
}
