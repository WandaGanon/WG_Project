using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        CointAmount.coinAmount += 1;
        Destroy (gameObject);
    }
}
