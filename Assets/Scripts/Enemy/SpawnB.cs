using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnB : Enemy
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform home;
    public LayerMask whatIsGround, whatIsPlayer;

    //public float health;

    public int MaxHP = 100;

    //public Slider Slider;

    //public int CurrentHP;

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
    //public int damage;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        //home = GameObject.Find("PickUp").transform;
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

        if (!InSightRange && !InAttackRange) Patroling();
        if (InSightRange && !InAttackRange) ChasePlayer();
        if (InAttackRange && InSightRange) AttackPlayer();

    }

    private void Patroling()
    {
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (!walkPointSet)
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(home.position.x * 1.3f + randomX * 2.3f, transform.position.y, home.position.z * 1.3f + randomZ * 2.9f);
            walkPointSet = true;
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Invoke(nameof(ChangeTargetB), 2.5f);
        }

        if (distanceToWalkPoint.magnitude < 0.2f)
        {
            walkPointSet = false;
            //Invoke(nameof(ChangeTarget), 0.9f);
        }

    }

    private void ChangeTarget()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(home.position.x * 1.85f + randomX * 2.3f, transform.position.y, home.position.z * 1.35f + randomZ * 2.9f);
        walkPointSet = true;
        //agent.SetDestination(walkPoint);
    }

    private void ChangeTargetB()
    {
        walkPoint2 = new Vector3(home.position.x, home.position.y, home.position.z);
        agent.SetDestination(walkPoint2);
        Invoke(nameof(ChangeTargetC), 2.5f);
        //walkPointSet = true;
    }

    private void ChangeTargetC()
    {
        walkPointSet = false;
    }

    /*private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);


        if (Physics.Raycast(walkPoint, -transform.up, 2.0f, whatIsGround))
        {
            walkPointSet = true;
        }
        //Invoke(nameof(ChangeTarget), 1.0f);

    }*/

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!Attacked)
        {
            //bullet power
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            rb.AddForce(transform.up * 0.5f, ForceMode.Impulse);

            Attacked = true;
            Invoke(nameof(ResetAttack), Ainterval);
        }
    }
    private void ResetAttack()
    {
        Attacked = false;
    }

    /*public void TakeDamage(int damage)//
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    //public void TakeDamage(int damage)
    //{
    //    //if (!isServer) return;
    //    CurrentHP -= damage;
    //    Debug.Log("hp--");
    //    //ShowHPSlider();
    //    if (CurrentHP <= 0)
    //    {
    //        Invoke(nameof(DestroyEnemy), 0.1f);
    //    }
    //}

    //public void DestroyEnemy()
    //{
    //    Destroy(gameObject);
    //}



    /*public void ShowHPSlider()//for hp UI
    {
        Slider.value = CurrentHP / (float)MaxHP;
    }*/
}
