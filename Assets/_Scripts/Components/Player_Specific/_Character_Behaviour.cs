using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class _Character_Behaviour : MonoBehaviourPunCallbacks, IDamageable
{
    [Header("Components")]
    public Stats_Component                              statsComponent;
    public ImmunoXP_Component                           immunoComponent;
    [SerializeField] private Initialize_Component       serverInitializeComponent;
    [System.NonSerialized] public Weapon_Component      weaponComponent = null;
    public AnimationState_Component                     animationComponent;
    private PhotonView                                  photonComponent;
    public Aim_Component                                aimComponent;
    [Header("ProgressBar")]
    [SerializeField] private ProgressBar                healthProgressBar;
    [SerializeField] private TextMeshProUGUI            playerText;

    [Header("Sockets")]
    [SerializeField] private Transform handTransform;

    [SerializeField] private Ranged_WeaponClass weaponClass;

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
            aimComponent = null;
        }
        else
        {
            GameObject aimCube = Instantiate(Resources.Load<GameObject>("Weapons/AimCube/DebugAimCube"));
            aimComponent.Start(aimCube);
        }
        playerText.text = PhotonNetwork.NickName;
    }
    private void Update()
    {
        if (photonComponent.IsMine)
        {
            if (aimComponent != null)
                aimComponent.Update();
            animationComponent.Update();
            if (Input.GetMouseButtonDown(0) && weaponComponent != null) //Left Mouse Button
            {
                weaponComponent.Shot(gameObject);
            }
        }

    }
    private void Start()
    {
        SpawnWeapon(weaponClass.weaponData.prefabLocation);
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
        weaponComponent.aimComponent = aimComponent;
        weaponComponent.animComponent = animationComponent;
    }

}
