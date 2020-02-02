using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class OpenWindow : AndActivator
{
    [SerializeField] private Animator[] windowAnimators;

    public AudioMixerSnapshot windowOpen;
    public AudioMixerSnapshot windowClosed;

    public override void Activate()
    {

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
