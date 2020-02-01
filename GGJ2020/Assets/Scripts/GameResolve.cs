using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameResolve : MonoBehaviour
{
    public UnityEvent successEvent, captainFail, mainFail, engineFail;
    public RocketSectionHealth captainSection, mainSection, engineSection;
    public HealthManager manager;

    public void ExecuteEndCondition()
    {
        if(manager != null)
        {
            if(captainSection != null && captainSection.currentHealth <= 0 && captainFail != null)
            {
                captainFail.Invoke();
                return;
            }
            else if(mainSection != null && mainSection.currentHealth <= 0 && mainFail != null)
            {
                mainFail.Invoke();
                return;
            }
            else if(engineSection != null && engineSection.currentHealth <= 0 && engineFail != null)
            {
                engineFail.Invoke();
                return;
            }
        }

        if(successEvent != null)
        {
            successEvent.Invoke();
        }
    }
}
