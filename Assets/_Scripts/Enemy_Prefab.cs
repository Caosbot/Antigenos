using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy_Prefab
{
    [SerializeField] private Enemies EnemyPrefab;
    public string GetLoadLocation()
    {
        return "Assets/Resources/EnemyPrefabs/" + EnemyPrefab.ToString();
    }
}

public enum Enemies
{
    BacteriaLight,
}
