using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEvent", menuName = "ScriptableObjects/DamageEvent", order = 1)]
public class DamageEvent : DurationEvent
{
    public int damage;
    public RocketSectionHealth healthSection;
    public Activatable damageCondition;
    public DamageGroup[] damageGroups;

    public override void End(float eventDuration)
    {
        DoDamage();
        base.End(eventDuration);
    }

    protected void DoDamage()
    {
        if(damageCondition == null || damageCondition.isActive)
        {
            healthSection.currentHealth -= damage;

            foreach(DamageGroup dg in damageGroups)
            {
                dg.DoDamage();
            }
        }
    }
}
