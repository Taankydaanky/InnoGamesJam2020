﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    

    public bool isActive;
    public bool updateOnStart = true;
    public Activatable[] toActivate;
    private Animator animator, highlightAnimator;


    public void Activate()
    {
        if(CanActivate())
        {
            if(!isActive)
            {
                isActive = true;
                OnActivationChange();

                //if(activateAnimation != null && !activateAnimation.Equals(""))
                //{
                //    Animator ani = gameObject.GetComponent<Animator>();
                //    if(ani != null)
                //    {
                //        ani.Play(activateAnimation);
                //    }
                //}
                animator.SetBool("Active", true);
                Debug.Log("activatable");
            }
        }
    }

    public virtual bool CanActivate()
    {
        Breakable breakable = gameObject.GetComponent("Breakable") as Breakable;
        if (breakable != null)
        {
            return !breakable.isBroken;
        }
        return true;
    }

    public void Deactivate()
    {
        if(isActive && CanDeactivate())
        {
            isActive = false;
            OnActivationChange();
            //if (deactivateAnimation != null && !deactivateAnimation.Equals(""))
            //{
            //    Animator ani = gameObject.GetComponent<Animator>();
            //    if (ani != null)
            //    {
            //        ani.Play(deactivateAnimation);
            //    }

            //}
            animator.SetBool("Active", false);
            Debug.Log("un-activatable");
        }
    }

    public virtual bool CanDeactivate()
    {
        return true;
    }

    protected void PropagateActivation(bool activate)
    {
        foreach (Activatable acc in toActivate)
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

    protected virtual void OnActivationChange()
    {
        PropagateActivation(isActive);
    }

    private void Start()
    {
        if(updateOnStart)
        {
            OnActivationChange();
        }
        animator = GetComponent<Animator>();
        highlightAnimator = transform.GetChild(0)?.GetComponent<Animator>();
    }

    public void Highlight(bool highlight)
    {
        highlightAnimator?.SetBool("Highlighted", highlight);
    }
}
