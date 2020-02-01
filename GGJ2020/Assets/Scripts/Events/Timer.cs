using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int maxSeconds = 120;
    public UnityEvent timerEnded;
    public EventGenerator eventGenerator;
    public int currentSeconds { get; set; }
    private DateTime startTime;
    private double elapsedSeconds = 0;

    void Start()
    {
        currentSeconds = 0;
        startTime = DateTime.UtcNow;
    }

    void Pause()
    {
        enabled = false;
        elapsedSeconds = DateTime.UtcNow.Subtract(startTime).TotalSeconds + elapsedSeconds;
    }

    void Resume()
    {
        enabled = true;
        startTime = DateTime.UtcNow;
    }
    
    void Update()
    {
        float exactTime = (float) (DateTime.UtcNow.Subtract(startTime).TotalSeconds + elapsedSeconds);
        int newSeconds = (int) exactTime;

        if(newSeconds != currentSeconds)
        {
            eventGenerator.UpdateRunningEvents(exactTime);
        }

        eventGenerator.Update(exactTime);

        currentSeconds = newSeconds;

        if(currentSeconds >= maxSeconds)
        {
            if(timerEnded != null)
            {
                timerEnded.Invoke();
            }
            enabled = false;
        }
    }
}
