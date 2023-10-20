using Photon.Pun;
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
        if (distanceTarget >= 4f )
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
        float friendDistance=5f;
        //float maxSpaceBetween = 10f;
        //int num = 0;

        foreach(EnemyNavMesh go in FindObjectsOfType<EnemyNavMesh>())
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
                //Debug.Log(enemyGroup.Length);
            }
        }
        
    }


// * ***************************************************************************************
/*
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    private float maxSpeed;
    private float separationDistance;
    private float alignmentDistance;
    private float cohesionDistance;
    private float minDistance = 0.5f;
    
    private int maxAgents = 2;
    private bool group =false;

    private List<EnemyNavMesh> boids;

   

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

        IniciarFlocking();
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
        if (distanceTarget >= 4f)
        {
            if (group==true)
                enemyAgent.SetDestination(targetPotion);
            else
                Flocking();
        }
        else
        {
            enemyBehaviour.InnTargetEnd();
        }
        //PhotonNetwork.Destroy(gameObject);
    }
    public void Flocking()
    {
        // Atualizar a lista de boids vizinhos.

        foreach (EnemyNavMesh boid in GameObject.FindObjectsOfType<EnemyNavMesh>())
        {
            if (boid != this)
            {
                boids.Add(boid);
            }
        }
        //*****************************************************************************
        foreach (EnemyNavMesh boid in boids)
        {
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (distance < minDistance)
            {
                // Aplicar a força de atração.
                Vector3 attractionForce = boid.transform.position - transform.position;
                attractionForce.Normalize();
                attractionForce *= 10f;

                // Atualizar a velocidade do boid.
                enemyAgent.velocity += attractionForce;
            }
        }
        //*****************************************************************************
        // Calcular a força de separação.
        Vector3 separationForce = Vector3.zero;
        /*foreach (EnemyNavMesh boid in boids)
        {
            Vector3 direction = boid.transform.position - transform.position;
            direction.Normalize();
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (distance < separationDistance)
            {
                separationForce += direction / distance;
            }
        }*/

        // Calcular a força de alinhamento.
       /* Vector3 alignmentForce = Vector3.zero;
        foreach (EnemyNavMesh boid in boids)
        {
            Vector3 direction = boid.transform.forward;
            alignmentForce += direction;
        }
        alignmentForce.Normalize();
        alignmentForce /= boids.Count;

        // Calcular a força de coesão.
        Vector3 cohesionForce = Vector3.zero;
        Vector3 centerOfMass = Vector3.zero;
        foreach (EnemyNavMesh boid in boids)
        {
            centerOfMass += boid.transform.position;
        }
        centerOfMass /= boids.Count;
        cohesionForce = centerOfMass - transform.position;

        // Aplicar as forças ao boid.
        Vector3 desiredVelocity = separationForce + alignmentForce + cohesionForce;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        // Atualizar a posição do boid.
        enemyAgent.velocity = desiredVelocity;
    }

    public void IniciarFlocking()
    {
        boids = new List<EnemyNavMesh>();
        maxSpeed = 5f;
        //separationDistance = 0.5f;
        //alignmentDistance = 1f;
        //cohesionDistance = 3f;
        // Remover os agentes extras.
        int numAgents = boids.Count;
        if (numAgents > maxAgents)
        {
            group = true;
            for (int i = numAgents - 1; i >= maxAgents; i--)
            {
                boids.RemoveAt(i);
            }
        }
    }

}*/

