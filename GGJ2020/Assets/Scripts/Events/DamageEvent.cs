using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : DurationEvent
{
    public int damage;
    public HealthManager.Sections section;
    public Activatable damageCondition;
    public DamageGroup[] damageGroups;
    private HealthManager healthManager = HealthManager.healthManager;

    public override void End(float eventDuration)
    {
        DoDamage();
        base.End(eventDuration);
    }

    protected void DoDamage()
    {
        if(damageCondition == null || damageCondition.isActive)
        {
            healthManager.RemoveSectionHealth((int) section, damage);

            foreach(DamageGroup dg in damageGroups)
            {
                dg.DoDamage();
            }
        }
    }
}
