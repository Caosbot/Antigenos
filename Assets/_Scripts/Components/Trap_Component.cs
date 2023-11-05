using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Trap_Component : MonoBehaviour
{
    private bool canDamage = true;
    public int damage = 10;
    public float delay = 2;
    public GameObject damager;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage)
        {
            return;
        }
        _Enemy_Behaviour e = other.gameObject.GetComponent<_Enemy_Behaviour>();
        if(e!= null)
        {
            if (e.gameObject.TryGetComponent(out IAntigen antigen))
            {
                InterfaceHelper.GetDamageable(e.gameObject).TakeDamage(10, false, gameObject);
                GetComponent<TrapVariables>().TrapWork();
                GameManager.Debuger("Armadilha Funcionou");
            }
        }
    }
    public IEnumerator DelayDamage()
    {
        canDamage = false;
        damager.SetActive(true);
        yield return new WaitForSeconds(delay);
        damager.SetActive(false);
        canDamage = true;
    }
    public void StartDelay()
    {
        StartCoroutine(DelayDamage());
    }
}
