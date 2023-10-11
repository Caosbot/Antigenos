using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private Vector3 targetPotion;
    private Vector3 enemyPosition;
    private Vector3 playerPosition;
    private float distanceTarget, distancePlayer;
    public _Enemy_Behaviour enemyBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        targetPotion = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
        enemyBehaviour = GetComponent<_Enemy_Behaviour>();

        enemyAgent.speed = enemyBehaviour.speed;
        enemyAgent.acceleration = enemyBehaviour.acceleration;
        enemyAgent.stoppingDistance = enemyBehaviour.stoppingDistance;
    }

    // Update is called every frame
    
    void Update()
    {
        
        enemyPosition = transform.position;
        distanceTarget = Vector3.Distance(targetPotion, enemyPosition);
        distancePlayer = Vector3.Distance(playerPosition, enemyPosition);
        //if (distancePlayer <= 5f)
        {
            //Debug.Log("Podia te Pegar.");
        }
        if (distanceTarget >= 4f )
        {
            enemyAgent.SetDestination(targetPotion);
        }
        else
        {
            enemyBehaviour.InnTargetEnd();
        }
            //PhotonNetwork.Destroy(gameObject);
    }
}
