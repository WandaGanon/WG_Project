using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassObject : MonoBehaviour
{

    private void Start() {
        var dara = GetComponent<Transform>();
        var rigidbody = GetComponent<Rigidbody>();
        float masa = dara.localScale.x * dara.localScale.y * dara.localScale.z;
        rigidbody.mass = masa;
    }

}
