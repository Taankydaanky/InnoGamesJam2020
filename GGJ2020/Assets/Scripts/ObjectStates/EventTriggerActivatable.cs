using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerActivatable : Activatable
{
    public UnityEvent activationEvent;
    public UnityEvent deactivationEvent;

    protected override void OnActivationChange()
    {
        base.OnActivationChange();

        if(isActive)
        {
            if(activationEvent != null)
            {
                activationEvent.Invoke();
            }
        }

        else
        {
            if(deactivationEvent != null)
            {
                deactivationEvent.Invoke();
            }
        }
    }
}
