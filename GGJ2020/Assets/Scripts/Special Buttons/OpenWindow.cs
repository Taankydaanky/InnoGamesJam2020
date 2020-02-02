using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : Activatable
{
    [SerializeField] private Animator[] windowAnimators;

    public override void Activate()
    {
        base.Activate();
        foreach(Animator animator in windowAnimators)
        {
            animator.SetBool("open", true);
        }
        //todo start music here
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (Animator animator in windowAnimators)
        {
            animator.SetBool("open", false);
        }
        //todo stop music here
    }
}
