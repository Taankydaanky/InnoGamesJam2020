using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float bodySpeed, headSpeed;
    [SerializeField] private float maxHeadDistance;

    private Rigidbody2D body, head;
    private ControlsMaster controlsMaster;
    private Vector2 movement;
    private float headMinY, headMaxY;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        head = transform.GetChild(0).GetComponent<Rigidbody2D>();
        headMinY = head.transform.localPosition.y;
        headMaxY = head.transform.localPosition.y + maxHeadDistance;

        Debug.Log("min " + headMinY + " max " + headMaxY);
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


    void FixedUpdate()
    {
        body.velocity = new Vector2(movement.x * bodySpeed * Time.fixedDeltaTime, body.velocity.y);

        float headGoalY = movement.y > 0 ? headMaxY : headMinY;
        Vector2 headGoal = head.transform.TransformPoint(new Vector2(0, headGoalY));

        //head.transform.position = Vector2.MoveTowards(head.transform.position, headGoal, movement.y * headSpeed * Time.fixedDeltaTime);

        Vector2 moveDir = Vector2.MoveTowards(head.transform.position, headGoal, movement.y * headSpeed * Time.fixedDeltaTime);


        if (movement.y != 0)
        {
            Debug.Log(head.transform.position+" global "+headGoal+" moveDir "+moveDir);
        }
    }

}
