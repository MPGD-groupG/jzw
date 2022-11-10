
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
     }

     private void DestoryBullet()
     {
         Destroy(this.gameObject);
     }*/
    void Start()
    {
        Destroy(this.gameObject, 2.0f);

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
            other.gameObject.GetComponent<PlayerHP>().TakeDamage(22);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}