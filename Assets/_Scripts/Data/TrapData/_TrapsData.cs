using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrapsData", menuName = "_TrapsData", order = 1)]
public class _TrapsData: ScriptableObject
{
    public int numRef;
    public string prefabLocation;
    public string trapsPlace;

}