using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class axe : MonoBehaviour
{
    public MeshCollider coll3D;
    public Animator anim;
    public float time;
    public int damage;
    public float StartTime;
    void Start()
    {
        coll3D = GetComponent<MeshCollider>();
    }
    
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("att");
                StartCoroutine(StartAttack());

               GetComponent<Animator>();
               
               Debug.Log("att");
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(StartTime);
        coll3D.enabled = true;
        StartCoroutine(disableHitbox());
    }
    IEnumerator disableHitbox()
    {
        yield return new WaitForSeconds(time);
        coll3D.enabled = false;
    }
}
