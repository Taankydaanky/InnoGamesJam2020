using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightBreakable : Breakable
{
    private Light2D light;
    private float oldIntensity;

    protected override void Start()
    {
        light = GetComponent<Light2D>();
        oldIntensity = light.intensity;
        base.Start();
    }

    protected override void OnBreakStateChange()
    {
        if(isBroken)
        {
            oldIntensity = light.intensity;
            light.intensity = 0;
        }
        else
        {
            light.intensity = oldIntensity;
        }
    }

    public override void Highlight(bool highlight)
    {
        if(isBroken && highlight)
        {
            light.intensity = oldIntensity / 4;
        }
        else if(isBroken && !highlight)
        {
            light.intensity = 0;
        }
    }
}
