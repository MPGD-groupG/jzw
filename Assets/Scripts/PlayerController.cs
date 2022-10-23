using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Vector2 moveValue;
    public float speed;
    private int count;
    private int numPickups = 3;
    public Text scoreText;
    public Text winText;
    private float turnSpeed = 14f;
    //public Animation animation;

   

    public float speedWalk = 6f;
    public float speedRun = 12.0f;
    public float speedSuper = 18.0f;

    public bool isSpeedUp;  // Check if the player is running
    public bool canSpeedUp; // Check if the player can run

    public GameObject player;

    public int speedUpConsume = 10; // Running cost 10 SP/s
    public float timeBetweenConsume = 1f;
    private float time;

    private float superTimeVal = 10; // Superpower Duration
    public bool gotSuperpower; // Superpower status

    public GameObject visualEffect;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");

        canSpeedUp = true;  // At the beginning the player's stamina value is full



    }

    private void Start()
    {
        count = 0;
        winText.text = "";
        SetCountText();
        //animation = GetComponent<Animation>();
    }
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }
    //void Onfire(InputValue value)
    //{
    //    animation.Play();
    //}

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        //GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
        //transform.position = new Vector3(moveValue.x*speed* Time.fixedDeltaTime, 0.0f, moveValue.y * speed * Time.fixedDeltaTime);
        transform.position += new Vector3(moveValue.x, 0.0f, moveValue.y)*speed;
        //transform.Rotate(new Vector3(moveValue.x, 0.0f, moveValue.y*90),Space.Self);
        if(moveValue.x!=0||moveValue.y!=0)
        {
            Rotating(moveValue.x, moveValue.y);
        }

        if (gotSuperpower)
        {
            // Speed up by not consuming stamina value
            //speed = speedSuper;   // Move with superpower speed
            visualEffect.SetActive(true); // Character effects display

            superTimeVal -= Time.deltaTime;
            if (superTimeVal <= 0)  // Can only have superpowers during superpower time
            {
                visualEffect.SetActive(false);
                gotSuperpower = false;
                superTimeVal = 10;
            }

            time = time + Time.deltaTime;
        }

        }
    void Rotating(float h,float v)
    {
        Vector3 targetDir = new Vector3(h, 0, v);

        // 传入一个向量值使物体朝向向量方向
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            //other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if(count >= numPickups)
        {
            winText.text = "Win!";
        }
    }
}