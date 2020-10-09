using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSup : MonoBehaviour
{
    //SCRIPTS PARA AYUDAR A LOS OBJETOS A HACER ACCIONES SIMPLES
    void Update()
    {
        RotationItemY();
    }
    public void RotationItemY()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
}
