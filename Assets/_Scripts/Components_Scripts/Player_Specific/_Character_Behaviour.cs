using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Character_Behaviour : MonoBehaviour, IDamageable
{
    public Stats_Component statsComponent;
    public ImmunoXP_Component immunoComponent;
    [SerializeField] private Initialize_Component serverInitializeComponent;
    public WeaponClass baseClass;
    public Mellee_WeaponClass melleeClass;
    public Ranged_WeaponClass rangedClass;
    public ProgressBar healthProgressBar;

    public _Enemy_Behaviour e;

    public void DoDamage(_Enemy_Behaviour enemyBehaviour)
    {
        int tempDamage = 10;
        if (enemyBehaviour.gameObject.TryGetComponent(out IAntigen antigen))
        {
            tempDamage *= (int)immunoComponent.GetImmunoMultiplier(antigen.GetImmunoType()); //Multiplica o dano pelo multiplicador Imune
            InterfaceHelper.GetDamageable(enemyBehaviour.gameObject).TakeDamage(tempDamage, false,gameObject); //Dá o dano
        }
        else
        {
            Debug.LogWarning("Not Antigen");
        }
    }

    private void Start()
    {
        TakeDamage(10);
        healthProgressBar.UpdatePercentage(statsComponent.GetStatPercentage("Vida"));
        Debug.Log(statsComponent.GetStatPercentage("Vida"));
    }
    public void TakeDamage(int inDamage, bool ignoreArmor = false, GameObject damageDealer = null)
    {
        statsComponent.ReceiveDamage(inDamage,ignoreArmor);
        healthProgressBar.UpdatePercentage(statsComponent.GetStatPercentage("Vida"));
    }

}
