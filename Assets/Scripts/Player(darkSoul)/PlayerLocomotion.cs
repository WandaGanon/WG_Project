using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform camaraObject;
        InputHandler InputHandler;
        Vector3 moveDirection;
        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler AnimatorHandler;
        public new Rigidbody rigidbody;
        public GameObject normalCamara;
         
        [Header("Stasts ")]
        [SerializeField]
        float movementSpeed = 5f;
        [SerializeField]
        float rotationSpeed = 10f;

        // Start is called before the first frame update
        void Start()
        {   
            rigidbody = GetComponent<Rigidbody>();
            InputHandler =  GetComponent<InputHandler>();
            AnimatorHandler =  GetComponent<AnimatorHandler>();
            camaraObject = Camera.main.transform;
            myTransform = transform;
            AnimatorHandler.Initialize();
        }

        public void Update() {
            float delta = Time.deltaTime;

            InputHandler.TickInput(delta);
            moveDirection = camaraObject.forward * InputHandler.vertical;
            moveDirection += camaraObject.right * InputHandler.horizontal;

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            if (AnimatorHandler.canRotate)
            {
                Handlerotation(delta);
            }

        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void Handlerotation(float delta){
            Vector3 targetDir = Vector3.zero;
            float moveOverride = InputHandler.moveAmount;

            targetDir = camaraObject.forward * InputHandler.vertical;
            targetDir += camaraObject.right * InputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;
            if (targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward; 
            }

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }
        #endregion
    }
}
