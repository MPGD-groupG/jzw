using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject player;
    public GameObject visualEffect;
    public static PlayerController instance;
    public float moveSpeed = 2f;
    public float rotateSpeed = 2f;

    // area clamp
    public float minX = -9f;
    public float maxX = 12f;
    public float minZ = -12f;
    public float maxZ = 5f;

    public Vector2 moveValue;
    public float speed = 0.12f;
    private int count;
    private int numPickups = 8;
    public Text scoreText;
    public Text winloseText;
    private float turnSpeed = 14f;
    // public Animation animation;

    // Buff for speed up
    // Check sp
    public bool isSpeedUp;  // Check if the player is running
    public bool canSpeedUp; // Check if the player can run
    public int speedUpConsume = 10; // Running cost 10 SP/s
    public float timeBetweenConsume = 1f;

    private float time;
    public float superTimeVal = 10; // Superpower Duration
    public bool gotSuperpower; // Superpower status

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
        player = GameObject.FindGameObjectWithTag("Player");
        canSpeedUp = true;  // At the beginning the player's stamina value is full
    }

    private void Start()
    {
        count = 0;
        winloseText.text = "";
        //SetCountText();
        //animation = GetComponent<Animation>();
    }

    void Update()
    {
        OpenMyBag();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 Direction = new Vector3(h, 0f, v).normalized;
        if (Direction.magnitude>=0.1f)
        {
            float targetAngle = Mathf.Atan2(Direction.x, Direction.y)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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

    //void OnMove(InputValue value)
    //{
    //    moveValue = value.Get<Vector2>();
    //}
    //void Onfire(InputValue value)
    //{
    //    animation.Play();
    //}

    void FixedUpdate()
    {

        //Clamp();

        //Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        //GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
        //transform.position = new Vector3(moveValue.x*speed* Time.fixedDeltaTime, 0.0f, moveValue.y * speed * Time.fixedDeltaTime);
        //transform.position += new Vector3(moveValue.x, 0.0f, moveValue.y)*speed;
        //transform.Rotate(new Vector3(moveValue.x, 0.0f, moveValue.y*90),Space.Self);
        //if(moveValue.x!=0||moveValue.y!=0)
        //{
        //    Rotating(moveValue.x, moveValue.y);
        //}

        //if (gotSuperpower)
        //{
        //    // Speed up by not consuming stamina value
        //    speed = 0.3f;   // Move with superpower speed
        //    visualEffect.SetActive(true); // Character effects display

        //    superTimeVal -= Time.deltaTime;
        //    if (superTimeVal <= 0)  // Can only have superpowers during superpower time
        //    {
        //        visualEffect.SetActive(false);
        //        gotSuperpower = false;
        //        superTimeVal = 10;
        //    }

        //    time = time + Time.deltaTime;
        //}
        //else
        //{
        //    speed = 0.1f;
        //}

        }

    /*private void Clamp()
    {
        float xClamp = Mathf.Clamp(transform.position.x, minX, maxX);
        float zClamp = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(xClamp, transform.position.y, zClamp);
    }*/

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

    /*    void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PickUp")
            {
                // other.gameObject.SetActive(false); // If set false here, the items can't be generated in the prop bar.
                playerTouchedOnce++;
                if (playerTouchedOnce == 1)
                {
                    count++;
                    SetCountText();
                }


            }
            //check if player out of scene
            if (other.gameObject.tag == "Check")
            {
                winloseText.text = "Dead....";
                Invoke("Restart", 2.0f);
            }
        }


        private void SetCountText()
        {
            //win check
            scoreText.text = "Score: " + count.ToString();
            if(count >= numPickups)
            {
                winloseText.text = "Win!";
                Invoke("AlreadyWin", 0.0f);

            }
        }

        void AlreadyWin()//to avoid wrong win/lose check
        {
            gameObject.SetActive(false);
            Invoke("Restart", 2f);
        }

        /*    void Restart()
            {
                //reset game 
                SceneManager.LoadScene(0);
            }*/



    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }
    }
}