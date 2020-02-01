using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ElevatorUp" && playerMovement.isInElevatorMoveDir == 0)
        {
            Debug.Log("trigger enter with " + collision.name + " " + collision.tag);
            playerMovement.isInElevatorMoveDir = 1;
            playerMovement.elevatorGoal = collision.GetComponent<Elevator>().elevatorGoal;
        }
        else if(collision.tag == "ElevatorDown" && playerMovement.isInElevatorMoveDir == 0)
        {
            Debug.Log("trigger enter with " + collision.name+" "+collision.tag);
            playerMovement.isInElevatorMoveDir = -1;
            playerMovement.elevatorGoal = collision.GetComponent<Elevator>().elevatorGoal;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "ElevatorUp" || collision.tag == "ElevatorDown"))
        {
            Debug.Log("trigger exit with " + collision.name + " " + collision.tag);
            playerMovement.isInElevatorMoveDir = 0;
        }
    }

}
