using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public GameObject pauseMenu;
    private bool isPaused;
    public string buttPause;


    // initialization
    void Start () {
	    buttPause = "PS4_Options";
	    pauseMenu.SetActive(false);
	    Pause();
    }

    void Pause() {
	    isPaused = !isPaused;
	    if (isPaused) {
		    Time.timeScale = 0;
	    } else {
		    Time.timeScale = 1;
	    }
	    pauseMenu.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetButtonDown(buttPause)) {
		    Pause();
	    }
    }
}
