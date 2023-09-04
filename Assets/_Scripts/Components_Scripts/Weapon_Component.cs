using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon_Component
{
    public WeaponClass weapon;
    public Transform handTransform;
    private GameObject weaponMesh;
    public string GetWeaponLocation()
    {
        return weapon.weaponData.prefabLocation;
    }
    public void Shot()
    {

    }
    public void SpawnParticle()
    {

    }
}
