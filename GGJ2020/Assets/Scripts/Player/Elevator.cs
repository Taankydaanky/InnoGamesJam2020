using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform goal;

    public Vector2 elevatorGoal
    {
        get
        {
            return goal.position;
        }
    }
}
