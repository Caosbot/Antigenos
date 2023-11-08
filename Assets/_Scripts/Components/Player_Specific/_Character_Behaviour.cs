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
    public PhotonView                                  photonComponent;
    public Aim_Component                                aimComponent;
    [Header("ProgressBar")]
    [SerializeField] private ProgressBar                healthProgressBar;
    [SerializeField] private TextMeshProUGUI            playerText;

    [Header("Sockets")]
    [SerializeField] private Transform handTransform;

    [SerializeField] private Ranged_WeaponClass weaponClass;

    public int money;

    private GameObject[] instantedObjects;

    public void DoDamage(_Enemy_Behaviour enemyBehaviour)
    {
        int tempDamage = 5;
        if (enemyBehaviour.gameObject.TryGetComponent(out IAntigen antigen))
        {
            tempDamage *= (int)immunoComponent.GetImmunoMultiplier(antigen.GetImmunoType()); //Multiplica o dano pelo multiplicador Imune
            InterfaceHelper.GetDamageable(enemyBehaviour.gameObject).TakeDamage(tempDamage, false,gameObject); //D� o dano
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Not Antigen");
#endif
        }
    }

    private void Awake()
    {
        StartCoroutine(DelayAwake());
    }
    private IEnumerator DelayAwake()
    {
        yield return new WaitForSeconds(2);
        photonComponent = GetComponent<PhotonView>();
        if (!photonComponent.IsMine)
        {
            serverInitializeComponent.Initialize();
            aimComponent = null;
        }
        else
        {
            GameObject aimCube = Instantiate(Resources.Load<GameObject>("Weapons/AimCube/DebugAimCube"));
            DontDestroyOnLoad(aimCube);
            aimComponent.Start(aimCube);
        }
        playerText.text = PhotonNetwork.NickName;
    }
    private void Update()
    {
        playerText.transform.LookAt(Camera.main.transform);/////////////
        if (photonComponent.IsMine)
        {
            if (aimComponent != null)
                aimComponent.Update();
            animationComponent.Update();
            if (Input.GetMouseButtonDown(0) && weaponComponent != null) //Left Mouse Button
            {
                weaponComponent.Shot(gameObject);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("A");
                Application.Quit();
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
        GameObject instance = PhotonNetwork.Instantiate(weaponLocation,new Vector3(0,0,0), new Quaternion(0,0,0,0));
        instance.transform.parent = handTransform;
        instance.transform.localPosition = new Vector3(0, 0, 0);
        instance.transform.localRotation = new Quaternion(0, 0, 0, 0);
        instance.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        weaponComponent = instance.GetComponent<Weapon_Component>();
        weaponComponent.aimComponent = aimComponent;
        weaponComponent.animComponent = animationComponent;
        instantedObjects = new GameObject[1];
        instantedObjects[0] = instance;
    }
    public void DestroyInstantedObjects()
    {
        GetComponent<PhotonView>().RPC(nameof(DestroyOb), RpcTarget.MasterClient);
    }
    [PunRPC]
    private void DestroyOb()
    {
        foreach (GameObject g in instantedObjects)// Acho que aqui está distruindo todas as armas.
        {
            PhotonNetwork.Destroy(g);
        }
    }
}
