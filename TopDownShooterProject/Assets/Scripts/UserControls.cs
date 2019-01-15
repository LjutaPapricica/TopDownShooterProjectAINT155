using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserControls : MonoBehaviour {

    bool isPaused;
    public Image instructions;

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
                instructions.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                AudioListener.pause = false;
                isPaused = false;
                instructions.gameObject.SetActive(false);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //camera size between 3 - 8;

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel"), 3f, 8f);
        }
            

        
	}
}
