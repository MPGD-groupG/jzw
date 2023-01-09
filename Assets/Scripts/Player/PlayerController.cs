using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    CharacterController characterController;
    Rigidbody rigidBody;
    public GameObject visualEffect;
    Vector3 newMovePosition;
    public static PlayerController instance;
    // public float moveSpeed = 10f;
    public float rotateSpeed = 2f;
    public float g = -9.81f;
    public float gravity = 10f;//gravity for controller test
    bool isOnGround;// if on the ground
    float ySpeed;//Y speed

    Vector3 gVelocity;


    public Vector2 moveValue;
    private int count;
    private int numPickups = 8;
    public Text scoreText;
    public Text winloseText;
    private float turnSpeed = 14f;
    // public Animation animation;

    // Buff time
    private float time;
    public float superTimeVal = 10; // Superpower Duration

    // Speed up
    public int speedUpConsume = 10; // Running cost 10 SP/s
    public float timeBetweenConsume = 1f;

    public float walkSpeed = 10.0f;
    public float runSpeed = 15.0f;
    public float superSpeed = 20.0f;
    public float moveSpeed;

    private PlayerSP playerSP;
    public bool isSpeedUp;  // Check if the player is running
    public bool canSpeedUp; // Check if the player can run

    // Restore HP
    Transform enemyTransform;

    // Speed up
    public bool gotSpeedUpPower;

    // Check once trigger
    private int playerTouchedOnce = 0;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    // Inventory
    public GameObject myBag;
    bool openBag;
    // Crafting
    public GameObject myCraft;
    bool openCraft;
    public Animator anim;
    private GameObject weapon;
    public MeshCollider coll3D;
    public float cooltime;
    public int damage;
    public float StartTime;

    private void Awake()
    {
        instance = this;
        characterController = GetComponent<CharacterController>();
        //rigidBody = GetComponent<Rigidbody>();
        playerSP = GetComponent<PlayerSP>();
        canSpeedUp = true;  // At the beginning the player's stamina value is full

        var enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyTransform = enemy.transform;

    }

    private void Start()
    {
        count = 0;
        winloseText.text = "";
        weapon = GameObject.Find("weapon");
        coll3D = weapon.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        OpenMyBag();
        OpenMyCraft();
    }


    void FixedUpdate()
    {
        isOnGround = characterController.isGrounded;
        Drop();


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 Direction = new Vector3(h, 0f, v);


        if (Direction.magnitude > 0)
        {
            anim.SetBool("run", true);
            float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Check the current status of the character
            if (canSpeedUp && Input.GetKey(KeyCode.LeftShift) && !gotSpeedUpPower)
            {
                // Speed up by consuming stamina value
                isSpeedUp = true;
                moveSpeed = runSpeed;   // Move with running speed
                time = time + Time.deltaTime;
                if (time >= timeBetweenConsume && playerSP.currentSP >= 0)    // Can only be consumed when there is a stamina value left
                {
                    ConsumeSP();
                }

            }
            else if (gotSpeedUpPower)
            {
                // Speed up by not consuming stamina value
                moveSpeed = superSpeed;   // Move with superpower speed
                visualEffect.SetActive(true); // Character effects display

                superTimeVal -= Time.deltaTime;
                if (superTimeVal <= 0)  // Can only have superpowers during superpower time
                {
                    visualEffect.SetActive(false);
                    gotSpeedUpPower = false;
                    superTimeVal = 10;
                }

                time = time + Time.deltaTime;
                if (time >= timeBetweenConsume && playerSP.currentSP <= 100)  // Can only restore stamina value when player is not running
                {
                    AutoRestoreSP();
                }
            }
            else
            {
                // Normal speed
                isSpeedUp = false;
                moveSpeed = walkSpeed;// Move with walk speed
                time = time + Time.deltaTime;
                if (time >= timeBetweenConsume && playerSP.currentSP <= 100)  // Can only restore stamina value when player is not running
                {
                    AutoRestoreSP();
                }

            }

            newMovePosition = moveDir * Time.deltaTime * moveSpeed;

            if (this.ObstacleDetect())   // Obstruction detected, object does not move
            {
                newMovePosition = -newMovePosition; // 
            }

            //rigidBody.MovePosition(rigidBody.position + newMovePosition);
            Vector3 speed = moveDir * moveSpeed;
            speed += Vector3.up * ySpeed;
            characterController.SimpleMove(speed);
        }
        else { anim.SetBool("run", false); }

    }

    void Drop()
    {
        if (!isOnGround)
        {

            ySpeed -= gravity * Time.deltaTime;

        }
        else
        {
            if (ySpeed < -1)
            {
                ySpeed += gravity * Time.deltaTime;
            }

        }
    }


    bool ObstacleDetect()
    {
        float radius = 0.5f; // Detection range
        Collider[] objectColliders = Physics.OverlapSphere(this.transform.position, radius, LayerMask.NameToLayer("layername")); // Multiple objects detected
        if (objectColliders.Length > 0)
        {
            for (int i = 0; i < objectColliders.Length; i++)
            {
                if (objectColliders[i].CompareTag("Obstacle"))
                {
                    return true;
                }
            }

        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 10);
    }


    void OnTriggerEnter(Collider other)
    {

        // Check if player out of scene
        if (other.gameObject.tag == "Check")
        {
            HUD.instance.outOfScene = true;
        }
    }


    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            openBag = !openBag;
            myBag.SetActive(openBag);
        }
    }

    void OpenMyCraft()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            openCraft = !openCraft;
            myCraft.SetActive(openCraft);
        }
    }

    private void ConsumeSP()
    {
        time = 0;
        playerSP.ConsumSP();
    }

    private void AutoRestoreSP()
    {
        time = 0;
        playerSP.RestoreSP();
    }
 
}