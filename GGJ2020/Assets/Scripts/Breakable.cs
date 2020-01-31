using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private bool broken;
    public string breakAnimation;
    public string repairAnimation;
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
        string animation;
        if(isBroken)
        {
            animation = breakAnimation;
        }
        else
        {
            animation = repairAnimation;
        }

        Animator ani = gameObject.GetComponent<Animator>();
        if(ani != null && animation != null && !animation.Equals(""))
        {
            ani.Play(animation);
        }
    }

    private void Start()
    {
        //OnBreakStateChange();
    }
}
