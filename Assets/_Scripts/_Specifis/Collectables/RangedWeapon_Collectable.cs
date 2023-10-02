using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon_Collectable : MonoBehaviour
{
    [SerializeField] private Ranged_WeaponClass weaponClass;
    [SerializeField] private float rotationSpeed = 1;
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.transform.gameObject.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<_Character_Behaviour>().SpawnWeapon(weaponClass.weaponData.prefabLocation);
            other.transform.gameObject.GetComponent<_Character_Behaviour>().weaponComponent.weapon = weaponClass;
            Destroy(gameObject);
        }*/
    }
    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
