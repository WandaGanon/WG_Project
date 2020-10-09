using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;
        PlayerControls inputActions;
        Vector2 movementInput;
        Vector2 camaraInput;


        public  void OnEnable() 
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movent.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camara.performed += i => camaraInput = i.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }

        private void OnDisable() {
            inputActions.Disable();
        }

        public void TickInput(float delta){
            MoveInput(delta);
        }

        private void MoveInput(float delta){
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = camaraInput.x;
            mouseY = camaraInput.y;
        }

    }
}