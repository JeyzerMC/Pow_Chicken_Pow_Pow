using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
public class EndOfRoundScreen : MonoBehaviour
{


    public Text t;
    public Image canvas;
    public float fadingspeed;

    // Use this for initialization
    void Start()
    {
        

        // Start Sound Event
        //AkSoundEngine.PostEvent("end_level", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = this.gameObject.GetComponent<CanvasGroup>().alpha;
        if (alpha < 1)
        {
            alpha += Time.unscaledDeltaTime / fadingspeed; ;

            // Debug.Log("alpha =  "+alpha);
            this.gameObject.GetComponent<CanvasGroup>().alpha = alpha;
        }

        GamePad.SetVibration(0, 0, 0);
        GamePad.SetVibration((PlayerIndex)1, 0, 0);

    }






    public void LaunchEndOfRound(String winnerName)
    {
        Debug.Log("LaunchEndOfRound");
        Debug.Log(winnerName + " \n  wins the round !");
        //t.text = winnerName + " \n  wins the round !";
        this.gameObject.SetActive(true);
    }
}
