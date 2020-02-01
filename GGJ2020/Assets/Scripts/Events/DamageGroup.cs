using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageGroup", menuName = "ScriptableObjects/DamageGroup", order = 1)]
public class DamageGroup : ScriptableObject
{
    public BreakableObjectList breakableObjectList;
    public Activatable damageCondition;
    public int minObjectBreaks, maxObjectBreaks;

    public void DoDamage()
    {
        if(damageCondition == null || damageCondition.isActive)
        {
            breakableObjectList.BreakObjects(minObjectBreaks, minObjectBreaks);
        }
    }
}
