using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

    private float currentX, currentY, currentZ;

    public Vector3 idealPosition = new Vector3(8f, 52.2f, -25f);
    public int offSet = 30;
    //private float shake = 0.2f;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //thx Mehdi
            IEnumerator cor = shakshakshakira(0.2f);
            StartCoroutine(cor);
        }
	}


    public IEnumerator shakshakshakira(float shake)
    {
        transform.position = new Vector3(idealPosition.x - shake, idealPosition.y - shake, idealPosition.z - shake);
        yield return new WaitForSeconds(0.03f);
        transform.position = new Vector3(idealPosition.x + shake, idealPosition.y + shake, idealPosition.z + shake);
        yield return new WaitForSeconds(0.03f);

        transform.position = new Vector3(idealPosition.x, idealPosition.y, idealPosition.z);
    }
}
