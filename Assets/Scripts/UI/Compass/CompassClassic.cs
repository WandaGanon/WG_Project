using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassClassic : MonoBehaviour
{
    //CompassClassic elementos
    public Transform player;
    Vector3 vector;
    void Update()
    {
        CompassClassicF();
    }
    public void CompassClassicF()
    {
        //rota en Z la imagen segun la posicion de Y del jugador
        vector.z = player.eulerAngles.y;
        transform.localEulerAngles = vector;
    }
}
