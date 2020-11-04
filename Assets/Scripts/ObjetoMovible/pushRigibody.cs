using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushRigibody : MonoBehaviour
{
    // Esta es la variable de fuerza del personaje
    public float pushPower = 2.0f;
    // esta es la variable de la masa del objeto
    private float targetMass;


    private void OnControllerColliderHit(ControllerColliderHit hit) {
    Rigidbody body = hit.collider.attachedRigidbody;
    // se revisa si el objeto tiene o no rigibody
    if (body == null || body.isKinematic)
    {
        return;
    }
    
    if (hit.moveDirection.y < -0.3)
    {
        return;
    }

    targetMass = body.mass;
    Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
    // se multiplica la direccion del player * la fuerza / la masa del objeto.
    body.velocity = pushDir * pushPower / targetMass;

    }


}
