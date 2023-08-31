using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;

    public void UpdatePercentage(float percentage)
    {
        mask.fillAmount = percentage;
    }
}
