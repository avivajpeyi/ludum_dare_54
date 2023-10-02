using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSComputer : Singleton<FPSComputer>
{
 
    public int fps;
    
    
    public float updateInterval = 0.5f; // Update the FPS every 0.5 seconds
    private float accum = 0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    
    
    private void Start()
    {
        timeleft = updateInterval;
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Interval ended, so update FPS and reset variables
        if (timeleft <= 0.0)
        {
            fps = Mathf.RoundToInt(accum / frames);
            // Reset variables
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

}
