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
    public bool onGround;
    public bool waitIdle;
    public bool agachado;
    public bool jump;
    public bool run;
    public bool OnGroundAngles_0;
    public bool OnGroundFloor;

    public bool isOnSlope;
    private Animator anim;

    [Header("Colicion de la cabeza")]
    public GameObject cabeza;
    public LogicaCabeza logicaCabeza;
    [Header("Colicion de Piso")]
    public float distanceGround;
    public float distanceGroundMAX;

    public float distanceAngle = 1.0f; //  distancia del raycast hacia abajo, entre transform.position y el objeto de abajo
    public LayerMask hitMask; // En que capa el raycast esta funcionando
    [SerializeField]
    private float slopeForce;
    [SerializeField]
    private float slopeForceRayLengh;
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;
    /*
    Start:
    */
    void Start()
    {
        setingData();
    }
    /*
    Update: 
    */
    void Update()
    {
        Idle();
        PlayerMovement();
        SetGravity();
        PlayerJump();

        Player.Move( moverPlayer * Time.deltaTime);

        getAnim();
        betterment();
    }
    // ----------------------------------------------- Movimiento ------------------------------------------------ //
    /*
    Idle: Analiza el codigo cuando el player no realiza ningun movimiento
    */
    public void  Idle()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        anim.SetFloat("Velocidad_x", h);
        anim.SetFloat("Velocidad_y", v); 

        if(h == 0  && v == 0){ anim.SetBool("waitIdle?", true); }
        else{ anim.SetBool("waitIdle?", false); }
    }  

    /*
    PlayerMovement: Analiza el codigo cuando el player  realiza ningun movimiento, y este esta segmentado en 3 partes Caminar, Correr y agacharce
    */
    void PlayerMovement()
    {
        GroundDetails();

        playerInput = new Vector3(h, 0, v);
        //Este es un ajuste para que cuando se realiza un velocidad en diagonal no sea superior al maximo
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        camDireccion();
        moverPlayer = playerInput.x * camRight + playerInput.z * camForward;
        Player.transform.LookAt(Player.transform.position + moverPlayer);


        if(h != 0  && v != 0 || h == 0  && v != 0 || h != 0  && v == 0)
        {
            walkPlayer();
            runPlayer();    
            bendPlayer();
        }
    }
    /*
    walkPlayer:
    */
    void walkPlayer()
    {
        // Velocidad Para Caminar
        if (Player.isGrounded && !agachado)
        {
            anim.SetBool("run?", false); 
            moverPlayer = moverPlayer * Velocidad; 
        }
        if(!jump)
        {
            moverPlayer = moverPlayer * Velocidad; 
        }
    }
    /*
    runPlayer: 
    */
    void runPlayer()
    {
        // Velovidad para Correr
        if (Player.isGrounded && Input.GetKey(KeyCode.LeftShift) && !agachado)
        { 
            anim.SetBool("run?", true);
            moverPlayer = moverPlayer * VelocidadCorrer; 
        }
        if(!jump  && Input.GetKey(KeyCode.LeftShift)){
            moverPlayer = moverPlayer * VelocidadCorrer; 
        }
    }
    /*
    bendPlayer: Cuando este agachado
    */
    void bendPlayer() 
    {
        if(Player.isGrounded && Input.GetKey(KeyCode.LeftControl) || agachado)
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
            else
            {
                moverPlayer = moverPlayer * Velocidad * VelocidadAgachado; 
            }
        }
        //Cuando no este agachado
        if(agachado && !Input.GetKey(KeyCode.LeftControl))
            {
                //se realiza pregunta a cabeza si tiene o no algun objeto arriba de este
                if(logicaCabeza.contadorOnTrigger <= 0 )
                {
                    cabeza.SetActive(false);
                    anim.SetBool("bend?", false);
                    // Variable que se modifica al personaje con el fin de cambiar su colicion
                    Player.height = 1.73f;
                    Player.center = new Vector3(0, 0.95f, 0);
                }
            }
    }
    // ----------------------------------------------- Camara ----------------------------------------------------//
    /*
    camDireccion: funcion a coloca la camara atras el
    */
    void camDireccion(){

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y =  0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    // ----------------------------------------------- Salto -----------------------------------------------------//
    /*
    PlayerJump
    */
    void PlayerJump()
    {
        if (Player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !agachado) 
        {
            anim.SetBool("jump?",false);
            fallVelocity = jumpForce;
            moverPlayer.y = fallVelocity;
        }
    }
    // ----------------------------------------------- Deslizar --------------------------------------------------//
    /*
    SlideDown
    */
    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= Player.slopeLimit;

        if (isOnSlope == true)
        {
             moverPlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
             moverPlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

            moverPlayer.y += slopeForceDown;
        }
        anim.SetBool("Slide?",isOnSlope);
    }    
    /*
    OnControllerColliderHit
    */
    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        hitNormal  = hit.normal;
    }
    // ----------------------------------------------- Gravedad --------------------------------------------------//
    /*
    SetGravity
    */
    void SetGravity()
    {
        if (Player.isGrounded)
        {
            onGround = true; 
            fallVelocity = -gravedad * Time.deltaTime;
            moverPlayer.y = fallVelocity;
            anim.SetBool("jump?",true);
        }
        else
        {
            onGround = false; 
            fallVelocity -= gravedad * Time.deltaTime;
            moverPlayer.y = fallVelocity;
        }
        anim.SetBool("OnFloor?",onGround);
        SlideDown();
    }
    // ----------------------------------------------- Inyeccion de informacion a Anim -----------------------------------------------------//
    /*
    GroundDetails
    */
    void GroundDetails()
    {
       OnGrounded();
       stepClimb();
       OnGroundAngles_0 = OnGroundAngles();
    }
        /* private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Stairs") && jump)
        {
            gravedad = 1000f;
            Debug.Log("ENTROOOOOO");
        }
        if (other.CompareTag("Stairs") && !jump)
        {
            gravedad = 9.8f;
            jumpForce = 8.52f;
            Debug.Log("ASDASDASDASD");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stairs"))
        {
            gravedad = 9.8f;
            jumpForce = 8.52f;
            Debug.Log("SALIIIIIIIIIIIO");
        }
        else{}
    } */
    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
       stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }
    void stepClimb()
    {
        //si el personaje no esta en un angulo de 0 y esta en tierra aumenta su gravedad y fuerza de salto para contrarrestar la inclinacion
        if (!OnGroundAngles_0 && Player.isGrounded)
        {
            isOnSlope = true;
            jumpForce = 25f;
            gravedad = 1000f;
            if(Player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !agachado)
            {
                isOnSlope = false;
                gravedad = 9.8f;
                jumpForce = 8.52f;
            }
        }
        //si el personaje esta en angulo 0 en tierra o en aire, su gravedad sera normal
        if (OnGroundAngles_0 && Player.isGrounded || OnGroundAngles_0 && !Player.isGrounded)
        {
            gravedad = 9.8f;
            jumpForce = 8.52f;
        }
        if (Player.isGrounded && OnGroundAngles_0)
        {
            Debug.Log("PEROPORQUEEEE");
            onGround = true;
            jumpForce = 25f;
            gravedad = 1000f;
            RaycastHit hitLower;
            RaycastHit hitLower45;
            RaycastHit hitLowerMinus45;
            if(Player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !agachado)
            {
                gravedad = 9.8f;
                jumpForce = 8.52f;
            }
            else if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
            {
                RaycastHit hitUpper;
                if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
                {
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
                    Debug.Log("hitLower");
                }
            }
            else if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f,0,1), out hitLower45, 0.1f))
            {
                RaycastHit hitUpper45;
                if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f,0,1), out hitUpper45, 0.2f))
                {
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
                    Debug.Log("hitLower45");
                }
            }
            else if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f,0,1), out hitLowerMinus45, 0.1f))
            {
                RaycastHit hitUpperMinus45;
                if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f,0,1), out hitUpperMinus45, 0.2f))
                {
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
                    Debug.Log("hitLowerMinus45");
                }
            }
        }
    }
     /*
    OnGroundAngles
    */
    private bool OnGroundAngles()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanceAngle, hitMask))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            Debug.Log("Angulo " + angle);
            if (angle != 0 && onGround)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }
     /*
    OnGrounded
    */
    void OnGrounded()
    {
        //Limitador de distancia entre piso y personaje para minimizar caidas pequeñas
        if (distanceGround <= distanceGroundMAX)
       {
            onGround = true;
            anim.SetBool("OnFloor?",true);
            print("esta tocando piso");
       }
       else
       {
           onGround = false;
           anim.SetBool("OnFloor?",false);
           print("NO esta tocando piso");
       }
    }

    // ----------------------------------------------- Set inicio -----------------------------------------------------//
    /*
    setingData
    */
    void setingData(){
        Player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        anim.SetBool("jump?",true);
        cabeza.SetActive(false);
        rb = GetComponent<Rigidbody>();
        //distancia con el piso, se puede dejar como esta para que el programa lo calcule o cambiarlo por numeros fijos, 
        //se le agrega un * al lado de Y para aumentar el numero
        distanceGround = GetComponent<Collider>().bounds.extents.y + 1f;
        //hitMask es la capa predefinida para el inspector
        hitMask = LayerMask.GetMask("suelo");
    }
    /*
    getAnim
    */
    void getAnim(){
        onGround = anim.GetBool("OnFloor?");
        waitIdle = anim.GetBool("waitIdle?");
        agachado = anim.GetBool("bend?");
        jump = anim.GetBool("jump?");
        run = anim.GetBool("run?");
        isOnSlope = anim.GetBool("Slide?");
    }

    // ----------------------------------------------- Cosas a Mejorar -----------------------------------------------------//
    void betterment(){
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("roll?", true);
        }
        else
        {
            anim.SetBool("roll?", false);
        }
    }
 /*    
    void FixedUpdate(){
        Vector3 moveVect = new Vector3(GetAxisRaw("Vertical"), 0, -GetAxisRaw("Horizontal"));//sideways player controls
        moveVect = Vector3.Cross(upVect, moveVect);//set move direction
        GetComponent<Rigidbody>().velocity = moveVect * speed;//sample application of movement direction
    }
    void OnCollisionStay(Collision col){
        if(col.contacts[0].normal.y > 0.7f){//check if ground is walkable, in this case 45 degrees and lower
            upVect = col.contacts[0].normal;//set up direction
        }
    } */

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
