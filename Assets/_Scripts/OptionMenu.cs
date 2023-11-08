using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public void LeaveMenu()
    {
        Movement_Component.onMenu = false;
    }
}
