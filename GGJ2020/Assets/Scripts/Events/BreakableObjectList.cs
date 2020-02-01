using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectList : ScriptableObject
{
    public Breakable[] breakableObjects;

    public int GetBrokenCount()
    {
        int broken = 0;
        foreach(Breakable ba in breakableObjects)
        {
            if(ba.isBroken)
            {
                broken++;
            }
        }
        return broken;
    }

    public int BreakObjects(int min, int max)
    {
        int toBreak = Random.Range(min, max + 1);
        int newlyBroken = 0;

        List<Breakable> selectable = new List<Breakable>(breakableObjects);

        while(selectable.Count > 0 && newlyBroken < toBreak)
        {
            Breakable ba = selectable[Random.Range(0, selectable.Count)];
            selectable.Remove(ba);

            if(!ba.isBroken)
            {
                ba.isBroken = true;
                newlyBroken++;
            }
        }

        return newlyBroken;
    }
}
