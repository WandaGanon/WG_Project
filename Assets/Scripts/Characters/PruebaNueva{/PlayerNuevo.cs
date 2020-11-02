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
    public bool isOnSlope = false;
    private Animator anim;
 
    void Start()
    {
        Player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetBool("jump?",true);

    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical"); 
        Idle();

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
        if (Player.isGrounded && Input.GetKey(KeyCode.LeftShift))
        { 
            anim.SetBool("run?", true);
            moverPlayer = moverPlayer * Velocidad * VelocidadCorrer; 
        }
        // Velocidad agachado
        else if(Player.isGrounded && Input.GetKey(KeyCode.LeftControl))
        { 
            anim.SetBool("bend?", true);
            moverPlayer = moverPlayer * Velocidad * VelocidadAgachado; 
            Player.height = 0.9f;
        }
        // Velocidad Para Caminar
        else{ 
            moverPlayer = moverPlayer * Velocidad; 
            anim.SetBool("run?", false);
            anim.SetBool("bend?", false);
        }

            //Cuando no este agachado
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
            Player.height = 1.73f;
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
        if (Player.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
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
}
