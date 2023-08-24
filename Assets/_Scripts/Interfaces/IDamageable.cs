using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int inDamage, bool ignoreArmor = false);
}
public static class InterfaceHelper
{
    public static IDamageable GetDamageable(GameObject desiredObject)
    {
        if(desiredObject.TryGetComponent(out IDamageable clickableObject))
        {
            return clickableObject;
        }
        Debug.LogWarning("Not Damageable");
        return null;
    }
}
