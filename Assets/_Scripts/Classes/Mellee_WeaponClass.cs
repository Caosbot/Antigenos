using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

[System.Serializable]
public class Mellee_weapon
{
    public float damage;
    public GameObject weaponPrefab;
    [SerializeField] private Transform weaponNozzle;
    [SerializeField] private float shootingInterval;
    [SerializeField] private WeaponType typeOfWeapon;

}
