using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float bodySpeed, headSpeed;
    [SerializeField] private float maxHeadDistance;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
    private Transform head;
    private ControlsMaster controlsMaster;
    private Vector2 movement;
    private float headMinY;
    private bool isFacingLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        head = transform.GetChild(0);
        headMinY = head.transform.localPosition.y;
        SetFaceDirection();
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
        SetFaceDirection();

    }


    void FixedUpdate()
    {
        body.velocity = new Vector2(movement.x * bodySpeed * Time.fixedDeltaTime, body.velocity.y);

        float headGoalY = movement.y > 0 ? headMinY+maxHeadDistance : headMinY;
        Vector2 headGoal = transform.TransformPoint(new Vector2(0, headGoalY));

        head.transform.position = Vector2.MoveTowards(head.transform.position, headGoal, Mathf.Abs(movement.y) * headSpeed * Time.fixedDeltaTime);

    }

    private void SetFaceDirection()
    {
        bool oldIsFacingLeft = isFacingLeft;
        isFacingLeft = movement.x <= 0;
        if(oldIsFacingLeft != isFacingLeft)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            Debug.Log("switch face direction");
        }
    }
}
