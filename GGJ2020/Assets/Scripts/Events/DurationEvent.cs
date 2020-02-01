using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DurationEvent", menuName = "ScriptableObjects/DurationEvent", order = 1)]
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
