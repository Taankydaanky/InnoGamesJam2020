using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO Stop on pause
public class DelayActivator : Activatable
{
    public float seconds;
    private DateTime startTime;

    void Start()
    {
        startTime = DateTime.UtcNow;
    }

    void Update()
    {
        double currentSeconds = DateTime.UtcNow.Subtract(startTime).TotalSeconds;
        
        if(currentSeconds >= seconds)
        {
            isActive = true;
            enabled = false;
        }
    }
}
