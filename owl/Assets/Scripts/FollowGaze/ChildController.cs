using UnityEngine;
using System.Collections;

public class ChildController : MonoBehaviour {

    public float rotationspeedX = 0.01F;
    public float rotationspeedY = 40;
    public float rotationspeedZ = 0.005F;

    private ParentController parent;


    //initialization
    void Start() {
        parent = transform.parent.GetComponent<ParentController>();
    }


    void OnCollisionEnter(Collision col) {
        parent.isFlying = false;
    }


    void FixedUpdate() {
        if (parent.isFlying) {
            float yVel = parent.rb.velocity.y + Physics.gravity.y;
            parent.rb.AddForce(0, -yVel, 0, ForceMode.Acceleration + 1);
        }
        else if (Input.GetButtonDown(parent.buttLaunch)) {
            parent.rb.AddForce(Vector3.up * 500, ForceMode.Acceleration);
            parent.isFlying = true;
        }

        // rotate
        if (Input.GetAxis("Vertical") != 0) {
            Vector3 camForward = parent.cam.transform.forward;
            Quaternion camRotation = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, Time.deltaTime * 3F);
        }
    }
}
