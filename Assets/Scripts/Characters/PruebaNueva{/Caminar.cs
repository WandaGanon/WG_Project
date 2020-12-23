using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminar : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    private Animator anim;

    void Start(){

    anim = GetComponent<Animator>();

    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Idle(); 

    }

    public void  Idle()
    {
        if(Horizontal == 0  && Horizontal == 0){ 
            anim.SetBool("waitIdle?", true); 
        }
        else{ 
            anim.SetBool("waitIdle?", false);
        }
    }  

}
