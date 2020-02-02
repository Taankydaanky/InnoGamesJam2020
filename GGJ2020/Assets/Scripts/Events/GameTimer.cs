using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    public int maxSeconds = 120;
    public UnityEvent timerEnded;
    public EventGenerator eventGenerator;
    public int currentSeconds { get; set; }
    private DateTime startTime;
    private double elapsedSeconds = 0;

    void Start()
    {
        currentSeconds = -1;
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
        bool ended = false;
        float exactTime = (float) (DateTime.UtcNow.Subtract(startTime).TotalSeconds + elapsedSeconds);
        int newSeconds = (int) exactTime;

        if(newSeconds != currentSeconds)
        {
            eventGenerator.UpdateRunningEvents(exactTime);
            if(HealthManager.healthManager != null && HealthManager.healthManager.AnySectionDestroyed())
            {
                ended = true;
            }
        }

        eventGenerator.UpdateEvents(exactTime);

        currentSeconds = newSeconds;

        if(currentSeconds >= maxSeconds)
        {
            ended = true;
        }

        if(ended)
        {
            if (timerEnded != null)
            {
                timerEnded.Invoke();
            }
            enabled = false;
        }
    }
}
