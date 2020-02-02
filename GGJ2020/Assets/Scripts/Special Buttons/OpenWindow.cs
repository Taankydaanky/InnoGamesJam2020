using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class OpenWindow : Activatable
{
    [SerializeField] private Animator[] windowAnimators;
    [SerializeField] private Activatable[] inputs;

    public AudioMixerSnapshot windowOpen;
    public AudioMixerSnapshot windowClosed;

    public override void Activate()
    {
        foreach(Activatable input in inputs)
        {
            if (!input.isActive)
                return;
        }

        base.Activate();
        foreach(Animator animator in windowAnimators)
        {
            animator.SetBool("open", true);
        }
        if (windowOpen != null)
        {
            windowOpen.TransitionTo(transitionTime);
        }
    }

    public override void Deactivate()
    {
        foreach (Activatable input in inputs)
        {
            if (!input.isActive)
                return;
        }

        base.Deactivate();
        foreach (Animator animator in windowAnimators)
        {
            animator.SetBool("open", false);
        }
        if (windowClosed != null)
        {
            windowClosed.TransitionTo(transitionTime);
        }
    }
}
