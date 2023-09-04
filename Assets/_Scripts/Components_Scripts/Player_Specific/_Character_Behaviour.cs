using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Character_Behaviour : MonoBehaviour, IDamageable
{
    [Header("Components")]
    public Stats_Component statsComponent;
    public ImmunoXP_Component immunoComponent;
    [SerializeField] private Initialize_Component serverInitializeComponent;
    public Weapon_Component weaponComponent;
    public AnimationState_Component animationComponent;
    [Header("ProgressBar")]
    [SerializeField] private ProgressBar healthProgressBar;

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
        GameObject instance = Instantiate(Resources.Load(weaponComponent.GetWeaponLocation(), typeof(GameObject)), weaponComponent.handTransform) as GameObject;
        TakeDamage(10);
        healthProgressBar.UpdatePercentage(statsComponent.GetStatPercentage("Vida"));
        Debug.Log(statsComponent.GetStatPercentage("Vida"));
    }
    private void Update()
    {
        animationComponent.Update();
    }
    public void TakeDamage(int inDamage, bool ignoreArmor = false, GameObject damageDealer = null)
    {
        statsComponent.ReceiveDamage(inDamage,ignoreArmor);
        healthProgressBar.UpdatePercentage(statsComponent.GetStatPercentage("Vida"));
    }

}
