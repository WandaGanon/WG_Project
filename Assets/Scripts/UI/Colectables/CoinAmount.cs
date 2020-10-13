using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAmount : MonoBehaviour
{
    // El Text sirve para enlazar la informacion numerica con el UI de Text cosa que se muestre en la pantalla
    Text CoinNumbers;
    public static int coinAmount;
    void Start()
    {
        CoinNumbers = GetComponent<Text> ();
    }
    void Update()
    {
        CoinNumbers.text = coinAmount.ToString();
    }
}
