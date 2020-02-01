using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableEvent : Event
{
    public Activatable completeCondition;

    public override bool HasEnded(float eventDuration)
    {
        return completeCondition == null || completeCondition.isActive;
    }
}
