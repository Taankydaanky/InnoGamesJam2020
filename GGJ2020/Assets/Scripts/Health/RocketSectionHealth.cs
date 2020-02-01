using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RocketSectionHealth", menuName = "ScriptableObjects/RocketSectionHealth", order = 1)]
public class RocketSectionHealth : ScriptableObject
{
    private int _currentHealth;

    public int maxHealth;
    public int currentHealth {
        get => _currentHealth;
        set
        {
            if(value < 0)
            {
                value = 0;
            }
            _currentHealth = value;
            //imageToColor.color = gradient.Evaluate(((float)currentHealth) / maxHealth);
        }
    }

    //Gradient gradient;
    //GradientColorKey[] colorKey;
    //GradientAlphaKey[] alphaKey;

    //void Start()
    //{
    //    gradient = new Gradient();

    //    colorKey = new GradientColorKey[3];
    //    colorKey[0].color = Color.red;
    //    colorKey[0].time = 0.0f;
    //    colorKey[1].color = Color.yellow;
    //    colorKey[1].time = ((float)maxHealth - 1) / maxHealth;
    //    colorKey[2].color = Color.green;
    //    colorKey[2].time = 1f;

    //    alphaKey = new GradientAlphaKey[2];
    //    alphaKey[0].alpha = 1.0f;
    //    alphaKey[0].time = 0.0f;
    //    alphaKey[1].alpha = 1.0f;
    //    alphaKey[1].time = 1.0f;

    //    gradient.SetKeys(colorKey, alphaKey);
    //    currentHealth = maxHealth;
    //}
}
