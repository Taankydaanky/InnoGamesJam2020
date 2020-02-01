using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float bodySpeed, headSpeed, elevatorSpeed;
    [SerializeField] private float maxHeadDistance;
    [SerializeField] private float disableColliderTime, fallTime;

    private Rigidbody2D body;
    private Collider2D bodyCollider;
    private Transform head;
    private ControlsMaster controlsMaster;
    private Vector2 movement;
    private float headMinY;
    private bool isFacingLeft;
    private Vector2 currentElevatorGoal;
    private Animator bodyAnimator, headAnimator;

    public int elevatorMoveDir { get; private set; }
    public int isInElevatorMoveDir { get; set; } //1 = up; -1 = down
    public Vector2 elevatorGoal { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();
        bodyAnimator = GetComponent<Animator>();
        head = transform.GetChild(0);
        headAnimator = head.GetComponent<Animator>();
        headMinY = head.transform.localPosition.y;
        isFacingLeft = true;
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
        if(Mathf.Abs(movement.x)>0)
        {
            SetFaceDirection();

            bodyAnimator.SetBool("Move", true);
            headAnimator.SetBool("Move", true);
        }
        else
        {
            bodyAnimator.SetBool("Move", false);
            headAnimator.SetBool("Move", false);
        }
        
    }


    void FixedUpdate()
    {
        if (elevatorMoveDir == 0)
        {
            body.velocity = new Vector2(movement.x * bodySpeed * Time.fixedDeltaTime, body.velocity.y);

            float headGoalY = movement.y > 0 ? headMinY + maxHeadDistance : headMinY;
            Vector2 headGoal = transform.TransformPoint(new Vector2(0, headGoalY));

            head.transform.position = Vector2.MoveTowards(head.transform.position, headGoal, Mathf.Abs(movement.y) * headSpeed * Time.fixedDeltaTime);
            UpdateAnimator(headGoalY);

            if ((Vector2)head.transform.position == headGoal && elevatorMoveDir == 0)
            {
                if (isInElevatorMoveDir == 1 && headGoalY > headMinY && movement.y > 0.75f)     //Todo: no magic numbers!
                {
                    MoveUp();
                }
                else if (isInElevatorMoveDir == -1 && headGoalY == headMinY && movement.y < -0.75f)      //Todo: no magic numbers!
                {
                    MoveDown();
                }
            }

        }
        else if (Mathf.Abs(elevatorMoveDir) == 1)
        {
            body.velocity = Vector2.zero;
            Vector2 goal = new Vector2(transform.position.x, currentElevatorGoal.y);
            transform.position = Vector2.MoveTowards(transform.position, goal, elevatorSpeed * Time.fixedDeltaTime);
            if((Vector2)transform.position == goal)
            {
                elevatorMoveDir = 0;
            }
        }

        
    }

    private void SetFaceDirection()
    {
        bool oldIsFacingLeft = isFacingLeft;
        isFacingLeft = movement.x <= 0;
        if(oldIsFacingLeft != isFacingLeft)
        {
            transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
        }
    }

    private void MoveUp()
    {
        currentElevatorGoal = elevatorGoal;
        elevatorMoveDir = 1;
        //start animation?
    }

    private void MoveDown()
    {
        currentElevatorGoal = elevatorGoal;
        elevatorMoveDir = -1;
        //start animation?
        StartCoroutine(DisableCollider(disableColliderTime, fallTime));
    }

    private IEnumerator DisableCollider(float colliderTime, float moveTime)
    {
        bodyCollider.enabled = false;
        yield return new WaitForSeconds(colliderTime);
        bodyCollider.enabled = true;
    }

    private void UpdateAnimator(float headGoalY)
    {
        if (headGoalY == headMinY + maxHeadDistance)
        {
            headAnimator.SetBool("Fly", true);
            bodyAnimator.SetBool("Fly", true);
        }
        else if(head.transform.localPosition.y == headMinY)
        {
            headAnimator.SetBool("Fly", false);
            bodyAnimator.SetBool("Fly", false);
        }
    }
}
