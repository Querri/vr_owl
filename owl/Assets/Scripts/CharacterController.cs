using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float groundspeed;
    public float launchspeed;
    public float airspeed;
    public float rotationspeedX;
    public float rotationspeedYGround;
    public float rotationspeedYAir;
    public float rotationspeedZ;
    public float gravity;

	public string buttFlap;
	public string buttDive;
	public string buttLaunch;

	private Rigidbody rb;
	private bool isFlying = false;


	//initialization
	void Start() {
	    Cursor.lockState = CursorLockMode.Locked;
	    rb = GetComponent<Rigidbody>();

        // android mapping for PS4 controller is messed up
	    if (Application.isEditor) {
	        buttFlap = "PS4_X";
	        buttDive = "PS4_Circle";
	        buttLaunch = "PS4_X";
	    } else {
	        buttFlap = "PS4_X";
	        buttDive = "PS4_Square";
	        buttLaunch = "PS4_X";
	    }
    }



    void OnCollisionEnter(Collision col) {
	    if (isFlying) {
		    isFlying = false;
	    }
    }


	void FixedUpdate() {

        if (isFlying) {

            // not dive
            if (!Input.GetButton(buttDive)) {
	            float yVel = rb.velocity.y + Physics.gravity.y;
	            rb.AddForce(Vector3.up * -yVel, ForceMode.Acceleration + 1);
		    }

		    // gain altitude
		    if (Input.GetButton(buttFlap)) {
			    rb.AddForce(Vector3.up * 50, ForceMode.Acceleration + 1);
		    }

            // alter speed
            float translation = (Input.GetAxis("Vertical") + 1) * airspeed * Time.deltaTime;
            transform.Translate(0, 0, translation);

            // tilt
            float angle = Input.GetAxis("Horizontal") * -20;
            Quaternion tiltAngle = Quaternion.Euler(0, transform.localEulerAngles.y, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, tiltAngle, Time.time * rotationspeedX);

            // turn
            if (Input.GetAxis("Horizontal") != 0) {
                transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationspeedYAir * Time.deltaTime, Space.World);
            }
        }



        else {
            // launch to air
            if (Input.GetButtonDown(buttLaunch)) {
                rb.AddForce(Vector3.up * 500, ForceMode.Acceleration);
                isFlying = true;
            }

            // correct x and z rotation
            if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0) {
                Quaternion groundAngle = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, groundAngle, Time.time * rotationspeedZ);
            }

            // move
            if (Input.GetAxis("Vertical") != 0) {
                float translation = (Input.GetAxis("Vertical")) * groundspeed * Time.deltaTime;
                transform.Translate(0, 0, translation);
            }

            // turn
            if (Input.GetAxis("Horizontal") != 0) {
                transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationspeedYGround * Time.deltaTime, Space.World);
            }
        }
    }
}
