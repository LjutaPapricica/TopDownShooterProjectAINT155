using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControls : MonoBehaviour {

    bool isPaused;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                AudioListener.pause = true;
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                AudioListener.pause = false;
                isPaused = false;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //camera size between 3 - 8;

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel"), 3f, 8f);
        }
            

        
	}
}
