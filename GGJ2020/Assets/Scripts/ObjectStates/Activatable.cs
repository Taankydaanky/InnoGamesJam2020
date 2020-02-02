using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Activatable : MonoBehaviour
{
    public AudioClip buttonSound1;
    public AudioClip buttonSound2;
    public AudioMixerSnapshot buttonSongStart;
    public AudioMixerSnapshot buttonSongStop;
    public AudioMixerSnapshot buttonSongStop1;
    public AudioMixerSnapshot buttonSongStop2;
    public AudioMixerSnapshot buttonSongStop3;
    public AudioMixerSnapshot buttonSongStop4;
    public AudioMixerSnapshot buttonSongStop5;
    public AudioMixerSnapshot buttonSongStop6;
    public AudioMixerSnapshot buttonSongStop7;
    public AudioMixerSnapshot buttonSongStop8;
    public AudioMixerSnapshot buttonSongStop9;
    public AudioMixerSnapshot buttonSongStop10;
    public AudioMixerSnapshot buttonSongStop11;
    public AudioMixerSnapshot buttonSongStop12;
    public float transitionTime = .8f;



    [SerializeField] protected bool active;
    public virtual bool isActive { get => active; set => active = value; }
    public bool updateOnStart = true;
    public Activatable[] toActivate;
    private Animator animator, highlightAnimator;


    public virtual void Activate()
    {
        if (CanActivate())
        {
            if (!active)
            {
                isActive = true;
                OnActivationChange();

                if (animator != null)
                {
                    animator.SetBool("Active", true);



                }

                if (buttonSound1 != null && buttonSound2 != null)
                {
                    SoundManager.instance.RandomizeSfx(buttonSound2);
                }

                if (buttonSongStart != null)
                {
                    buttonSongStart.TransitionTo(transitionTime);
                }

                if (buttonSongStop1 != null)
                {
                    buttonSongStop1.TransitionTo(transitionTime);
                }
                if (buttonSongStop2 != null)
                {
                    buttonSongStop2.TransitionTo(transitionTime);
                }
                if (buttonSongStop3 != null)
                {
                    buttonSongStop3.TransitionTo(transitionTime);
                }
                if (buttonSongStop4 != null)
                {
                    buttonSongStop4.TransitionTo(transitionTime);
                }
                if (buttonSongStop5 != null)
                {
                    buttonSongStop5.TransitionTo(transitionTime);
                }
                if (buttonSongStop6 != null)
                {
                    buttonSongStop6.TransitionTo(transitionTime);
                }
                if (buttonSongStop7 != null)
                {
                    buttonSongStop7.TransitionTo(transitionTime);
                }
                if (buttonSongStop8 != null)
                {
                    buttonSongStop8.TransitionTo(transitionTime);
                }
                if (buttonSongStop9 != null)
                {
                    buttonSongStop9.TransitionTo(transitionTime);
                }
                if (buttonSongStop10 != null)
                {
                    buttonSongStop10.TransitionTo(transitionTime);
                }
                if (buttonSongStop11 != null)
                {
                    buttonSongStop11.TransitionTo(transitionTime);
                }
                if (buttonSongStop12 != null)
                {
                    buttonSongStop12.TransitionTo(transitionTime);
                }
            }
        }
    }


    public virtual bool CanActivate()
    {
        Breakable breakable = gameObject.GetComponentInChildren<Breakable>() as Breakable;
        if (breakable != null)
        {
            return !breakable.isBroken;
        }
        return true;
    }

    public virtual void Deactivate()
    {
        if(active && CanDeactivate())
        {
            isActive = false;
            OnActivationChange();

            if(animator != null)
            {
                animator.SetBool("Active", false);

                if (buttonSound1 != null && buttonSound2 != null)
                {
                    SoundManager.instance.RandomizeSfx(buttonSound1);
                    if (buttonSongStop != null)
                    {
                        buttonSongStop.TransitionTo(transitionTime);
                    }
                }
            }
        }
    }

    public virtual bool CanDeactivate()
    {
        return true;
    }

    public void Toggle()
    {
        if (!active)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    protected void PropagateActivation(bool activate)
    {
        foreach (Activatable acc in toActivate)
        {
            if(acc != null)
            {
                if (activate)
                {
                    acc.Activate();
                }
                else
                {
                    acc.Deactivate();
                }
            }
            
        }
    }

    protected virtual void OnActivationChange()
    {
        PropagateActivation(active);
    }

    protected virtual void Start()
    {
        if(updateOnStart)
        {
            OnActivationChange();
        }
        animator = GetComponent<Animator>();
        if(transform.childCount > 0)
        {
            highlightAnimator = transform.GetChild(0)?.GetComponent<Animator>();
        }
    }

    public void Highlight(bool highlight)
    {
        if(highlightAnimator != null)
        {
            highlightAnimator.SetBool("Highlighted", highlight);
        }
    }
}
