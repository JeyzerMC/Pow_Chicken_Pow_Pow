using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class explosiveBullet : MonoBehaviour
{

    // Speed loss due to air
    public float airSpeedLoss = 0.05f;

    // Minimum Speed
    public float minimumSpeed = 2f;

    // Speed
    public float speed_ = 20f;

    // Elasticity Factor of the bullet after the collision
    public float elasticityFactor = 0.5f;


    // Number of ricochet the bullet did
    //byte nbRico = 0;

    // Pickable Bullet
    public GameObject pickableBullet;


    public string shootButtonName;
    public float timeStamp = -1;
    public float cooldDown = 0.2f;
    public float explosionRadius;
    public float rotationSpeed = 1f;


    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {
        speed_ -= airSpeedLoss;

        if (speed_ <= minimumSpeed)
            Die();

        // This Code doesn't make sense... come see Adem
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed_);

    
    }

    IEnumerator myVibration()
    {
        //GamePad.SetVibration(0, 1, 1);
        yield return new WaitForSeconds(.2f);
        //GamePad.SetVibration(0, 0, 0);
    }

    // On Collision Function
    void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Walls")
        {
            Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            speed_ *= elasticityFactor;
        }
        else if (collision.gameObject.tag == "Player")
        {
            print("entered collsion with a player");
            PlayerController script = collision.gameObject.GetComponent<PlayerController>();
            script.OnTouchedByBullet();
            this.gameObject.SetActive(false);
            //GamePad.SetVibration(0, 0, 0);
        }
        else if (collision.gameObject.tag == "Chicken")
        {
            Die();
        }

    }
    // This function is called when the bullet dies
    void Die()
    {
        if (pickableBullet != null)
        {
            // Instantiate the pickable and setting it active
            GameObject pickable = Instantiate(pickableBullet, transform.position, transform.rotation);
            pickable.SetActive(true);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }

    
}

