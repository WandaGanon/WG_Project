using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject _GroundCheck;
    public float distanceGround;
    public float distanceGroundMAX;
    public bool onGround;
    private Animator anim;
    private void Update()
    {
        distanceGround = GetComponent<Collider>().bounds.extents.y + 1f;
    }
    void OnGrounded()
    {
        //Limitador de distancia entre piso y personaje para minimizar caidas pequeñas
        if (distanceGround <= distanceGroundMAX)
       {
            onGround = true;
            anim.SetBool("OnFloor?",true);
            print("esta tocando piso");
       }
       else
       {
           onGround = false;
           anim.SetBool("OnFloor?",false);
           print("NO esta tocando piso");
       }
    }
}
