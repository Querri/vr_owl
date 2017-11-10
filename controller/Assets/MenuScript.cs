using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	public GameObject pauseMenu;
	private bool isPaused;
	public string buttPause;

	// Use this for initialization
	void Start () {
		buttPause = "PS4_Options";
		Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update () {

			// pause game
			if (Input.GetButtonDown(buttPause)) {
			   isPaused = !isPaused;
				 if (isPaused) {
					 Time.timeScale = 0;
				 } else {
					 Time.timeScale = 1;
				 }
			   pauseMenu.SetActive(isPaused);
			}
	}
}
