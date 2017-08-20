using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDisable : MonoBehaviour
{

    public float seconds = 1.0f;
    // put time scale to 0
    public bool autoFreez = false;

    // equivalent in frames
    private long frames;
    // count of frames
    private long count;
    // Use this for initialization
    void Start()
    {
        count = 0;
        frames = (long)(seconds * 60);
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count >= frames)
        {
            count = 0;
            if (autoFreez)
                Time.timeScale = 0f;
            else
                gameObject.SetActive(false);
        }
    }
}
