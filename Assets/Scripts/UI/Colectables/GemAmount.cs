using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemAmount : MonoBehaviour
{
    // El Text sirve para enlazar la informacion numerica con el UI de Text cosa que se muestre en la pantalla
    Text GemNumbers;
    public static int gemAmount;
    void Start()
    {
        GemNumbers = GetComponent<Text> (); 
    }
    void Update()
    {
        GemNumbers.text = gemAmount.ToString(); 
    }
}
