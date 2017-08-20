using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour {

    //rotation speed per sec in seconds
    public float rotationSpeed = 90f;
    public float floatingSpeed = 12f;
    public float floatingAmplitude = 100f;

	// Use this for initialization
	void Start () {
        //this.gameObject.transform.Rotate(new Vector3(45,0,45));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Transform t = this.gameObject.transform;
        t.Rotate(new Vector3(0f, rotationSpeed*Time.fixedDeltaTime, 0f));
        float y =   floatingAmplitude* Mathf.Cos(Time.time*floatingSpeed)/360f;
        t.position = t.position + new Vector3(0f, y, 0f);
	}



}
