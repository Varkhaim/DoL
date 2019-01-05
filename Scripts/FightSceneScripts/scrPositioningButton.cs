using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POSITIONING
{
    SPREAD,
    STACK
}

public class scrPositioningButton : MonoBehaviour
{
    public POSITIONING buttonType;

    private void OnMouseDown()
    {
        if (buttonType == POSITIONING.SPREAD)
        GameCore.Core.troopsHandler.SpreadTroop();
        if (buttonType == POSITIONING.STACK)
            GameCore.Core.troopsHandler.StackTroop();
    }
}
