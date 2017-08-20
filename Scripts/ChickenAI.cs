using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAI : MonoBehaviour
{
    // The chicken spawner from witch it was spawned
    public ChickenSpawner chickenSpawner;


    private float speed = 0.05f;

    //private Vector3 direction;

    // Angle the chicken is going foward
    private float angle;


    //money prefab
    public GameObject money;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("ChangeDirection", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //Debug.Log(direction.normalized);
        transform.Translate(new Vector3(-speed, 0f,0f));
    }

    void ChangeDirection()
    {
        //direction = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        angle = Random.Range(-180.0f, 180.0f);
        transform.Rotate(new Vector3(0, angle, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("le poulet rentre ds qqchose");
        if (collision.gameObject.tag == "Walls" )
        {
            Debug.Log("Wall collided!");

            transform.Rotate(new Vector3(0, 90, 0));


            Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);

            //direction = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            //Debug.Log(direction.normalized);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("La balle tue le poulet. Le poulet est mooooooooooort");
            CreateMoney();
            chickenSpawner.nbChicken--;
            Destroy(this.gameObject);
        }

    }

    private void CreateMoney()
    {
        GameObject m = Instantiate(money, transform.position, Quaternion.identity);
        m.SetActive(true);
        
        
    }

}
