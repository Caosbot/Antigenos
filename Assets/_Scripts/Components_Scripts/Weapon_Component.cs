using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Component : MonoBehaviour
{
    public WeaponClass weapon = null;
    [System.NonSerialized]public Aim_Component aimComponent;
    private bool canShoot = true;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject impact;
    public string GetWeaponLocation()
    {
        return weapon.weaponData.prefabLocation;
    }
    public void Shot()
    {
        if(weapon != null && canShoot)
        {
            StartCoroutine(Particles());
            Debug.Log("Shoting");
            GameObject g = aimComponent.hitTransform.gameObject;
            if (g != null)
            {
                Debug.Log("Hit Something");
                IDamageable iDamage = InterfaceHelper.GetDamageable(g);
                if (iDamage != null)
                {
                    Debug.Log("Hit Damageable");
                    iDamage.TakeDamage((int)weapon.damage);
                }
            }
            canShoot = false;
            StartCoroutine(ShootTimer());
        }
    }
    private IEnumerator Particles()
    {
        muzzle.SetActive(true);
        RaycastHit hit = aimComponent.hitR;
        Instantiate(impact, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        yield return new WaitForSeconds(0.2f);
        muzzle.SetActive(false);
    }
    private IEnumerator ShootTimer()
    {
        float time = 0.1f;
        if(weapon is Ranged_WeaponClass)
        {
            Ranged_WeaponClass classWeapon = weapon as Ranged_WeaponClass;
            time = classWeapon.shootingInterval;
        }
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
