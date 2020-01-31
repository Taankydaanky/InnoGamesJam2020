using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrActivator : ConditionalActivator
{
    protected override bool UpdateCondition()
    {
        bool shouldActivate = false;
        foreach(Activatable acc in inputs)
        {
            if(acc.isActive)
            {
                shouldActivate = true;
                break;
            }
        }

        return shouldActivate;
    }
}
