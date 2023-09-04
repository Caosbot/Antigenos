using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

[System.Serializable]
public class WeaponClass
{
    [Header("Attributes")]
    public float damage;
    [SerializeField] protected WeaponType typeOfWeapon;
    public _WeaponData weaponData;
}

[System.Serializable]
public class Mellee_WeaponClass : WeaponClass
{
    [Header("Bases")]
    public Transform weaponBlade;
}

[System.Serializable]
public class Ranged_WeaponClass : WeaponClass
{
    [Header("Bases")]
    [SerializeField] private Transform weaponNozzle;

    [Header("Attributes")]
    [SerializeField] private float shootingInterval;
}
