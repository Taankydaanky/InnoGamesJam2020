using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager healthManager;

    public enum Sections : int { TOP_SECTION, MIDDLE_SECTION, BOTTOM_SECTION };
    public RocketSectionHealth[] sections = new RocketSectionHealth[3];
    [SerializeField] private Image[] sectionImages;
    [SerializeField, Tooltip("dead to alive")] private Gradient healthGradient;

    private void Awake()
    {
        if (healthManager == null)
        {
            healthManager = this;
        }

        if (healthManager != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        

        for(int i=0;i<sectionImages.Length;i++)
        {
            SetSectionHealth(i,sections[i].maxHealth);
        }
    }

    public void ShowHealth(int section)
    {
        RocketSectionHealth rsh = sections[section];
        sectionImages[section].color = healthGradient.Evaluate(((float)rsh.currentHealth) / rsh.maxHealth);
    }

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
            sectionImages[section].color = healthGradient.Evaluate(((float)rsh.currentHealth) / rsh.maxHealth);
        }
    }

    public int RemoveSectionHealth(int section, int healthToRemove)
    {
        RocketSectionHealth rsh = sections[section];
        if(rsh != null)
        {
            rsh.currentHealth -= healthToRemove;
            sectionImages[section].color = healthGradient.Evaluate(((float)rsh.currentHealth) / rsh.maxHealth);
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
