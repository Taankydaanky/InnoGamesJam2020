using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableActivator : Breakable
{
    public Activatable[] toActivate;

    protected override void OnBreakStateChange()
    {
        base.OnBreakStateChange();

        
        foreach(Activatable acc in toActivate)
        {
            if (isBroken)
            {
                acc.Deactivate();
            }
            else
            {
                acc.Activate();
            }
        }
    }
}
