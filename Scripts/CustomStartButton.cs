using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomStartButton : MonoBehaviour {

    StartOptions starter = new StartOptions();
	// Use this for initialization
	void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("StartBtn"))
        {
            starter.StartButtonClicked();
        }
	}
}
