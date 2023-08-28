using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy_Behaviour : MonoBehaviour, IAntigen, IDamageable
{
    public Stats_Component statsComponent;
    [SerializeField] private ImmunoXpQuantity quantityOfXp = new ImmunoXpQuantity(1);

    public void TakeDamage(int inDamage, bool ignoreArmor = false, GameObject damageDealer = null)
    {
        Debug.Log(inDamage + " de Dano foi recebido de " + damageDealer.name);
        statsComponent.ReceiveDamage(inDamage, ignoreArmor);
    }
    public ImmunoType GetImmunoType()
    {
        return quantityOfXp.typeOfImmunity;
    }
}

[System.Serializable]
struct ImmunoXpQuantity
{
    public ImmunoType typeOfImmunity;
    public int quantity;
    public ImmunoXpQuantity(int value, ImmunoType TypeOfImmunity = ImmunoType.Bacteria)
    {
        typeOfImmunity = TypeOfImmunity;
        quantity = value;
    }
}

interface IAntigen
{
    public ImmunoType GetImmunoType();
}
