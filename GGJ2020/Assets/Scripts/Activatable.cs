using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    public bool isActive;
    public Activatable[] toActivate;
    public string activateAnimation;
    public string deactivateAnimation;

    public void Activate()
    {
        if(CanActivate())
        {
            if(!isActive)
            {
                isActive = true;
                OnActivationChange();

                if(activateAnimation != null && !activateAnimation.Equals(""))
                {
                    Animator ani = gameObject.GetComponent<Animator>();
                    if(ani != null)
                    {
                        ani.Play(activateAnimation);
                        print("Tset");
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
        if(isActive && CanDeactivate())
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
        PropagateActivation(isActive);
    }

    private void Start()
    {
        OnActivationChange();
    }
}
