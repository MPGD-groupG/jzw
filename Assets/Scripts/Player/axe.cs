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
    void OnFire(InputValue value)
    {
        anim.SetTrigger("att");
        StartCoroutine(StartAttack());

        GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        //anim.Play("AxeAnim");
        //anim.Play("attack");
        Debug.Log("att");
        
       
        


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
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.GetComponent<EnemyAI1>().TakeDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
