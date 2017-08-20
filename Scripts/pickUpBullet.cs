using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpBullet : MonoBehaviour
{

    void OnTriggerEnter( Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.nbBulletsLeft < 6)
            {
                playerController.pickUp();
                Destroy(gameObject);
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
