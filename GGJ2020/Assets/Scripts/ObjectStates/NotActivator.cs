using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotActivator : Activatable
{
    public override bool isActive { get => !base.isActive; set => base.isActive = value; }
    protected override void OnActivationChange()
    {
        PropagateActivation(isActive);
    }
}
