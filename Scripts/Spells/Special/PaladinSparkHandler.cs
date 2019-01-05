using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PaladinSparkHandler
{
    private int Sparks;
    private GameObject sparkObject;

    private GameCore core;

    public PaladinSparkHandler(GameCore _core)
    {
        Sparks = 0;
        core = _core;
    }

    public void AddSparks(int amount)
    {
        Sparks += amount;
        RefreshSparkCounter();
    }

    public int SpendSparks()
    {
        int amount = Mathf.Min(125, Sparks);
        Sparks -= amount;
        RefreshSparkCounter();
        return amount;
    }

    private void RefreshSparkCounter()
    {
        GameObject.Find("SpecialSlot1").transform.GetChild(0).GetComponent<Text>().text = Sparks.ToString();
    }
}

