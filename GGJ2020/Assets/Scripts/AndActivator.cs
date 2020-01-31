using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndActivator : ConditionalActivator
{
    protected override bool UpdateCondition()
    {
        bool shouldActivate = true;
        foreach (Activatable acc in inputs)
        {
            if (!acc.isActive)
            {
                shouldActivate = false;
                break;
            }
        }

        return shouldActivate;
    }
}
