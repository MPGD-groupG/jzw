using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody rigidBody;
    public GameObject visualEffect;
    Vector3 newMovePosition;
    public static PlayerController instance;
    // public float moveSpeed = 10f;
    public float rotateSpeed = 2f;
    public float g = -9.81f;
    Vector3 gVelocity;


    public Vector2 moveValue;
    public float speed = 0.12f;
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




    public bool gotSpeedUpPower; // Superpower status

    // Check once trigger
    private int playerTouchedOnce = 0;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    // Inventory
    public GameObject myBag;
    bool isOpen;



    private void Awake()
    {
        instance = this;
        rigidBody = GetComponent<Rigidbody>();

        playerSP = GetComponent<PlayerSP>();
        canSpeedUp = true;  // At the beginning the player's stamina value is full
        
        var enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyTransform = enemy.transform;

    }

    private void Start()
    {
        
        count = 0;
        winloseText.text = "";
        //SetCountText();
        //animation = GetComponent<Animation>();
    }

    private void Update()
    {
        OpenMyBag();
    }


    void FixedUpdate()
    {
        // OpenMyBag();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 Direction = new Vector3(h, 0f, v);

        
        if (Direction.magnitude>0)
        {
            float targetAngle = Mathf.Atan2(Direction.x, Direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);

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
                    ConsumeStamina();
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
                    RestoreStamina();
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
                    RestoreStamina();
                }

            }

/*            if (isSpeedUp)
            {
                Debug.Log("got super power");
                // Speed up by not consuming stamina value
                moveSpeed = 20f;   // Move with superpower speed
                visualEffect.SetActive(true); // Character effects display

                superTimeVal -= Time.deltaTime;
                if (superTimeVal <= 0)  // Can only have superpowers during superpower time
                {
                    visualEffect.SetActive(false);
                    gotSpeedUpPower = false;
                    superTimeVal = 10;
                }

                time = time + Time.deltaTime;
            }
            else
            {
                moveSpeed = walkSpeed;
            }*/

            newMovePosition = moveDir * Time.deltaTime * moveSpeed;

            if (this.ObstacleDetect())   // Obstruction detected, object does not move
            {
                newMovePosition = - newMovePosition; // 
            }

            rigidBody.MovePosition(rigidBody.position + newMovePosition); 

            //rigidBody.MovePosition(rigidBody.position+moveDir * Time.deltaTime * moveSpeed); // Eliminate jitter
            //transform.Translate(moveDir*Time.deltaTime*moveSpeed,Space.World);
            //controller.Move(moveDir * moveSpeed * Time.deltaTime);
            //gVelocity.y+=g* Time.deltaTime;
            //controller.Move(gVelocity * Time.deltaTime);
            //float y = Camera.main.transform.rotation.eulerAngles.y;
            //targetDirection = Quaternion.Euler(0, y, 0) * targetDirection;//这里控制移动朝向，有bug
            //targetDirection = Quaternion.Euler(0, 0, 0) * targetDirection;
            //controller.Move(moveDir * moveSpeed * Time.deltaTime);
            //transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);
            //transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed, Space.World);
        }
        /*if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * rotateSpeed);
        }*/
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


    //void Rotating(float h,float v)
    //{
    //    Vector3 targetDir = new Vector3(h, 0, v);

    //    // facing
    //    Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);

    //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    //}

    void OnTriggerEnter(Collider other)
    {

        // Check if player out of scene
        if (other.gameObject.tag == "Check")
        {
            UIManager.instance.checkScene();
        }
    }


    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }
    }

    private void ConsumeStamina()
    {
        time = 0;
        playerSP.ConsumSP();
    }

    private void RestoreStamina()
    {
        time = 0;
        playerSP.RestoreSP();
    }


}