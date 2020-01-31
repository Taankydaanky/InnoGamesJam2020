using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private bool broken;
    public bool isBroken {
        get => broken;
        set
        {
            if(value != broken)
            {
                broken = value;
                OnBreakStateChange();
            }
        }    
    }

    void OnBreakStateChange()
    {
        // Do something here
    }
}
