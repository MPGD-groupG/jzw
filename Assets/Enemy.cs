using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int CurrentHP;
    public int damage;

    public void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        //if (!isServer) return;
        CurrentHP -= damage;
        Debug.Log("hp--");
        //ShowHPSlider();
        if (CurrentHP <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.1f);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
