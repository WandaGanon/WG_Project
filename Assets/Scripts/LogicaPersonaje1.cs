using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class LogicaPersonaje1 : MonoBehaviour
{
    [Header("Variables de configuracion")]
    [Range(1,20)]
    [Tooltip("Velocidad de caminar")]
    public float velocidadCaminar = 5.0f;
    [Tooltip("Multiplicador de caminar x correr, ejemplo ( 2 x 5.f )")]
    [Range(1,20)]
    public float velocidadCorrerX = 2.0f;
    [Tooltip("Multiplicador de caminar x agacharce, ejemplo ( 2 x 5.f )")]
    [Range(1,20)]
    public float velocidadAgachadoX = 0.5f;
    [Tooltip("configuracion de rotacion")]
    [Range(1,400)]
    public float rotar = 200.0f;
    [Tooltip("La intensidad del salto")]
    [Range(1,20)]
    public float ForceJump = 8f;
    [Tooltip("Valor Eje X")]
    [Range(-1,1)]
    public float x;
    [Tooltip("Valor Eje Y")]
    [Range(-1,1)]
    public float y;
    [Header("Variables de Animacion")]

    public bool Can_jump;
    public bool waitIdle;

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
        InjectEjesPrincipales(x,y);
        //Verifica si el personaje se mueve o no
        Idle();
        //Insertar informacion en Animator de "Run?"
        Corriendo_Update();
        //Insertar informacion en Animator de "Crouched?"
        Agacharce_Update();
        //Insertar informacion en Animator de Salto y Caida
        JumpAndFall();
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
    
    public void  Idle(){
        if(x == 0  && y == 0){ anim.SetBool("waitIdle?", true);
           waitIdle = anim.GetBool("waitIdle?");
        }
        else{ anim.SetBool("waitIdle?", false);
           waitIdle = anim.GetBool("waitIdle?");
        }
    }     

    public void JumpAndFall(){
                if (Can_jump) { Saltar(); } else {  Falling(); }
    }
   public void InjectEjesPrincipales(float x, float y){
        anim.SetFloat("Velocidad_x", x);
        anim.SetFloat("Velocidad_y", y); 
   }
// Mejoras para FixedUpdate

    public void  Corriendo_FixedUpdate(){
        if (Input.GetKey(KeyCode.LeftShift)) {transform.Translate(0 , 0, y * Time.deltaTime * velocidadCaminar * velocidadCorrerX); }
        else{  transform.Translate(0 , 0, y * Time.deltaTime * velocidadCaminar); }
    }
    
    public void  Agacharce_FixedUpdate(){
        if (Input.GetKey(KeyCode.LeftControl)) { transform.Translate(0 , 0, y * Time.deltaTime * velocidadCaminar * velocidadAgachadoX); }
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
