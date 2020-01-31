using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float bodySpeed, headSpeed;

    private Rigidbody2D body, head;
    private ControlsMaster controlsMaster;
    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        head = transform.GetChild(0).GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if(controlsMaster == null)
        {
            controlsMaster = new ControlsMaster();
            controlsMaster.Player.Move.performed += Move;
            controlsMaster.Player.Move.canceled += Move;
            controlsMaster.Player.Move.canceled += context => movement = Vector2.zero;
        }

        controlsMaster.Player.Enable();
    }

    private void OnDisable()
    {
        controlsMaster.Player.Disable();
    }

    private void Move(InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = new Vector2(movement.x * bodySpeed * Time.deltaTime, body.velocity.y);
        head.velocity = new Vector2(body.velocity.x, movement.y * headSpeed * Time.deltaTime);
    }
}
