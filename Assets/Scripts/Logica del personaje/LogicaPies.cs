using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{
    public int OnFloorOnTrigger;

    void OnTriggerEnter(Collider other) 
    {
        OnFloorOnTrigger++;
    }
    void OnTriggerExit(Collider other) 
    {
        OnFloorOnTrigger--;
    }
  /*
    PlayerNuevo pies;
    void Start()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay(Collider other) 
    {
        Debug.Log(other);
        //pies.suelo_real = true;
    }
    private void OnTriggerExit(Collider other) 
    {
      //  pies.suelo_real = false;
    }

    private void OnTriggerStay(Collider other) 
    {
        pies.Can_jump = true;
    }
    private void OnTriggerExit(Collider other) 
    {
        pies.Can_jump = false;
    }
    */
}
