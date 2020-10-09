using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayItems : MonoBehaviour
{
    public GameObject Items;
    public Transform[] allchildren;

    // Update is called once per frame
    void Update()
    {
        Items = GameObject.Find("/Items");
        allchildren = this.gameObject.GetComponentsInChildren<Transform>();
    }
    
}
