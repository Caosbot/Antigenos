using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy_Animation
{
    public Animator animInstance;
    public void PlayDesiredAnimation(string animationName)
    {
        if(animInstance != null)
        animInstance.Play(animationName, 0);
    }
}
