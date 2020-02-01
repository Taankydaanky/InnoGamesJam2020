using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ElevatorUp" && playerMovement.isInElevatorMoveDir == 0)
        {
            playerMovement.isInElevatorMoveDir = 1;
            playerMovement.elevatorGoal = collision.GetComponent<Elevator>().elevatorGoal;
        }
        else if(collision.tag == "ElevatorDown" && playerMovement.isInElevatorMoveDir == 0)
        {
            playerMovement.isInElevatorMoveDir = -1;
            playerMovement.elevatorGoal = collision.GetComponent<Elevator>().elevatorGoal;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "ElevatorUp" || collision.tag == "ElevatorDown"))
        {
            playerMovement.isInElevatorMoveDir = 0;
        }
    }

}
