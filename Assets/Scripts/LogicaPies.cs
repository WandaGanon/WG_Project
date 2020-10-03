using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{
    public LogicaPersonaje1 data1;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay(Collider other) 
    {
        data1.Can_jump = true;
    }
    private void OnTriggerExit(Collider other) 
    {
        data1.Can_jump = false;
    }
}
