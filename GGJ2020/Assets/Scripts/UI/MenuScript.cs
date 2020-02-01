using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MenuScript : MonoBehaviour
{
    public UnityEvent exitEvent;
    private ControlsMaster control;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(control == null)
        {
            control = new ControlsMaster();
            control.Player.UIAccept.performed += UIAccept;
        }
        control.Player.Enable();
    }

    private void OnDisable()
    {
        control.Player.Disable();
    }

    private void UIAccept(InputAction.CallbackContext obj)
    {
        enabled = false;
        // Call event
        if(exitEvent != null)
        {
            exitEvent.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
