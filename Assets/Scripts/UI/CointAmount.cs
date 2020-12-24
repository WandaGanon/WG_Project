using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CointAmount : MonoBehaviour
{
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
