using UnityEngine;
using System.Collections;

public class capsuleController : MonoBehaviour {

  public float rotationspeedX = 0.01F;
  public float rotationspeedY = 40;
  public float rotationspeedZ = 0.005F;

  private Camera cam;

  //initialization
  void Start() {
    cam = Camera.main;
  }

  void FixedUpdate() {
    // rotate
    if (Input.GetAxis("Vertical") != 0) {
      Vector3 camForward = cam.transform.forward;
      Quaternion camRotation = Quaternion.LookRotation(camForward);
      transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, Time.deltaTime * 3F);
    }
  }
}
