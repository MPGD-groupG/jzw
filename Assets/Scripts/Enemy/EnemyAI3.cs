using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //public float health;

    public int MaxHP = 100;

    //public Slider Slider;

    public int CurrentHP;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet = false;
    public float walkPointRange;
    public GameObject sphere;//range
    public GameObject target;
    Vector3 wanderTarget = Vector3.zero;
    GameObject jitter;

    //Attacking
    public float Ainterval;
    bool Attacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool InSightRange, InAttackRange;
    public int damage;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        CurrentHP = MaxHP;
        jitter = Instantiate(sphere);
    }

    void Seek(Vector3 location)
    {

        agent.SetDestination(location);
    }

    private void Update()
    {
        //Check for sight and attack range
        InSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        InAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!InSightRange && !InAttackRange) Patroling();
        if (InSightRange && !InAttackRange) ChasePlayer();
        if (InAttackRange && InSightRange) AttackPlayer();

    }

    /*private void Patroling()
    {
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (!walkPointSet)
        {
            Invoke(nameof(ChangeTarget), 2.0f);
        }

        if (walkPointSet)
        {
            Invoke(nameof(ChangeTargetB), 1.0f);
            //agent.SetDestination(walkPoint);
        }

        if (distanceToWalkPoint.magnitude < 0.2f)
        {
            walkPointSet = false;
            Invoke(nameof(ChangeTarget), 0.9f);
        }

    }*/

    private void Patroling()
    {
        float wanderRadius = 10.0f;
        float wanderDistance = 20.0f;
        float wanderJitter = 1.0f;

        wanderTarget += new Vector3(
            Random.Range(-1.0f, 1.0f) * wanderJitter,
            0.0f,
            Random.Range(-1.0f, 1.0f));
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0.0f, 0.0f, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        Debug.DrawLine(transform.position, targetWorld, Color.red);
        jitter.transform.position = targetWorld;
        Seek(targetWorld);
    }

   /*private void ChangeTarget()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(player.position.x * 1.85f + randomX * 2.3f, transform.position.y, player.position.z * 1.35f + randomZ * 2.9f);
        walkPointSet = true;
        //agent.SetDestination(walkPoint);
    }

    private void ChangeTargetB()
    {
        agent.SetDestination(walkPoint);
        walkPointSet = false;
    }*/

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
            rb.AddForce(transform.forward * 45f, ForceMode.Impulse);
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

    public void TakeDamage(int damage)
    {
        //if (!isServer) return;
        CurrentHP -= damage;
        Debug.Log("hp--");
        //ShowHPSlider();
        if (CurrentHP <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.1f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }



    /*public void ShowHPSlider()//for hp UI
    {
        Slider.value = CurrentHP / (float)MaxHP;
    }*/
}
