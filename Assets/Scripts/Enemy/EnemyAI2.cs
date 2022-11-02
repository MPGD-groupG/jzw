using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnemyAI2 : MonoBehaviour
{
    public enum FSMState
    {
        Wander,     //randon wander state
        Seek,       //search player state
        Chase,      //follow player state
        Attack,     //attack player state
        Dead,       //dead state, dead check and action
    }
    public float SoundRange; //{ get; private set; }//this is about auditory system
    public GameObject player; //{ get; private set; }//to set which one is player
    public Transform nearbyPlayer; //{ get; private set; }//lock player and do next action
    public float SightRange; //{ get; private set; }//vision range
    public int attackFieldOfView; //{ get; private set; }//attack field means can not attack back need turn around first then attack
    public bool firstInDead; //{ get; private set; }//dead (and could be used in resurgence if need)

    public GameObject zombieEye;//set vision range object

    public float SightAngle;//set vision range

    public float senseTimer;//it is a timecount, using to fixupdate part

    public float SensorInterval;//a fixed value of interval time

    bool IsAlive = true;//working with dead system
    public float currentHP;//hp
    public float stopTime;
    bool getDamaged = false;//damage system
    public Vector3 damageDirection;

    public FSMState currentState;

    public Transform targetPlayer;

    public NavMeshAgent agent;
    private float seekDistance;

    public float attackRange;
    float attackTimer;
    float attackInterval;

    public float disappearTimer;
    bool disappeared;
    //bool AgentDone = false;

    float disappearTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        FSMUpdate();
        //perception at regular intervals
        if (senseTimer >= SensorInterval)
        {
            senseTimer = 0;
            SenseNearbyPlayer();
        }
        senseTimer += Time.deltaTime;

    }
    //state update
    void FSMUpdate()
    {
        //different state check and use
        switch (currentState)
        {
            case FSMState.Wander:
                UpdateWanderState();
                Debug.Log("001B");
                break;
            case FSMState.Seek:
                UpdateSeekState();
                Debug.Log("002B");
                break;
            case FSMState.Chase:
                UpdateChaseState();
                Debug.Log("003B");
                break;
            case FSMState.Attack:
                UpdateAttackState();
                Debug.Log("004B");
                break;
            case FSMState.Dead:
                UpdateDeadState();
                Debug.Log("005B");
                break;
        }
    }
    public void UpdateWanderState()
    {
        //feel player then try to chase
        targetPlayer = getNearbyPlayer();
        if (targetPlayer != null)
        {
            currentState = FSMState.Chase;
            agent.ResetPath();
            return;
        }

        //if get damage then go chase

        if (this.getDamaged)
        {
            currentState = FSMState.Seek;
            agent.ResetPath();
            return;
        }
        if (AgentDone())
        {  //arrive place? and next place
            //Invoke(nameof(Wait), 1.5f);
            Vector3 randomRange = new Vector3((Random.value - 0.5f) * 2,
                                                0, (Random.value - 0.5f) * 2);
            Vector3 nextDestination = this.transform.position + randomRange;
            agent.destination = nextDestination;
        }
        //if dead
        if (currentState != FSMState.Dead && !this.IsAlive)
        {
            
            currentState = FSMState.Dead;
        }
    }
    protected bool AgentDone()
    {
        //there are two rules one is pathPending = false, one is remainingDistance = or less than stoppingDistance
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    private void Wait()
    {
        Vector3 randomRange = new Vector3((Random.value - 0.5f) * 2,
                                                0, (Random.value - 0.5f) * 2);
        Vector3 nextDestination = this.transform.position + randomRange;
        agent.destination = nextDestination;
    }

    /*private void Wait2()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        Vector3 nextDestination = new Vector3(player.position.x * 0.85f + player.position.x * 1.9f, transform.position.y, player.position.z * 0.85f + player.position.z * 1.9f);
        agent.destination = nextDestination;
    }*/

    public void UpdateSeekState()
    {
        //if player in range then chess
        targetPlayer = this.getNearbyPlayer();
        if (targetPlayer != null)
        {
            currentState = FSMState.Chase;
            agent.ResetPath();
            return;
        }
        //if get attack,follow attack way and check
        if (this.getDamaged)
        {
            Vector3 seekDirection = this.damageDirection;
            agent.destination = this.transform.position
                + seekDirection * seekDistance;
            //already getDamaged
            this.getDamaged = false;
        }

        //if arrive place or cant move,then back wander state
        if (AgentDone())
        {
            currentState = FSMState.Wander;
            agent.ResetPath();
            return;
        }
        //if dead
        if (currentState != FSMState.Dead && !this.IsAlive)
        {
            Debug.Log("002");
            currentState = FSMState.Dead;
        }
    }
    public void UpdateChaseState()
    {
        //if no player this place then back wander
        targetPlayer = this.getNearbyPlayer();
        if (targetPlayer == null)
        {
            currentState = FSMState.Wander;
            agent.ResetPath();
            return;
        }
        //if player distance less than attack range then move to attack state
        if (Vector3.Distance(targetPlayer.position, transform.position) <= attackRange)
        {
            currentState = FSMState.Attack;
            agent.ResetPath();
            return;
        }
        //if dead
        if (currentState != FSMState.Dead && !this.IsAlive)
        {
            currentState = FSMState.Dead;
        }
        //set destination is player position
        agent.SetDestination(targetPlayer.position);
    }
    public void UpdateAttackState()
    {
        //if no player anymore in this place then back to wander
        targetPlayer = this.getNearbyPlayer();
        if (targetPlayer == null)
        {
            currentState = FSMState.Wander;
            agent.ResetPath();
            this.GetComponent<Animator>().SetBool("isAttack", false);
            return;
        }
        //if player out of attack range then chase state
        if (Vector3.Distance(targetPlayer.position, transform.position) > attackRange)
        {
            currentState = FSMState.Chase;
            agent.ResetPath();
            this.GetComponent<Animator>().SetBool("isAttack", false);
            return;
        }
        //caculate attack range, in this range can attack
        Vector3 direction = targetPlayer.position - transform.position;
        float degree = Vector3.Angle(direction, transform.forward);
        if (degree < attackFieldOfView / 2 && degree > -attackFieldOfView / 2)
        {
            this.GetComponent<Animator>().SetBool("isAttack", true);
            if (attackTimer > attackInterval)
            {
                attackTimer = 0;
                //damage player
                // ph.TakeDamage(attackDamage);
            }
            attackTimer += Time.deltaTime;
        }
        else
        {
            //if player at back then need turn back first
            this.GetComponent<Animator>().SetBool("isAttack", false);
            transform.LookAt(targetPlayer);
        }
        //if dead
        if (currentState != FSMState.Dead && !this.IsAlive)
        {
            Debug.Log("003");
            currentState = FSMState.Dead;
        }
    }
    public void UpdateDeadState()
    {
        Debug.Log("00ex");
        if (firstInDead)
        {
            firstInDead = false;

            agent.ResetPath();
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            ////play dead animator//no animation right now
            //this.GetComponent<Animator>().applyRootMotion = true;
            //this.GetComponent<Animator>().SetTrigger("toDie");
            //dead time 
            disappearTimer = 0;
            disappeared = false;
        }
        if (!disappeared)
        {

            if (disappearTimer > disappearTime)
            {
                this.gameObject.SetActive(false);
                disappeared = true;
            }
            //dead time counter
            disappearTimer += Time.deltaTime;
        }
    }
    public void SenseNearbyPlayer()
    {

        //caculate distance with player
        float dist = Vector3.Distance(player.transform.position, this.transform.position);

        //hear player funtion
        if (dist < SoundRange)
        {
            //save this player
            nearbyPlayer = player.transform;
        }
        //see player funtion
        else if (dist < SightRange)
        {
            //is player in the vision range check
            Vector3 direction = player.transform.position - this.transform.position;
            float degree = Vector3.Angle(direction, this.transform.forward);

            if (degree < SightAngle / 2 && degree > -SightAngle / 2)
            {
                Ray ray = new Ray();
                ray.origin = zombieEye.transform.position;
                ray.direction = direction;
                RaycastHit hitInfo;
                //check is there any barrier between palyer
                if (Physics.Raycast(ray, out hitInfo, SightRange))
                {
                    if (hitInfo.transform == player.transform)
                    {
                        //if can then save this player
                        nearbyPlayer = player.transform;
                    }
                }
            }
        }
        else
        {
            nearbyPlayer = null;
            currentState = FSMState.Wander;

        }
    }
    public Transform getNearbyPlayer()
    {
        return nearbyPlayer;
    }

    public void TakeDamage(int damage, Vector3 shootPosition)
    {
        if (!IsAlive)
            return;
        //undate hp
        currentHP -= damage;
        if (currentHP <= 0) currentHP = 0;
        if (IsAlive)
        {
            //get damage!
            getDamaged = true;
            //where get damage and save this way
            damageDirection = shootPosition - transform.position;
            damageDirection.Normalize();
        }
    }
}
