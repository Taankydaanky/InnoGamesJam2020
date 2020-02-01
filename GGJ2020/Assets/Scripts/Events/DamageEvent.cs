using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : DurationEvent
{
    public int damage;
    public HealthManager.Sections section;
    public Activatable damageCondition;
    public bool completeOnConditionInactive;
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
            HealthManager.healthManager.RemoveSectionHealth((int) section, damage);

            foreach(DamageGroup dg in damageGroups)
            {
                dg.DoDamage();
            }
        }
    }

    public override bool HasEnded(float eventDuration)
    {
        return base.HasEnded(eventDuration)
            || (completeOnConditionInactive && damageCondition != null && !damageCondition.isActive);
    }
}
