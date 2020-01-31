using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotActivator : Activatable
{
    protected override void OnActivationChange()
    {
        PropagateActivation(!isActive);
    }
}
