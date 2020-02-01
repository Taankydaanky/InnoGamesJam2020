using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSequence : Activatable
{
    public ActivatableEvent[] sequence;
    public UnityEvent endedEvent;
    public int currentIndex = -1;
    
    void Start()
    {
        ProgressEvents();
    }

    public override void Activate()
    {
        if(enabled)
        {
            ProgressEvents();
        }
    }

    void ProgressEvents()
    {
        while(currentIndex < sequence.Length && (currentIndex < 0 || sequence[currentIndex].HasEnded(0)))
        {
            if(currentIndex >= 0)
            {
                sequence[currentIndex].End(0);
            }

            currentIndex++;

            if(currentIndex < sequence.Length)
            {
                Debug.Log(currentIndex);
                sequence[currentIndex].Execute(0);
            }

            Debug.Log(sequence[currentIndex].HasEnded(0));
        }

        if(currentIndex >= sequence.Length)
        {
            enabled = false;
            if(endedEvent != null)
            {
                endedEvent.Invoke();
            }
        } 
    }
}
