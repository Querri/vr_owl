using UnityEngine;
using System.Collections;

public class capsuleController : MonoBehaviour {

  private playerController parent;
  public bool imTrue;


  //initialization
  void Start() {
    //parent = transform.parent.GetComponent<playerController>();
    parent = GetComponentInParent<playerController>();
    imTrue = true;
    //parent.isFlying;
  }


  void OnCollisionEnter(Collision col) {
    parent.isFlying = false;
  }


  void FixedUpdate() {
    if (parent.isFlying) {
      float yVel = parent.rb.velocity.y + Physics.gravity.y;
      parent.rb.AddForce(0, -yVel, 0, ForceMode.Acceleration + 1);

      // rotate in air
      Quaternion roll = Quaternion.Euler(0, parent.transform.localEulerAngles.y - parent.cam.transform.localEulerAngles.z, 0);
      parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, roll, Time.deltaTime * 3F);
    }

    else if (Input.GetButtonDown(parent.buttLaunch)) {
      parent.rb.AddForce(Vector3.up * 500, ForceMode.Acceleration);
      parent.transform.rotation = transform.rotation;
      parent.isFlying = true;
    }

    else {
      // rotate on ground
      Vector3 camForward = parent.cam.transform.forward;
      Quaternion camRotation = Quaternion.LookRotation(camForward);
      transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, Time.deltaTime * 3F);
    }
  }
}
