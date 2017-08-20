using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour {

    public GameObject end;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (end.activeSelf)
        {
            AkSoundEngine.PostEvent("mus_death", gameObject);
            gameObject.GetComponent<TheEnd>().enabled = false;
        }
	}
}
