using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Initialize_Component
{
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject[] objectsToShow;
    public void Initialize()
    {
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToShow)
        {
            g.SetActive(true);
        }
    }
}
