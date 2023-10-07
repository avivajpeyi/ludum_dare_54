using System;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    private float currentTime = 0.0f;
    private bool isStopwatchRunning = false;

    public string time
    {
        get
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            return string.Format("[{0:00}:{1:00}]", minutes, seconds);
        }
    }


    void Update() 
    { if (isStopwatchRunning) {currentTime += Time.deltaTime;} }

    public void Resume() => isStopwatchRunning = true;
    public void Pause() => isStopwatchRunning = false;
}