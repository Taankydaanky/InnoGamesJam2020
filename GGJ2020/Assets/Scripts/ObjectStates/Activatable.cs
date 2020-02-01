using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    [SerializeField] protected bool active;
    public virtual bool isActive { get => active; set => active = value; }
    public bool updateOnStart = true;
    public Activatable[] toActivate;
    private Animator animator, highlightAnimator;


    public virtual void Activate()
    {
        if(CanActivate())
        {
            if(!active)
            {
                isActive = true;
                OnActivationChange();

                if(animator != null)
                {
                    animator.SetBool("Active", true);
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

    public virtual void Deactivate()
    {
        if(active && CanDeactivate())
        {
            isActive = false;
            OnActivationChange();

            if(animator != null)
            {
                animator.SetBool("Active", false);
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
