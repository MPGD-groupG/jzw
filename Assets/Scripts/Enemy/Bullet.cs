
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 22;

    private void DestoryBullet()//clean bullet
     {
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//clean bullet when hit player
        {
            other.gameObject.GetComponent<PlayerHP>().TakeDamage(bulletDamage);
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Obstacle")//clean bullet when bullet hit the ground
        {
            Invoke(nameof(DestoryBullet), 2.0f);
        }
    }
}