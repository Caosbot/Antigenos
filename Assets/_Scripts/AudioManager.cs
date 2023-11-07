using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float audioVolume = 1;
    [SerializeField] private Slider sliderS;
    public GameObject g;
    void Start()
    {
        //DontDestroyOnLoad(g);
    }
    private void Update()
    {
        AudioListener.volume = audioVolume;
        
    }
    public void ChangeVolume()
    {
        if(sliderS!=null)
        audioVolume = sliderS.value;
    }
}
