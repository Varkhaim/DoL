using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldMapCore
{
    
    // Singleton
    static private WorldMapCore wmCore;

    private WorldMapCore()
    {

    }

    static public WorldMapCore WMCore
    {
        get
        {
            if (wmCore == null)
                wmCore = new WorldMapCore();
            return wmCore;
        }
    }
    // END Singleton
    //**********************************************************************************

    private int chosenAdventure = -1;
    private GameObject missionMark;
    public AdventureInfoHandler adventureInfoHandler = new AdventureInfoHandler();

    private bool MissionMarkUpdate(int _advID, Vector3 _position)
    {
        if (chosenAdventure != _advID)
        {
            chosenAdventure = _advID;
            if (missionMark == null)
            {
                missionMark = Object.Instantiate(Resources.Load("MissionMark")) as GameObject;
                missionMark.transform.SetParent(GameObject.Find("Canvas").transform);
            }
            missionMark.transform.position = _position;
            missionMark.transform.localScale = new Vector3(2, 2, 1);
            return true;
        }
        else
            return false;
    }

    public void SetChosenAdventure(int _advID, Vector3 _position)
    {
        if (MissionMarkUpdate(_advID, _position))
        {
            chosenAdventure = _advID;
            GameCore.Core.chosenAdventure = new Adventure(_advID);
            AdventureInfo adv = adventureInfoHandler.GetInfo(_advID);
            GameObject.Find("MissionTitle").GetComponent<scrMissionTitle>().SetContent(adv.MissionTitle);
            GameObject.Find("MissionDescription").GetComponent<scrMissionDescription>().SetContent(adv.MissionDescription);
            GameObject.Find("Requirements").GetComponent<scrMissionRequirements>().SetContent(adv.Requirements.GetRequirements());
            Sprite[] temp = adv.encountersInfo.GetEncountersPortraits();
            GameObject.Find("EncounterPortrait1").GetComponent<RawImage>().texture = temp[0].texture;
            GameObject.Find("EncounterPortrait2").GetComponent<RawImage>().texture = temp[1].texture;
            GameObject.Find("EncounterPortrait3").GetComponent<RawImage>().texture = temp[2].texture;
            GameObject.Find("EncounterPortrait4").GetComponent<RawImage>().texture = temp[3].texture;
        }
    }

    public void GoToDescendantScene()
    {
        if (WorldMapCore.WMCore.adventureInfoHandler.adventureInfo[chosenAdventure].Requirements.GetAccessibility())
        {
            ScenesController.SControl.SwitchScene("DescendantScene");
        }
    }
}
