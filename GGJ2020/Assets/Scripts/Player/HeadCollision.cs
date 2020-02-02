using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HeadCollision : MonoBehaviour
{
    [SerializeField] private Text messageTextField;

    private Activatable currentActivatable;
    private Breakable currentBreakable;
    private ControlsMaster controlsMaster;
    private PlayerItems playerItems;
    private Animator animator;

    private bool isTutorial = true;


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
                isTutorial = false;
            }
            else if(isTutorial)
            {
                messageTextField.text += "\n Find a new button to fix it!!";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Activatable tempActivatable = collision.GetComponent<Activatable>();
        Breakable tempBreakable = collision.GetComponentInChildren<Breakable>();

        if (tempActivatable!=null)
        {
            if(currentActivatable != null)
            {
                currentActivatable.Highlight(false);
            }
            currentActivatable = tempActivatable;
            currentActivatable.Highlight(true);
        }

        if(tempBreakable!=null)
        {
            currentBreakable = tempBreakable;
            currentBreakable.Highlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Activatable tempActivatable = collision.GetComponent<Activatable>();
        Breakable tempBreakable = collision.GetComponentInChildren<Breakable>();

        if (currentActivatable != null && currentActivatable == tempActivatable)
        {
            currentActivatable.Highlight(false);
            currentActivatable = null;
        }

        if (currentBreakable != null && currentBreakable == tempBreakable)
        {
            currentBreakable.Highlight(false);
            currentBreakable = null;
        }
    }
}
