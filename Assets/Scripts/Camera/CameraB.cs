using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraB : MonoBehaviour
{
    public Transform player;
    public GameObject asso; 
    public float spinSpeed = 512;
    public float scale_mul = 128;
    public float Yaxis_mul = 0.02f;
    private float turnSpeed = 14f;
    public float Pitch { get; private set; }
    public float Yaw { get; private set; }
    Vector3 setoff; 
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        setoff = transform.position - player.position; 
    }

    // Update is called once per frame
    void Update()
    {
        float scale = Input.GetAxis("Mouse ScrollWheel");
        if (scale > 0 && Camera.main.fieldOfView < 100 || scale < 0 && Camera.main.fieldOfView > 50)
        {
            Camera.main.fieldOfView += scale * scale_mul;
        }

        transform.position = player.transform.position + setoff;

        /*if (!Input.GetMouseButton(1))
        {
            float X = Input.GetAxis("Mouse X");
            Quaternion spin = Quaternion.AngleAxis(X * spinSpeed * Time.deltaTime, player.up);
            transform.position = spin * setoff + player.position;

            /*float Y = Input.GetAxis("Mouse Y");
            float Yincrement = -Y * Yaxis_mul;
            if (Y > 0 && transform.position.y + Yincrement > 16.8 || Y < 0 && transform.position.y + Yincrement < 21.2)
            {
                transform.position += new Vector3(0, Yincrement, 0);
            }
            void Rotating(float h, float v)
            {
                Vector3 targetDir = new Vector3(h, 0, v);

                // facing
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }
        }*/

        if (Input.GetMouseButton(1))
        {
            float X = Input.GetAxis("Mouse X");
            Quaternion spin = Quaternion.AngleAxis(X * spinSpeed * Time.deltaTime, player.up);
            transform.position = spin * setoff + player.position;

            float Y = Input.GetAxis("Mouse Y");
            float Yincrement = -Y * Yaxis_mul;
            if (Y > 0 && transform.position.y + Yincrement > 16.8 || Y < 0 && transform.position.y + Yincrement < 21.2)
            {
                transform.position += new Vector3(0, Yincrement, 0);
            }
        }

        setoff = transform.position - player.position;
        transform.LookAt(asso.transform.position);
    }
}

