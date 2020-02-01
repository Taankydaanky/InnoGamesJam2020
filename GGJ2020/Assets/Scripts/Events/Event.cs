using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event : ScriptableObject
{
    public float duration;
    public UnityEvent startEvent, endEvent;
    public virtual void Execute(float time)
    {
        RunStartEvent();
    }

    public virtual void Update(float eventDuration)
    {

    }

    public virtual void End(float eventDuration)
    {
        RunEndEvent();
    }

    public virtual bool HasEnded(float eventDuration)
    {
        return duration <= eventDuration;
    }
    
    protected void RunStartEvent()
    {
        if(startEvent != null)
        {
            startEvent.Invoke();
        }
    }

    protected void RunEndEvent()
    {
        if(endEvent != null)
        {
            endEvent.Invoke();
        }
    }

}
