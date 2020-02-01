using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedEvent : ScriptableObject
{
    private Event execEvent;
    private float time;

    public bool Activate(float time)
    {
        if(time >= this.time)
        {
            execEvent.Execute();
            return true;
        }
        return false;
    }

    public GeneratedEvent(Event execEvent, float time)
    {
        this.execEvent = execEvent;
        this.time = time;
    }
}
