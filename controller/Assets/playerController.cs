using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public float groundspeed = 5.0F;
	public float launchspeed = 50.0F;
	public float airspeed = 10.0F;
	public float gravity = 20.0F;
	public float drag = 10.0F;

	public string buttFlap;
	public string buttLook;
	public string buttDive;
	public string buttLaunch;

	public bool isFlying;

	public Rigidbody rb;
	public Camera cam;

	// initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
    //cc = GetComponent<CharacterController>();
    rb = GetComponent<Rigidbody>();
    cam = Camera.main;

    if (Application.isEditor) {
      buttFlap = "PS4_L2";
      buttLook = "PS4_L1";
      buttLaunch = "PS4_X";
    } else {
      buttFlap = "PS4_L1";  // L2
      buttLook = "PS4_Triangle";  // L1
      buttLaunch = "PS4_X";
    }
  }



  void fly() {
    isFlying = true;
  }

  void land() {
    isFlying = false;
  }

  void OnCollisionEnter(Collision col) {
    if (isFlying) {
      land();
    }
	}



	// Update is called once per frame
	void FixedUpdate () {
		if (isFlying) {
      /* not diving
      if (true) {
        float yVel = rb.velocity.y + Physics.gravity.y;
        rb.AddForce(0, -yVel, 0, ForceMode.Acceleration + 1);
      }

      //move
      float translation = (Input.GetAxis("Vertical") + 1.1F) * airspeed * Time.deltaTime;
      if (Input.GetButton(buttLook)) {
        transform.position = transform.position + transform.forward * translation;
      } else {
        transform.position = transform.position + Camera.main.transform.forward * translation;
      }

      /* alter speed
      float translation = (Input.GetAxis("Vertical") + 1) * airspeed * Time.deltaTime;
      transform.Translate(0, 0, translation);

      // tilt
      float angle = Input.GetAxis("Horizontal") * -40;
      Quaternion tiltAngle = Quaternion.Euler(0, transform.localEulerAngles.y, angle);
      transform.rotation = Quaternion.Lerp(transform.rotation, tiltAngle, Time.time * rotationspeedX);

      // turn
      if (Input.GetAxis("Horizontal") != 0) {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * 45 * Time.deltaTime, Space.World);
      }*/
    }



    else {
      // launch to air
      if (Input.GetButtonDown(buttLaunch)) {
        rb.AddForce(Vector3.up * 500, ForceMode.Acceleration);
        fly();
      }

      // move
      if (Input.GetAxis("Vertical") != 0) {
        float translation = (Input.GetAxis("Vertical") * groundspeed * Time.deltaTime);
        transform.position = transform.position + cam.transform.forward * translation;
      }
    }
	}
}
