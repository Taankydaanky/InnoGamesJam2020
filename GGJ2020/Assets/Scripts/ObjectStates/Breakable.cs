using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private bool broken;

    public bool updateOnStart;
    public Enums.RepairKits repairKitNeeded;

    private Animator animator, highlightAnimator;

    public bool isBroken {
        get => broken;
        set
        {
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
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (updateOnStart)
        {
            OnBreakStateChange();
        }

        if (transform.childCount > 0 && GetComponent<Activatable>()==null)
        {
            highlightAnimator = transform.GetChild(0)?.GetComponent<Animator>();
        }
    }

    public void Highlight(bool highlight)
    {
        if (highlightAnimator != null)
        {
            highlightAnimator.SetBool("Highlighted", highlight);
        }
    }
}
