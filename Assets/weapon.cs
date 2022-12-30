using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class weapon : MonoBehaviour
{
    public MeshCollider coll3D;
    public Animator anim;
    public float time;
    public int damage;
    public float StartTime;
    public Animator anim1;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll3D = GetComponent<MeshCollider>();
    }
    //void OnFire(InputValue value)
    //{
    //    anim.SetTrigger("att");
    //    StartCoroutine(StartAttack());

    //    GetComponent<Animator>();
    //    
    //    
    //    
    //    Debug.Log("att");
    void Update()
    {
        Attack();
    }





    //}
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
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
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);

        }
    }

    // Update is called once per frame

}
