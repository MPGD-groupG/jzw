using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class axe : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
       
    }
    void OnFire(InputValue value)
    {

        
        GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        //anim.Play("AxeAnim");
        //anim.Play("attack");
        Debug.Log("att");
        anim.SetTrigger("att");
        


    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
