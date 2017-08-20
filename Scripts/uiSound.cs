using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Sound ()
    {
        AkSoundEngine.PostEvent("ui", gameObject);
    }
}
