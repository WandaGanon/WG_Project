using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public GameObject _characterStats;
    public float _Gravedad;
    public float _JumpForce;
     void Awake()
    {
        
        Debug.Log(_characterStats);
    }
     void OnTriggerStay(Collider other)
    {
        _Gravedad = _characterStats.GetComponent<PlayerNuevo>().gravedad;
        _JumpForce = _characterStats.GetComponent<PlayerNuevo>().jumpForce;
        _Gravedad = 1000f;
        _JumpForce = 25f;
        Debug.Log("ENTROOOOOO");
    }
     void OnTriggerExit(Collider other)
    {
        _Gravedad = _characterStats.GetComponent<PlayerNuevo>().gravedad;
        _JumpForce = _characterStats.GetComponent<PlayerNuevo>().jumpForce;
        _Gravedad = 9.8f;
        _JumpForce = 8.52f;
        Debug.Log("SALIIIIIIIIIIIO");
    }
}
