using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "EnemyData", menuName = "_EnemyData", order = 1)]
public class _EnemyData : ScriptableObject
{
    public string prefabLocation;
}

[System.Serializable]
public class EnemySpawnData
{
    public _EnemyData prefabData;
    public int quantityToSpawn;
    public string GetSpawnLocation(bool removeFromQueue = true)
    {
        if (removeFromQueue)
        {
            quantityToSpawn--;
        }
        return prefabData.prefabLocation;
    }
}
