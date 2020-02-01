using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadCollision : MonoBehaviour
{
    private Activatable currentActivatable;
    private Breakable currentBreakable;
    private ControlsMaster controlsMaster;
    private PlayerItems playerItems;
    private Animator animator;

    private void Start()
    {
        playerItems = GetComponentInParent<PlayerItems>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if(controlsMaster == null)
        {
            controlsMaster = new ControlsMaster();
            controlsMaster.Player.Activate.performed += Toggle;
        }

        controlsMaster.Player.Enable();
    }

    private void OnDisable()
    {
        controlsMaster.Disable();
    }

    private void Toggle(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("Click");
        currentActivatable?.Toggle();
        if (currentBreakable != null)
        {
            if (currentBreakable.repairKitNeeded == playerItems.currentRepairKit)
            {
                currentBreakable.isBroken = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentActivatable = collision.GetComponent<Activatable>();
        currentBreakable = collision.GetComponent<Breakable>();

        if (currentActivatable!=null)
        {
            currentActivatable.Highlight(true);
        }

        if(currentBreakable!=null)
        {
            currentBreakable.Highlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentActivatable = collision.GetComponent<Activatable>();
        currentBreakable = collision.GetComponent<Breakable>();

        if (currentActivatable != null)
        {
            currentActivatable.Highlight(false);
            currentActivatable = null;
        }

        if (currentBreakable != null)
        {
            currentBreakable.Highlight(false);
            currentBreakable = null;
        }
    }
}
