using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Component : MonoBehaviour
{
    private bool canDamage = true;
    public int damage = 10;
    public float delay;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        _Enemy_Behaviour e = other.gameObject.GetComponent<_Enemy_Behaviour>();
        if(e!= null)
        {
            if (e.gameObject.TryGetComponent(out IAntigen antigen))
            {
                InterfaceHelper.GetDamageable(e.gameObject).TakeDamage(10, false, gameObject);
                StartCoroutine(DelayDamage());
                GameManager.Debuger("Armadilha Funcionou");
            }
        }
    }
    public IEnumerator DelayDamage()
    {
        canDamage = false;
        yield return new WaitForSeconds(delay);
        canDamage = true;
    }
}
