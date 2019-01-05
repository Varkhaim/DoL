using UnityEngine;
using System.Collections;

public class scrAdventureButton : MonoBehaviour
{
    public int advID = 0;

    void OnMouseDown()
    {
        WorldMapCore.WMCore.SetChosenAdventure(advID, transform.position);
    }

    public bool available = false;

    void CheckColors()
    {
        if (available)
            GetComponent<CanvasRenderer>().SetColor(Color.green);
        else
            GetComponent<CanvasRenderer>().SetColor(Color.red);
    }

    bool _check = false;

    void CheckMission()
    {
        if (WorldMapCore.WMCore.adventureInfoHandler.adventureInfo[advID] != null)
        {
            available = WorldMapCore.WMCore.adventureInfoHandler.adventureInfo[advID].Requirements.GetAccessibility();
            CheckColors();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameCore.Core.chosenAccount != null) && (!_check))
        {
            _check = true;
            CheckMission();
        }
    }
}
