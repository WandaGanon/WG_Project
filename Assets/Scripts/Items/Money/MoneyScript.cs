using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider Player) 
    {
        //tambien se puede usar enves de gameObject.name, gameObject.tag pero seteando objetos a tag
        if (gameObject.name == "Coins")
        {
            CoinAmount.coinAmount += 1;
            Destroy (gameObject);
        }
        if (gameObject.name == "Gems")
        {
            GemAmount.gemAmount += 1;
            Destroy (gameObject);
        }        
    }
}
