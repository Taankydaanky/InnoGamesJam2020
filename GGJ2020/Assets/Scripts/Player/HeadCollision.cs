﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadCollision : MonoBehaviour
{
    private Activatable currentButton;
    private ControlsMaster controlsMaster;

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
        currentButton?.Toggle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Button")
        {
            currentButton = collision.GetComponent<Activatable>();
            currentButton.Highlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Button")
        {
            currentButton.Highlight(false);
            currentButton = null;
            Debug.Log("un-highlight");
        }
    }
}
