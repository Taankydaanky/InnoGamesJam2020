using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamageEvent : DamageEvent
{
    public float damageInterval;
    public bool doDamageOnStart = true;
    private float lastDamageTime;

    public override void Execute(float time)
    {
        base.Execute(time);
        if(doDamageOnStart)
        {
            lastDamageTime = - damageInterval - 1;
        }
        else
        {
            lastDamageTime = 0;
        }
    }

    public override void UpdateEvent(float eventDuration)
    {
        if(lastDamageTime + damageInterval <= eventDuration)
        {
            lastDamageTime = eventDuration;
            DoDamage();
        }
    }

    public override void End(float eventDuration)
    {
        RunEndEvent();
    }
}
