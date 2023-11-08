using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class _Enemy_Behaviour : MonoBehaviour, IAntigen, IDamageable
{
    public Stats_Component statsComponent;
    [SerializeField] private ImmunoXpQuantity quantityOfXp = new ImmunoXpQuantity(1);
    [SerializeField] private ProgressBar progressBar;
    public float speed=7f, acceleration=7f, stoppingDistance = 0.5f;
    public Enemy_Animation enemy_Animation;
    public int linha, coluna;

    public void TakeDamage(int inDamage, bool ignoreArmor = false, GameObject damageDealer = null)
    {
        string tempName = "";
        inDamage *= DamageMultiplayer();
        if(damageDealer != null)
        {
            tempName = damageDealer.name;
        }
        enemy_Animation.PlayDesiredAnimation("Hit");
#if UNITY_EDITOR
        GameManager.Debuger((inDamage )+ " de Dano foi recebido de " + tempName);
        //Debug.Log(statsComponent.FindStatValue("Vida"));
#endif
        GetComponent<PhotonView>().RPC(nameof(SetLife), RpcTarget.All, inDamage, ignoreArmor);
        if (statsComponent.FindStatValue("Vida") <= 0)
        {
            _Character_Behaviour[] cc = GameObject.FindObjectsOfType<_Character_Behaviour>();
            foreach(_Character_Behaviour c in cc)
            {
                c.money += 1;
            }
            GetComponent<PhotonView>().RPC(nameof(Die),RpcTarget.MasterClient);
        }
    }
    public IEnumerator DIESenaomover()
    {
        while (true)
        {
            Vector3 t = gameObject.transform.position;
            yield return new WaitForSeconds(30);
            float difference = t.x - gameObject.transform.position.x;
            if (difference > -0.1f && difference < 0.1f)
            {
                this.TakeDamage(100000);
            }
        }
    }
    void Start()
    {
        statsComponent.Initialize();
        StartCoroutine(DIESenaomover());
    }
    [PunRPC]
    public void SetLife(int inDamage, bool ignoreArmor, PhotonMessageInfo info)
    {
        statsComponent.ReceiveDamage(inDamage, ignoreArmor);
        progressBar.UpdatePercentage(statsComponent.GetStatPercentage("Vida"));
    }
    public ImmunoType GetImmunoType()
    {
        return quantityOfXp.typeOfImmunity;
    }
    [PunRPC]
    public void Die()
    {
        SpawnSystem.spawnedEnemies--;
        PhotonNetwork.Destroy(gameObject);
        SpawnSystem.enemyGroup[linha][coluna] = null;
    }
    public void InnTargetEnd()
    {
        GetComponent<PhotonView>().RPC(nameof(SpawnTakeDamage), RpcTarget.All);
        GetComponent<PhotonView>().RPC(nameof(Die), RpcTarget.MasterClient);
        
    }

    [PunRPC]
    public void SpawnTakeDamage()
    {
        SpawnSystem.SpawnTakeDamage();
    }
    public int DamageMultiplayer()
    {
        int numPlayers;
        numPlayers = SpawnSystem.numPlayers;
        //Debug.Log("Qtd de Players: " + numPlayers);
        if (numPlayers == 1)
        {
            return 3;
        }
        if (numPlayers == 2)
        {
            return 2;
        }
        else
            return 1;
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
