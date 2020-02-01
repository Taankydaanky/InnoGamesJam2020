using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameResolve : MonoBehaviour
{
    public UnityEvent successEvent, captainFail, mainFail, engineFail;
    public RocketSectionHealth captainSection, mainSection, engineSection;

    public void ExecuteEndCondition()
    {
        
        if(captainSection != null && captainSection.currentHealth <= 0 && captainFail != null)
        {
            captainFail.Invoke();
        }
        else if(mainSection != null && mainSection.currentHealth <= 0 && mainFail != null)
        {
            mainFail.Invoke();
        }
        else if(engineSection != null && engineSection.currentHealth <= 0 && engineFail != null)
        {
            engineFail.Invoke();
        }

        else if(successEvent != null)
        {
            successEvent.Invoke();
        }
    }
}
