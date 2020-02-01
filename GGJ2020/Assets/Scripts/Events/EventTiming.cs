using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventTiming", menuName = "ScriptableObjects/EventTiming", order = 1)]
public class EventTiming : ScriptableObject
{
    public float triggerTimeInSeconds;
    public float positiveVariability, negativeVariability;

}
