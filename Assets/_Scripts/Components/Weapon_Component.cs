using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Weapon_Component : MonoBehaviourPunCallbacks
{
    public WeaponClass weapon = null;
    [System.NonSerialized] public Aim_Component                         aimComponent;
    [System.NonSerialized] public AnimationState_Component              animComponent;
    private bool canShoot = true;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject impact;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public string GetWeaponLocation()
    {
        return weapon.weaponData.prefabLocation;
    }
    public void Shot(GameObject owner)
    {
        if(weapon != null && canShoot)
        {
            StartCoroutine(Particles());
            Debug.Log("Shoting");
            PlayAudio(weapon.weaponData.shootingSound);
            animComponent.ShootingAnim();
            GameObject g = aimComponent.hitTransform.gameObject;
            if (g != null)
            {
                Debug.Log("Hit Something");
                IDamageable iDamage = InterfaceHelper.GetDamageable(g);
                if(owner == g)
                {
                    Debug.Log("Hit self");
                    canShoot = false;
                    StartCoroutine(ShootTimer());
                    return;
                }
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
    private void PlayAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }
}
