using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPrewarning : MonoBehaviour
{
    public EventGenerator eventGenerator;
    

    void Update()
    {
        if(eventGenerator != null)
        {
            foreach (GeneratedEvent ev in eventGenerator.environmentGeneratedEvents)
            {
                Event e = ev.execEvent;

                if (e.eventIcon != null)
                {

                }
                else
                {
                    Debug.LogWarning("The event " + e + " has no eventIcon attached but is used as an environment event.");
                }
            }
        }
    }
}
