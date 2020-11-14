using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaCabeza : MonoBehaviour
{
    public int contadorOnTrigger;

    void OnTriggerEnter(Collider other) 
    {
        contadorOnTrigger++;
    }
    void OnTriggerExit(Collider other) 
    {
        contadorOnTrigger--;
    }
}
