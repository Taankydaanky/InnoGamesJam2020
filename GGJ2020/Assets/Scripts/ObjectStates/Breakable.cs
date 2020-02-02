using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private bool broken;
    [SerializeField] private HealthManager.Sections improveHealth;
    [SerializeField] private int improveHealthAmount;
    
    public bool updateOnStart;
    public Enums.RepairKits repairKitNeeded;

    private Animator animator, highlightAnimator;

    public bool isBroken {
        get => broken;
        set
        {
            broken = value;
            OnBreakStateChange();
            if (value != broken)
            {
                broken = value;
                OnBreakStateChange();
            }
        }    
    }
 

    protected virtual void OnBreakStateChange()
    {
        if(animator != null)
        {
            animator.SetBool("broken", isBroken);
        }

        if(!isBroken && improveHealth != null)
        {
            HealthManager.healthManager.RemoveSectionHealth((int)improveHealth, improveHealthAmount*-1);
        }
    }

    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (updateOnStart)
        {
            OnBreakStateChange();
        }

        if (transform.childCount > 0 && GetComponentInParent<Activatable>()==null)
        {
            highlightAnimator = transform.GetChild(0)?.GetComponent<Animator>();
        }
    }

    public virtual void Highlight(bool highlight)
    {
        if (highlightAnimator != null)
        {
            highlightAnimator.SetBool("Highlighted", highlight);
        }
    }
}
