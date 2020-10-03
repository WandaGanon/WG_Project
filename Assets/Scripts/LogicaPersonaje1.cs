using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPersonaje1 : MonoBehaviour
{
    //Velocidad de caminar
    public float velocidadCaminar = 5.0f;
    //Multiplicador de correr x caminar ( 2 x 5.f )
    public float velocidadCorrerX = 2.0f;
    public float velocidadAgachadoX = 0.5f;
    public float velocidadAgachadoCorrerX = 0.5f;
    public float rotar = 200.0f;
    public float x,y;
    public float ForceJump = 8f;
    public bool Can_jump;
    private Animator anim;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Can_jump = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener informacion de Eje X y Y del Axes
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        //Insertar informacion en Animator de los eje X y Y
        anim.SetFloat("Velocidad_x", x);
        anim.SetFloat("Velocidad_y", y); 
        //Insertar informacion en Animator de "Run?"
        Corriendo_Update();
        //Insertar informacion en Animator de "Crouched?"
        Agacharce_Update();
        //Insertar informacion en Animator de salto y caida
        if (Can_jump) { Saltar(); } else {  Falling(); }
    }

    private void FixedUpdate() {
        //Configuracion de la Rotacion de personaje
        transform.Rotate(0, x * Time.deltaTime * rotar, 0); 
        //Configuracion de la Velocidad cuando corra el personaje
        Corriendo_FixedUpdate();
       //Configuracion de la Velocidad cuando se agache el personaje
        Agacharce_FixedUpdate();
        
    }

// Mejoras para Update

    public void  Corriendo_Update(){
        if (Input.GetKeyDown(KeyCode.LeftShift)) { anim.SetBool("Run?", true); }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { anim.SetBool("Run?", false); }
    }
    
    public void  Agacharce_Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl)) { anim.SetBool("Crouched?", true); }
        if (Input.GetKeyUp(KeyCode.LeftControl)) { anim.SetBool("Crouched?", false); }
    }
    
// Mejoras para FixedUpdate

    public void  Corriendo_FixedUpdate(){
        if (Input.GetKeyDown(KeyCode.LeftShift)) { transform.Translate(0 , 0, y * Time.deltaTime * velocidadCaminar * velocidadCorrerX); }
        else{  transform.Translate(0 , 0, y * Time.deltaTime * velocidadCaminar); }
    }
    public void  Agacharce_FixedUpdate(){
        if (Input.GetKeyDown(KeyCode.LeftControl)) { transform.Translate(0 , 0, y * Time.deltaTime * velocidadCaminar * velocidadAgachadoX); }
        else{  transform.Translate(0 , 0, y * Time.deltaTime * velocidadCaminar); }
    }

// funciones de Salto
    public void Saltar(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
        anim.SetBool("Jump?", true);
        rb.AddForce(new Vector3(0,ForceJump,0),ForceMode.Impulse );
        }
        anim.SetBool("Suelo?", true);
    }

// Funcion de caida
    public void Falling(){
        anim.SetBool("Suelo?", false);
        anim.SetBool("Jump?", false);
    }
}
