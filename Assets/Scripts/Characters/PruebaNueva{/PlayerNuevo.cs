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
    public float gravedad = 9.8f;
    public float slopeForceDown = -10f;
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    public float fallVelocity;
    public float jumpForce;
    public float slideVelocity = 7f;

    [Header("Verificar estado del Player")]
    public bool suelo_real;
    public bool waitIdle;
    public bool isOnSlope = false;
    private Animator anim;
 
    void Start()
    {
        Player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
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

        moverPlayer = moverPlayer * Velocidad;

        Player.transform.LookAt(Player.transform.position + moverPlayer);

        SetGravity();

        PlayerJump();

        Player.Move( moverPlayer * Time.deltaTime);
        
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
            fallVelocity = jumpForce;
            moverPlayer.y = fallVelocity;
        }

        if (Player.isGrounded) { suelo_real = true; }
        else { suelo_real = false; }

    }

    void SetGravity(){
        if (Player.isGrounded)
        {
            fallVelocity = -gravedad * Time.deltaTime;
            moverPlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravedad * Time.deltaTime;
            moverPlayer.y = fallVelocity;
        }
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
    }

    public void  Idle(){
        if(h == 0  && v == 0){ anim.SetBool("waitIdle?", true);
           waitIdle = anim.GetBool("waitIdle?");
        }
        else{ anim.SetBool("waitIdle?", false);
           waitIdle = anim.GetBool("waitIdle?");
        }
    }   

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        hitNormal  = hit.normal;
    }

    public void InjectEjesPrincipales(float x, float y){
        anim.SetFloat("Velocidad_x", v);
        anim.SetFloat("Velocidad_y", y); 
   }
}
