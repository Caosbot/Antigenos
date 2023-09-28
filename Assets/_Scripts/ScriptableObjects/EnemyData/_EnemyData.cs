using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "EnemyData", menuName = "_EnemyData", order = 1)]
public class _EnemyData : ScriptableObject
{
    [Range(0.2f, 5)]
    public float spawnRate = 0.5f;
    public string prefabLocation;
}
