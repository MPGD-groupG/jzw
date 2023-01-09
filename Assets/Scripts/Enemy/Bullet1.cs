
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public int bullet1Damage = 10; 

     private void DestoryBullet()
     {
         Destroy(this.gameObject, 0.05f);
     }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//clean bullet when hit player
        {
            other.gameObject.GetComponent<PlayerHP>().TakeDamage(bullet1Damage);
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Ground")//clean bullet when bullet hit the ground
        {
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}