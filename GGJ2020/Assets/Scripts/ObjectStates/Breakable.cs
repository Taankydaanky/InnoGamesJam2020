using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private bool broken;
    public bool updateOnStart;
    private Animator animator;
    public bool isBroken {
        get => broken;
        set
        {
            if(value != broken)
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
    }
}
