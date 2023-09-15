using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Character_Behaviour : MonoBehaviour, IDamageable
{
    [Header("Components")]
    public Stats_Component                              statsComponent;
    public ImmunoXP_Component                           immunoComponent;
    [SerializeField] private Initialize_Component       serverInitializeComponent;
    [System.NonSerialized] public Weapon_Component      weaponComponent = null;
    public AnimationState_Component                     animationComponent;
    private PhotonView                                  photonComponent;
    [Header("ProgressBar")]
    [SerializeField] private ProgressBar                healthProgressBar;
    [SerializeField] private TextMeshProUGUI            playerText;

    [Header("Sockets")]
    [SerializeField] private Transform handTransform;

    public void DoDamage(_Enemy_Behaviour enemyBehaviour)
    {
        int tempDamage = 10;
        if (enemyBehaviour.gameObject.TryGetComponent(out IAntigen antigen))
        {
            tempDamage *= (int)immunoComponent.GetImmunoMultiplier(antigen.GetImmunoType()); //Multiplica o dano pelo multiplicador Imune
            InterfaceHelper.GetDamageable(enemyBehaviour.gameObject).TakeDamage(tempDamage, false,gameObject); //D� o dano
        }
        else
        {
            Debug.LogWarning("Not Antigen");
        }
    }

    private void Awake()
    {
        photonComponent = GetComponent<PhotonView>();
        if (!photonComponent.IsMine)
        {
            serverInitializeComponent.Initialize();
        }
        playerText.text = PhotonNetwork.NickName;
    }
    private void Update()
    {
        animationComponent.Update();
        if (Input.GetMouseButtonDown(0) && weaponComponent != null) //Left Mouse Button
        {
            weaponComponent.Shot(gameObject);
        }
    }
    public void Die()
    {

    }
    public void TakeDamage(int inDamage, bool ignoreArmor = false, GameObject damageDealer = null)
    {
        statsComponent.ReceiveDamage(inDamage,ignoreArmor);
        healthProgressBar.UpdatePercentage(statsComponent.GetStatPercentage("Vida"));
    }
    public void SpawnWeapon(string weaponLocation)
    {
        GameObject instance = Instantiate(Resources.Load(weaponLocation, typeof(GameObject)), handTransform) as GameObject;
        weaponComponent = instance.GetComponent<Weapon_Component>();
        weaponComponent.aimComponent = gameObject.GetComponent<Aim_Component>();
    }

}
