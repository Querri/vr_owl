using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
	public GameObject player;

	void Start () {
  }

	void LateUpdate () {
		transform.position = player.transform.position;
	}
}
