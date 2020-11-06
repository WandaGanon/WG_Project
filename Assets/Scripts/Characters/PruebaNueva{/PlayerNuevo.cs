using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNuevo : MonoBehaviour
{
// Informacion Obtenida de https://www.youtube.com/watch?v=FvvTDkJvBfA&list=PLOc9rHNEUHlyryuY0PvipHTXyL2mBij9-
//https://www.youtube.com/watch?v=Dp_MFvT_VIw&list=PLOc9rHNEUHlyryuY0PvipHTXyL2mBij9-&index=2&ab_channel=GamerGarage

    [Header("Variables de configuracion")]
    [Range(-1,1)]
    [Tooltip("Vector Horizontal")]
    public float h;
    [Range(-1,1)]
    [Tooltip("Vector Vertical")]
    public float v;
    public Rigidbody rb;
    private Vector3 playerInput;


    private Vector3 hitNormal;
        
    private Vector3 moverPlayer;
    public CharacterController Player;
    public float Velocidad = 0.1f;
    public float VelocidadCorrer = 2f;
    public float VelocidadAgachado = 0.5f;

    public float gravedad = 9.8f;
    public float slopeForceDown = -10f;
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    public float fallVelocity;
    public float jumpForce;
    public float slideVelocity = 7f;

    [Header("Verificar estado del Player")]
    public bool floor;
    public bool waitIdle;
    public bool agachado;
    public bool jump;
    public bool run;


    public bool isOnSlope = false;
    private Animator anim;
 /*
    public bool firstButtonPressed = true;
    public bool reset = true;

    public  float timeOfFirstButton = 0;
    public  float clickdelay = 0.5f;
     public  float t0, moveSpeed;*/

    [Header("Colicion de la cabeza")]
    public GameObject cabeza;
    public LogicaCabeza logicaCabeza;


/*
    - problemas al bajar las escaleras 
    *solucion posible crear o usar un codigo de pies que si el personaje 
    esta tocando suelo con x profuncidad no realice animacion de caida
    
    -porblema al correr o caminar por los cubos con fisica
    *solucion parecida a la de los escalones.

    -problemas al agacharse y tratar que se mantenga agachado cuando este por debajo de algo
    * Solucion revisar este video https://www.youtube.com/watch?v=a8d_q7sxodI&ab_channel=DonPachi

    -problema que se produce al saltar mirando una pared, muestra animacion de caida.
    *Solucion podria servir solucion de pies.
    
    -Fea animacion de correr a saltar, caer y tocar suelo
    * podria agregarse una transicion adicional.

    -Optimizar el codigo
    * Solo viendo lo util de lo que no y tratando de compactar y documentar mas el codigo

    -------------------------------------------------------
    cosas a rescatar
    el boton E esta para realizar RODAR
    el Correr si lo apretar varias veces verifica si se realizo el doble clic con un mensaje en consola. (Se puede usar a futuro)

*/
    void Start()
    {
        Player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetBool("jump?",true);
        cabeza.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical"); 
        Idle();

        //runRollVerificar();
        agachado = anim.GetBool("bend?");
        jump = anim.GetBool("jump?");
        run = anim.GetBool("run?");

        if ( Input.GetKey(KeyCode.E) )
        {
            anim.SetBool("roll?", true);
        }
        else
        {
            anim.SetBool("roll?", false);
        }

        playerInput = new Vector3(h, 0, v);

        InjectEjesPrincipales(h, v);

        //Es te es un ajuste para que cuando se realiza un velocidad en diagonal no sea superior al maximo
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDireccion();

        moverPlayer = playerInput.x * camRight + playerInput.z * camForward;

        MovimientoJugador();

        Player.transform.LookAt(Player.transform.position + moverPlayer);

        SetGravity();

        PlayerJump();

        Player.Move( moverPlayer * Time.deltaTime);
    }

    void MovimientoJugador(){

        // Velovidad para Correr
        if (Player.isGrounded && Input.GetKey(KeyCode.LeftShift) && !agachado)
        { 
            anim.SetBool("run?", true);
            moverPlayer = moverPlayer * Velocidad * VelocidadCorrer; 

        }
        // Velocidad agachado
        else if(Player.isGrounded && Input.GetKey(KeyCode.LeftControl) || agachado)
        { 
            cabeza.SetActive(true);
            anim.SetBool("bend?", true);
            // Variable que se modifica al personaje con el fin de cambiar su colicion
            Player.height = 0.9f;
            Player.center = new Vector3(0, 0.52f, 0);
            if (Input.GetKey(KeyCode.LeftShift))
            {
            moverPlayer = moverPlayer * Velocidad * VelocidadAgachado * VelocidadCorrer; 
            }
            else{
            moverPlayer = moverPlayer * Velocidad * VelocidadAgachado; 
            }

        }
        // Velocidad Para Caminar
        else{ 
            moverPlayer = moverPlayer * Velocidad; 
            anim.SetBool("run?", false); 
        }
            //Cuando no este agachado
            if(agachado && !Input.GetKey(KeyCode.LeftControl))
            {
                //se realiza pregunta a cabeza si tiene o no algun objeto arriba de este
                if(logicaCabeza.contadorOnTrigger <= 0 ){
                    cabeza.SetActive(false);
                    anim.SetBool("bend?", false);
                    cabeza.SetActive(false);
                    // Variable que se modifica al personaje con el fin de cambiar su colicion
                    Player.height = 1.73f;
                    Player.center = new Vector3(0, 0.88f, 0);
                }

            }
    }

    void camDireccion(){

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y =  0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void PlayerJump(){
        if (Player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !agachado) {
            anim.SetBool("jump?",false);
            fallVelocity = jumpForce;
            moverPlayer.y = fallVelocity;
        }
    }

    void SetGravity(){
        if (Player.isGrounded)
        {
            floor = true; 
            fallVelocity = -gravedad * Time.deltaTime;
            moverPlayer.y = fallVelocity;
            anim.SetBool("jump?",true);
        }
        else
        {
            fallVelocity -= gravedad * Time.deltaTime;
            moverPlayer.y = fallVelocity;
            floor = false; 
        }

        anim.SetBool("floor?",floor);
        SlideDown();
    }

    public void SlideDown(){
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= Player.slopeLimit;

        if (isOnSlope == true)
        {
             moverPlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
             moverPlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

            moverPlayer.y += slopeForceDown;
        }
        anim.SetBool("Slide?",isOnSlope);
    }

    public void  Idle(){
        if(h == 0  && v == 0){ anim.SetBool("waitIdle?", true); }
        else{ anim.SetBool("waitIdle?", false); }

        waitIdle = anim.GetBool("waitIdle?");
    }   

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        hitNormal  = hit.normal;
    }

    public void InjectEjesPrincipales(float x, float y){
        anim.SetFloat("Velocidad_x", x);
        anim.SetFloat("Velocidad_y", y); 
   }

 /*
    public void runRollVerificar()
    {
         if(Input.GetKeyDown(KeyCode.LeftShift) && firstButtonPressed) 
         {
             if(Time.time - timeOfFirstButton < 0.5f) {
                 Debug.Log("DoubleClicked");
             } else {
                 Debug.Log("Too late");
             }
 
             reset = true;
         }
 
         if(Input.GetKeyDown(KeyCode.LeftShift) && !firstButtonPressed) {
             firstButtonPressed = true;
             timeOfFirstButton = Time.time;
         }
 
         if(reset) {
             firstButtonPressed = false;
             reset = false;
         }

    }*/


}
