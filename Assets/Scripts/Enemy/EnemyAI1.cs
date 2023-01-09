using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI1 : Enemy
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform home;
    public LayerMask whatIsGround, whatIsPlayer;
    public int MaxHP = 100;

    //Patroling
    public Vector3 walkPoint;
    public Vector3 walkPoint2;
    public bool walkPointSet = false;
    public float walkPointRange;

    //Attacking
    public float Ainterval;
    bool Attacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool InSightRange, InAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        CurrentHP = MaxHP;
    }

    public void Update()
    {
        
        //Check for sight and attack range
        InSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        InAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //state check and loop
        if (!InSightRange && !InAttackRange) Patroling();
        if (InSightRange && !InAttackRange) ChasePlayer();
        if (InAttackRange && InSightRange) AttackPlayer();

    }

    private void Patroling()
    {
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (!walkPointSet)//if not set walkpoint yet, set it
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x * 1.3f + randomX, transform.position.y, transform.position.z * 1.3f + randomZ);
            Invoke(nameof(ChangeTarget), 3.5f);
            agent.SetDestination(walkPoint);
        }

        if (distanceToWalkPoint.magnitude < 1.0f)
        {
            walkPointSet = false;
        }

    }

    private void ChangeTarget()
    {
        if (!walkPointSet)
        {
            walkPointSet = true;
            walkPoint2 = new Vector3(home.position.x, home.position.y, home.position.z);
            agent.SetDestination(walkPoint2);
            Invoke(nameof(ChangeTargetB), 3.5f);
        }

        if (walkPointSet)
        {
            walkPoint2 = new Vector3(home.position.x, home.position.y, home.position.z);
            agent.SetDestination(walkPoint2);
            Invoke(nameof(ChangeTargetB), 3.5f);
        }
    }

    private void ChangeTargetB()//loop for patroling
    {
            walkPointSet = false;
    }

    private void ChasePlayer()//chasing player when find plyer, set new destination with player position
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()//attack player and look at player
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!Attacked)
        {
            //bullet power
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            rb.AddForce(transform.up * 0.6f, ForceMode.Impulse);

            Attacked = true;
            Invoke(nameof(ResetAttack), Ainterval);
        }
    }
    private void ResetAttack()
    {
        Attacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
