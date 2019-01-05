using UnityEngine;
using System.Collections;

public class GCDBar : MonoBehaviour
{

    void Start()
    {
        GameCore.Core.spellCastHandler.myGCDbar = gameObject;
    }

}
