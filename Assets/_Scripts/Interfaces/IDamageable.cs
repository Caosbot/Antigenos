using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int inDamage,bool ignoreArmor = false, GameObject damageDealer = null);
    void Die()
    {

    }
}
public static class InterfaceHelper
{
    public static IDamageable GetDamageable(GameObject desiredObject)
    {
        if(desiredObject.TryGetComponent(out IDamageable clickableObject))
        {
            return clickableObject;
        }
#if UNITY_EDITOR
        Debug.LogWarning("Not Damageable");
#endif
        return null;
    }
}
