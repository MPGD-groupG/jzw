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
            other.gameObject.SetActive(false);
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