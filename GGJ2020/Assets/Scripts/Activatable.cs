﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    [SerializeField] protected bool active;
    public virtual bool isActive { get => active; set => active = value; }
    public bool updateOnStart = true;
    public Activatable[] toActivate;
    public string activateAnimation;
    public string deactivateAnimation;

    public void Activate()
    {
        if(CanActivate())
        {
            if(!active)
            {
                isActive = true;
                OnActivationChange();

                if(activateAnimation != null && !activateAnimation.Equals(""))
                {
                    Animator ani = gameObject.GetComponent<Animator>();
                    if(ani != null)
                    {
                        ani.Play(activateAnimation);
                    }
                }
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
        if(active && CanDeactivate())
        {
            isActive = false;
            OnActivationChange();
            if (deactivateAnimation != null && !deactivateAnimation.Equals(""))
            {
                Animator ani = gameObject.GetComponent<Animator>();
                if (ani != null)
                {
                    ani.Play(deactivateAnimation);
                }
            }
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
        PropagateActivation(active);
    }

    private void Start()
    {
        if(updateOnStart)
        {
            OnActivationChange();
        }
    }
}
