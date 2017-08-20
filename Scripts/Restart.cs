using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    // Use this for initialization


    public bool p1pressedStart = false;
    public bool p2pressedStart = false;
    public bool p1cancelled = false;
    public bool p2cancelled = false;


    public GameObject confirmedp1;
    public GameObject confirmedp2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Si ça rentre qu'on me coupe la tête");

        //Debug.Log(Time.timeScale);
        if (Input.GetKeyDown(KeyCode.R) || Input.GetAxis("Restart") == 1)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        //Debug.Log(Time.timeScale == 0f);
        if (Time.timeScale == 0f)
        {
            p2cancelled = Input.GetButtonDown("CancelRestart2");
            p1cancelled = Input.GetButtonDown("CancelRestart1");
            p2pressedStart = (p2pressedStart || Input.GetButtonDown("Restart2")) && !p2cancelled;
            p1pressedStart = (p1pressedStart || Input.GetButtonDown("Restart1")) && !p1cancelled;
            // Debug.Log("Si ça sort qu'on me coupe la tête");

        }
        if (p2pressedStart)
        {
            Debug.Log("p2");
            confirmedp2.SetActive(true);
        }
        else
        {

            confirmedp2.SetActive(false);
        }

        if (p1pressedStart)
        {
            Debug.Log("p1");
            confirmedp1.SetActive(true);
        }
        else
        {

            confirmedp1.SetActive(false);
        }


        //TODO add p2pressedstart
        if (Time.timeScale == 0f && p1pressedStart && p2pressedStart)
        {
            Time.timeScale = 1f;
            Application.LoadLevel(Application.loadedLevel);
        }


    }
}
