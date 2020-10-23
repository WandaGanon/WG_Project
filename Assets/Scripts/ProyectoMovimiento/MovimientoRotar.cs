using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRotar : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float maxSpeed;
    public GameObject referencia;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        rb.AddForce( V * referencia.transform.forward * speed );
        rb.AddForce( H * referencia.transform.right * speed );




    }
}
