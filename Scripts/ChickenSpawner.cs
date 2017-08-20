using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{

    // Number Maximum of chicken permited on the map
    [SerializeField]
    int nbChickenMax = 4;
    // map Size
    public float mapSize = 100;

    public float spawnTime = 5f;
    // Number of chicken present on the scene

    public int nbChicken = 0;

    [SerializeField]
    private Transform chicken;

    private Vector3 position;


    // This Function create a Spawn a chicken in a random position
    void Spawn()
    {
        if (nbChicken < nbChickenMax)
        {
            float checkRadius = 0.1f;
            Collider[] checkPosition;
            do
            {
                position = new Vector3(Random.Range(-mapSize, mapSize), 0.5f, Random.Range(-mapSize, mapSize)) + transform.position;
                checkPosition = Physics.OverlapSphere(position, checkRadius);
            } while (checkPosition.Length > 1);

            //Debug.Log(checkPosition.Length);

            nbChicken++;
            ChickenAI chickenAI =  Instantiate(chicken, position, Quaternion.identity).GetComponent<ChickenAI>();
            chickenAI.chickenSpawner = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", 1f, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
