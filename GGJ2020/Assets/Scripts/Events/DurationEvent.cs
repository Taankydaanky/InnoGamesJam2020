using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationEvent : Event
{
    public float duration;

    public override void UpdateEvent(float eventDuration)
    {

    }

    public override bool HasEnded(float eventDuration)
    {
        return duration <= eventDuration;
    }
}
