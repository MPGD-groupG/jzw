
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
     private void DestoryBullet()
     {
         Destroy(this.gameObject, 0.05f);
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
            other.gameObject.GetComponent<PlayerHP>().TakeDamage(10);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}