using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{
    public LogicaPersonaje1 pies;
    void Start()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay(Collider other) 
    {
        pies.Can_jump = true;
    }
    private void OnTriggerExit(Collider other) 
    {
        pies.Can_jump = false;
    }
}
