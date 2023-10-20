using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class CopyEnemyNavMesh : MonoBehaviour
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

    private List<CopyEnemyNavMesh> boids;

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
        if (distanceTarget >= 4f)
        {
            enemyAgent.SetDestination(targetPotion);
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
        foreach (CopyEnemyNavMesh boid in GameObject.FindObjectsOfType<CopyEnemyNavMesh>())
        {
            if (boid != this)
            {
                boids.Add(boid);
            }
        }

        // Calcular a força de separação.
        Vector3 separationForce = Vector3.zero;
        foreach (CopyEnemyNavMesh boid in boids)
        {
            Vector3 direction = boid.transform.position - transform.position;
            direction.Normalize();
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (distance < separationDistance)
            {
                separationForce += direction / distance;
            }
        }

        // Calcular a força de alinhamento.
        Vector3 alignmentForce = Vector3.zero;
        foreach (CopyEnemyNavMesh boid in boids)
        {
            Vector3 direction = boid.transform.forward;
            alignmentForce += direction;
        }
        alignmentForce.Normalize();
        alignmentForce /= boids.Count;

        // Calcular a força de coesão.
        Vector3 cohesionForce = Vector3.zero;
        Vector3 centerOfMass = Vector3.zero;
        foreach (CopyEnemyNavMesh boid in boids)
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
}


