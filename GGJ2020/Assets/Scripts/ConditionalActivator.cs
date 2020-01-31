using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalActivator : Activatable
{
    public Activatable[] inputs;

    public override bool CanActivate()
    {
        return UpdateCondition();
    }

    public override bool CanDeactivate()
    {
        return !UpdateCondition();
    }

    protected abstract bool UpdateCondition();
}
