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
    private Vector3 targetAgent;
    private Vector3 enemyPosition;
    private float distance;
    public _Enemy_Behaviour enemyBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        targetAgent = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
        enemyBehaviour = GetComponent<_Enemy_Behaviour>();
        if (targetAgent == null )
        {
            Debug.Log("Vazio");
        }
        Debug.Log("targetAgent = "+ targetAgent);
        enemyAgent.speed = 10f;
        enemyAgent.acceleration = 10f;
        enemyAgent.stoppingDistance = 0.5f;
    }

    // Update is called every frame
    
    void Update()
    {
        
        enemyPosition = transform.position;
        distance = Vector3.Distance(targetAgent, enemyPosition);
        if (distance >=4 )
        {
            enemyAgent.SetDestination(targetAgent);
        }
        else
        {
            enemyBehaviour.InnTargetEnd();
        }
            //PhotonNetwork.Destroy(gameObject);
    }
}
