using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy_Behaviour : MonoBehaviour, IAntigen, IDamageable
{
    public Stats_Component statsComponent;
    [SerializeField] private ImmunoXpQuantity quantityOfXp = new ImmunoXpQuantity(1);
    [SerializeField] private ProgressBar progressBar;

    public void TakeDamage(int inDamage, bool ignoreArmor = false, GameObject damageDealer = null)
    {
        string tempName = "";
        if(damageDealer != null)
        {
            tempName = damageDealer.name;
        }
        Debug.Log(inDamage + " de Dano foi recebido de " + tempName);
        statsComponent.ReceiveDamage(inDamage, ignoreArmor);
        progressBar.UpdatePercentage(statsComponent.GetStatPercentage("Vida"));
        if (statsComponent.FindStatValue("Vida") <= 0)
        {
            Die();
        }
    }
    public ImmunoType GetImmunoType()
    {
        return quantityOfXp.typeOfImmunity;
    }
    public void Die()
    {
        Destroy(gameObject);
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
