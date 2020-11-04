using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{
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
}
