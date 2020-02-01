using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : ScriptableObject
{ 
    public enum Sections : int { TOP_SECTION, MIDDLE_SECTION, BOTTOM_SECTION };
    public RocketSectionHealth[] sections = new RocketSectionHealth[3];

    public int GetSectionHealth(int section)
    {
        RocketSectionHealth rsh = sections[section];
        if (rsh != null)
        {
            return rsh.currentHealth;
        }
        return -1;
    }

    public void SetSectionHealth(int section, int health)
    {
        RocketSectionHealth rsh = sections[section];
        if (rsh != null)
        {
            rsh.currentHealth = health;
        }
    }

    public int RemoveSectionHealth(int section, int healthToRemove)
    {
        RocketSectionHealth rsh = sections[section];
        if(rsh != null)
        {
            rsh.currentHealth -= healthToRemove;
            return rsh.currentHealth;
        }

        return -1;
    }

    public bool AnySectionDestroyed()
    {
        bool dest = false;
        foreach(RocketSectionHealth rsh in sections)
        {
            if(rsh.currentHealth <= 0)
            {
                dest = true;
                break;
            }
        }

        return dest;
    }
}
