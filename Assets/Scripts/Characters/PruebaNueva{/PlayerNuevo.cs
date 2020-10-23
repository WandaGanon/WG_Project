using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNuevo : MonoBehaviour
{
// Informacion Obtenida de https://www.youtube.com/watch?v=FvvTDkJvBfA&list=PLOc9rHNEUHlyryuY0PvipHTXyL2mBij9-
//https://www.youtube.com/watch?v=Dp_MFvT_VIw&list=PLOc9rHNEUHlyryuY0PvipHTXyL2mBij9-&index=2&ab_channel=GamerGarage
    public float h;
    public float v;
    private Vector3 playerInput;
    private Vector3 moverPlayer;
    public CharacterController Player;
    public float Velocidad = 0.1f;
    public float gravedad = 9.8f;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    public float fallVelocity;
    public float jumpForce;


    void Start()
    {
        Player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical"); 

        playerInput = new Vector3(h,0, v);

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
        if (Player.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            fallVelocity = jumpForce;
            moverPlayer.y = fallVelocity;
        }

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
    }

    private void FixedUpdate() {
    }
}
