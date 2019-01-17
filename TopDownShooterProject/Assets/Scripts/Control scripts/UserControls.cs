using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserControls : MonoBehaviour {

    bool isPaused;
    public Image instructions;
    float minCamSize = 3f, maxCamSize = 8f;
	
	// Update is called once per frame
	void Update () {

        //if the esacpe button is pressed and the game is not currently paused
        //set timescale to 0 which pauses all movement and audiolistners are set to pause playing sounds
        //instructions shown to player if they need to see controls 
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                AudioListener.pause = true;
                isPaused = true;
                instructions.gameObject.SetActive(true);
            }
            //esc key pressed again to resume game session
            //instructions hidden
            else
            {
                Time.timeScale = 1f;
                AudioListener.pause = false;
                isPaused = false;
                instructions.gameObject.SetActive(false);
            }
        }

        //if player moves the scroll wheel a float magnitude between -1 and 1 is returned
        //the size of the camera is altered depending on the value returned from the scroll wheel
        //camera zoom fied between set values to prevent zooming too close or too far
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel"), minCamSize, maxCamSize);
        }
            

        
	}
}
