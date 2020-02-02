using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendingMachineButton : Activatable
{
    [SerializeField] private Enums.RepairKits repairKit;
    [SerializeField] private float animationTime = 1f;

    private PlayerItems playerItems;


    override protected void Start()
    {
        base.Start();
        playerItems = FindObjectOfType<PlayerItems>();
    }

    public override void Activate()
    {
        base.Activate();
        if (repairKit != Enums.RepairKits.None)
        {
            playerItems.currentRepairKit = repairKit;
        }
        StartCoroutine("DeactivateAfterTime", animationTime);
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        base.Deactivate();
    }
}
