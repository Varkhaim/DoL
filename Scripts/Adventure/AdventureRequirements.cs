using UnityEngine;
using System.Collections;

public class AdventureRequirements
{
    AdvReq[] advReq = new AdvReq[10];
    int reqAmount = 0;

    public AdventureRequirements(params int[] reqs)
    {
        foreach (int req in reqs)
        {
            if (reqAmount==0)
                advReq[reqAmount] = new AdvReq(0, req);
            else
                advReq[reqAmount] = new AdvReq(1, req);
            reqAmount++;
        }
    }

    public bool GetAccessibility()
    {
        for (int i=0; i<reqAmount; i++)
        {
            if (!advReq[i].CheckIt())
                return false;
        }
        return true;
    }
    
    public string GetRequirements()
    {
        string temp = "";

        for (int i=0; i<reqAmount; i++)
        {
            temp += advReq[i].GetText();
        }

        return temp;
    }
}