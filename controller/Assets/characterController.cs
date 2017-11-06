using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

  public float groundspeed = 500.0F;
  public float launchspeed = 50.0F;
  public float airspeed = 10.0F;
  public float rotationspeedX = 0.01F;
  public float rotationspeedY = 40;
  public float rotationspeedZ = 0.005F;
  public float gravity = 20.0F;
  public float drag = 10.0F;

  public string buttonFlap;
  public string buttonDive;
  public string buttonLaunch;

  //private CharacterController cc;
  private Rigidbody rb;

  private bool isFlying = false;


  //initialization
  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    //cc = GetComponent<CharacterController>();
    rb = GetComponent<Rigidbody>();

    if (Application.isEditor) {
      buttonFlap = "PS4_L2";
      buttonDive = "PS4_L1";
      buttonLaunch = "PS4_X";
    } else {
      buttonFlap = "PS4_L1";  // L2
      buttonDive = "PS4_Triangle";  // L1
      buttonLaunch = "PS4_X";
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


  void FixedUpdate() {

    if (isFlying) {
      // dive
      if (Input.GetButton(buttonDive)) {
        if (transform.localEulerAngles.x != 70) {
          Quaternion diveAngle = Quaternion.Euler(70, transform.localEulerAngles.y, 0);
          transform.rotation = Quaternion.Lerp(transform.rotation, diveAngle, Time.time * rotationspeedZ);
        }
      }
      else {
        // glide and flap wings
        float yVel = rb.velocity.y + Physics.gravity.y;
        rb.AddForce(0, -yVel, 0, ForceMode.Acceleration + 1);
        if (Application.isEditor) {
          rb.AddForce(Vector3.up * (Input.GetAxis(buttonFlap) + 1) * 10, ForceMode.Acceleration);
        } else {
          rb.AddForce(Vector3.up * -(Input.GetAxis(buttonFlap) - 1) * 10, ForceMode.Acceleration);
        }
      }

      // alter speed
      float translation = (Input.GetAxis("Vertical") + 1) * airspeed * Time.deltaTime;
      transform.Translate(0, 0, translation);

      // tilt
      float angle = Input.GetAxis("Horizontal") * -40;
      Quaternion tiltAngle = Quaternion.Euler(0, transform.localEulerAngles.y, angle);
      transform.rotation = Quaternion.Lerp(transform.rotation, tiltAngle, Time.time * rotationspeedX);

      // turn
      if (Input.GetAxis("Horizontal") != 0) {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * 45 * Time.deltaTime, Space.World);
      }
    }



    else {
      // launch to air
      if (Input.GetButtonDown(buttonLaunch)) {
        rb.AddForce(Vector3.up * 500, ForceMode.Acceleration);
        fly();
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
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * 45 * Time.deltaTime, Space.World);
      }
    }
  }
}
