using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollider : Activatable

{
    private Collider2D collider;

    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    public override void Activate()
    {
        base.Activate();
        collider.enabled = true;
    }
}
