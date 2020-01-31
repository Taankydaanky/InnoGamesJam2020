using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public bool isBroken {
        get => isBroken;
        set
        {
            if(value != isBroken)
            {
                isBroken = value;
                OnBreakStateChange();
            }
        }    
    }

    void OnBreakStateChange()
    {
        // Do something here
    }
}
