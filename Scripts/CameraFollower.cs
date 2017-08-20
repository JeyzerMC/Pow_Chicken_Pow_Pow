using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    // Use this for initialization

    public Vector3 idealPosition = new Vector3(8,35,-25);
    public Vector3 idealRotation = new Vector3(60, 0, 0);
    public int offSet = 30;
	void Start () {
        transform.position = new Vector3(idealPosition.x, idealPosition.y + offSet, idealPosition.z + offSet);
        transform.eulerAngles = new Vector3(idealRotation.x + offSet, idealRotation.y, idealRotation.z);
        StartCoroutine(initialZoomIn());
    }
	
	// Update is called once per frame
	void Update () {
	}


    IEnumerator showArround()
    {
        for(int i= 0; i <100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 0.1f, transform.eulerAngles.z);
        }
    }


    IEnumerator initialZoomIn()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < offSet; i++)
        {
            yield return new WaitForSeconds(0.015f);
            transform.position = new Vector3(idealPosition.x, idealPosition.y + (offSet - i), idealPosition.z + (offSet - i));
            transform.eulerAngles = new Vector3(idealRotation.x + (offSet - i), idealRotation.y, idealRotation.z);
        }
    }
}
