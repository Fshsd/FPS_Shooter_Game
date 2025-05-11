using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround , whatIsPlayer;

    //Partoling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange , attackRange;
    public bool playerInSightRange , playerInAttackRange;


    private void Awake()
    {
        player.GetComponent<Transform>();
        agent.GetComponent<NavMeshAgent>();
    }

    private void Patroling()
    {

    }
    private void ChasePlayer()
    {
        
    }
    private void AttackPlayer()
    {
        
    }
}
