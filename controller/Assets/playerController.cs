using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public float rotationspeedX = 0.01F;
	public float rotationspeedY = 40;
	public float rotationspeedZ = 0.005F;
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
	private capsuleController child;


	// initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
    cam = Camera.main;
    rb = GetComponent<Rigidbody>();
		child = GetComponentInChildren<capsuleController>();

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


	// Update is called once per frame
	void FixedUpdate () {
		if (isFlying) {

      // alter speed
      float fly = (Input.GetAxis("Vertical") + 1) * airspeed * Time.deltaTime;
	    transform.Translate(child.transform.forward * fly);


      // tilt
			/*if (Quaternion.Euler(cam.transform.forward) != Quaternion.Euler(transform.forward) {
	      float angle = 20;
	      Quaternion tiltAngle = Quaternion.Euler(0, transform.localEulerAngles.y, angle);
	      transform.rotation = Quaternion.Lerp(transform.rotation, tiltAngle, Time.time * rotationspeedX);
			}*/
		}

    else {
			// rotate on ground
			Quaternion groundRot = Quaternion.Euler(0, transform.localEulerAngles.y + cam.transform.localEulerAngles.y, 0);
      transform.rotation = Quaternion.Slerp(transform.rotation, groundRot, Time.deltaTime);

      // move
      if (Input.GetAxis("Vertical") != 0) {
        float walk = (Input.GetAxis("Vertical") * groundspeed * Time.deltaTime);
				transform.Translate(cam.transform.forward * walk);
      }
    }
	}
}
