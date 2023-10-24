using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private Vector3 targetPosition;
    private Vector3 enemyPosition;
    private Vector3 playerPosition;
    private float distanceTarget, distancePlayer;
    private float minSpaceBetween = 1.5f;
    private float friendDistance = 5f;
    private int position;

    public _Enemy_Behaviour enemyBehaviour;
    //EnemyNavMesh[] enemyGroup = new EnemyNavMesh[5];
    public bool liberado = false;
    private int linha;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        targetPosition = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
        enemyBehaviour = GetComponent<_Enemy_Behaviour>();

        enemyAgent.speed = enemyBehaviour.speed;
        enemyAgent.acceleration = enemyBehaviour.acceleration;
        enemyAgent.stoppingDistance = enemyBehaviour.stoppingDistance;
        linha = this.GetComponent<_Enemy_Behaviour>().linha;
    }

    // Update is called every frame

    void Update()
    {
        int i = 0;
        if (this.GetComponent<_Enemy_Behaviour>().coluna == 4)
        {
            Liberar(linha);
        }
        if (liberado)
        {
            //Debug.Log("Rodando liberado");
            enemyPosition = transform.position;
            distanceTarget = Vector3.Distance(targetPosition, enemyPosition);
            distancePlayer = Vector3.Distance(playerPosition, enemyPosition);
            {

            }
            if (distanceTarget >= 4f)
            {
                Mover();
            }
            else
            {
                enemyBehaviour.InnTargetEnd();
            }
        }

    }
    public void Mover()
    {
        //Debug.Log("Rodando Agrupar");
        foreach (GameObject enemy in SpawnSystem.enemyGroup[linha])
        {
            if (enemy!=null)
            {
                float distance = Vector3.Distance(enemy.transform.position, this.transform.position);

                if (distance < friendDistance)
                {

                    if (distance <= minSpaceBetween)
                    {
                        //Debug.Log("distance: " + distance);
                        Vector3 direction = transform.position - enemy.transform.position;
                        transform.Translate(direction * Time.deltaTime);
                    }
                    //else
                    enemyAgent.SetDestination(targetPosition);
                }
            }
            
        }
    }
    public void Liberar(int linha)
    {
        for(int i = 0; i < SpawnSystem.enemyGroup[linha].Length; i++)
        {
            if (SpawnSystem.enemyGroup[linha][i]!=null)
            {
                SpawnSystem.enemyGroup[linha][i].GetComponent<EnemyNavMesh>().liberado = true;
                Debug.Log("Grupo: " + linha);
                Debug.Log("enemyGroup[" + i + "].liberado = " + SpawnSystem.enemyGroup[linha][i].GetComponent<EnemyNavMesh>().liberado);
            }
            
        }
        
    }
}
/*using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private Vector3 targetPosition;
    private Vector3 enemyPosition;
    private Vector3 playerPosition;
    private float distanceTarget, distancePlayer;
    public _Enemy_Behaviour enemyBehaviour;
    private EnemyNavMesh[] enemyGroup;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        targetPosition = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
        enemyBehaviour = GetComponent<_Enemy_Behaviour>();

        enemyAgent.speed = enemyBehaviour.speed;
        enemyAgent.acceleration = enemyBehaviour.acceleration;
        enemyAgent.stoppingDistance = enemyBehaviour.stoppingDistance;

        enemyGroup = new EnemyNavMesh[1];
    }

    // Update is called every frame

    void Update()
    {

        enemyPosition = transform.position;
        distanceTarget = Vector3.Distance(targetPosition, enemyPosition);
        distancePlayer = Vector3.Distance(playerPosition, enemyPosition);
        //if (distancePlayer <= 5f)
        {
            //Debug.Log("Podia te Pegar.");
        }
        if (distanceTarget >= 4f)
        {
            //enemyAgent.SetDestination(targetPotion);
            AlgonoUpdate();
        }
        else
        {
            enemyBehaviour.InnTargetEnd();
        }
        //PhotonNetwork.Destroy(gameObject);
    }
    public void AlgonoUpdate()
    {
        float minSpaceBetween = 1.5f;
        float friendDistance = 5f;
        //float maxSpaceBetween = 10f;
        //int num = 0;

        foreach (EnemyNavMesh go in FindObjectsOfType<EnemyNavMesh>())
        {

            if (go != gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, this.transform.position);

                if (distance < friendDistance)
                {
                    //Debug.Log("distance: " + distance + "| friendDistance: " + friendDistance);
                    foreach (EnemyNavMesh enemy in enemyGroup)
                    {
                        //
                        //distance = Vector3.Distance(enemy.transform.position, this.transform.position);
                        //
                        if (distance <= minSpaceBetween)
                        {
                            Vector3 direction = transform.position - go.transform.position;
                            transform.Translate(direction * Time.deltaTime);
                            // enemyBehaviour.speed = min;

                        }
                        else
                            enemyAgent.SetDestination(targetPosition);
                    }
                }
            }
        }
    }

}*/


