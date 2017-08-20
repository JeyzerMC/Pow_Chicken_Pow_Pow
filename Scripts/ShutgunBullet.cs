using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutgunBullet : MonoBehaviour {

    // Number of bullets the shutgun bullet partition itself into
    public int nbBulletsMax = 3;
    // Number of bullets in use
    public int nbBullets = 0;
    // Speed of bullets
    public int speedBullets = 20;
    // Rebounds
    public float elasticityFactor = 0.75f;
    // Air loss
    public float hairLoss = 0.02f;
    // angle of shooting
    public float angle = 20f;

    // The bullet that will be copied for the shutgun
    public GameObject bulletRef;

	void Start () {
        GameObject left = Instantiate(bulletRef, transform.position, transform.rotation);
        left.transform.Rotate(new Vector3(0, angle, 0));
        GameObject center = Instantiate(bulletRef, transform.position, transform.rotation);
        GameObject right = Instantiate(bulletRef, transform.position, transform.rotation);
        right.transform.Rotate(new Vector3(0, -angle, 0));

        left.GetComponent<bulletEffect>().speed_ = speedBullets;
        left.GetComponent<bulletEffect>().elasticityFactor = elasticityFactor;
        left.GetComponent<bulletEffect>().airSpeedLoss = hairLoss;

        right.GetComponent<bulletEffect>().speed_ = speedBullets;
        right.GetComponent<bulletEffect>().elasticityFactor = elasticityFactor;
        right.GetComponent<bulletEffect>().airSpeedLoss = hairLoss;

        center.GetComponent<bulletEffect>().speed_ = speedBullets;
        center.GetComponent<bulletEffect>().elasticityFactor = elasticityFactor;
        center.GetComponent<bulletEffect>().airSpeedLoss = hairLoss;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
