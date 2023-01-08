
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 22;

    /* void Start()
     {
         Destroy(this.gameObject, 3);

     }

     void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.tag == "Player")
         {
             PlayerHP playerHP = getComponet<PlayerHP>();
             playerHP -= 15;
             Invoke(nameof(DestoryBullet), 0.2f);
         }
     }*/

    private void DestoryBullet()
     {
        this.gameObject.SetActive(false);
    }
    void Start()
    {
        //Destroy(this.gameObject, 4.0f);

    }

    /*private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<PlayerHP>().TakeDamage(18);
        Destroy(this.gameObject);
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHP>().TakeDamage(bulletDamage);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Invoke(nameof(DestoryBullet), 2.0f);
            //Destroy(this.gameObject);
        }
    }
}