using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPersonaje1 : MonoBehaviour
{
    //correr
    public int velCorrer;
    public bool estoyAgachado;
    //mover
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 250.0f;
    private Animator anim;
    public float x, y;

    //salto
    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;
    //agacahar
    public float velocidadInicial;
    public float velocidadAgachado;

    //golpe
    public bool estoyAtacando;
    public bool avanzoSolo;
    public float impulsoDeGolpe = 10f;

    //gravedad
    public int fuerzaExtra = 0;
    //raytrace
    public LayerMask layers;
    float rangoVision = 30;
    float max ;
    
    RaycastHit hit;

    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();

        velocidadInicial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento * 0.5f;
    }

    void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
        }

        if(avanzoSolo)
        {
            rb.velocity=transform.forward * impulsoDeGolpe;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !estoyAgachado && puedoSaltar && !estoyAtacando)
        {
            velocidadMovimiento = velCorrer;
            if (y > 0)
            {
                anim.SetBool("correr", true);
            }
            else
            {
                anim.SetBool("correr", false);
            }
        }
        else
        {
            anim.SetBool("correr", false);

            if (estoyAgachado)
            {
                velocidadMovimiento = velocidadAgachado;
            }
            else if (puedoSaltar)
            {
                velocidadMovimiento = velocidadInicial;
            }
        }

        //
        
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Return) && puedoSaltar && !estoyAtacando)
        {
            anim.SetTrigger("golpeo");
            estoyAtacando = true;
        }

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        DibujarRayo(hit, max);

        if(puedoSaltar)
        {
            if (!estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("salte", true);
                    rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
                }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    anim.SetBool("agachado", true);
                     //velocidadMovimiento = velocidadAgachado;

                    // cambio de collider
                   // colAgachado.enabled = true;
                    //colParado.enabled = false;

                   // cabeza.SetActive(true);
                   // estoyAgachado = true;
                }
                
                else
                {
                    anim.SetBool("agachado", false);
                    //velocidadMovimiento = velocidadInicial;
                }
            }
            anim.SetBool("tocoSuelo", true);
        }
        else
        {
            if ( hit.distance >= 0.12)
            {
            EstoyCayendo();
            }
            else if ( hit.distance <= 0.0084629){
            EstoyCayendo();
            Debug.Log(hit.distance);
            Debug.Log(hit.distance);
            }

        }
    }
    public void EstoyCayendo()
    {
        //caer rapido
        rb.AddForce(fuerzaExtra * Physics.gravity);


        anim.SetBool("tocoSuelo", false);
        anim.SetBool("salte", false);
    }
    public void DejeDeGolpear()
    {
        estoyAtacando = false;
    }
    public void AnavanzoSolo()
    {
        avanzoSolo = true;
    }
    public void DejoDeAvanzar()
    {
        avanzoSolo = false;
    }

    void DibujarRayo(RaycastHit hit,  float max)
    {
        // Vector3 direccion = new Vector3(Mathf.Cos(angulo * Mathf.Deg2Rad), 0, 0);

        if (Physics. Raycast(transform.position, Vector3.down, out hit, rangoVision, layers))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            //Debug.Log(hit.distance);

            if(hit.distance > max){
                Debug.Log(hit.distance);
                max = hit.distance; 
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * rangoVision, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
