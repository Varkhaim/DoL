using UnityEngine;
using System.Collections;

public class AdvReq
{
    int reqType;
    int reqValue;

    public AdvReq(int _type, int _value)
    {
        reqType = _type;
        reqValue = _value;
    }

    public bool CheckIt()
    {
        switch (reqType)
        {
            case 0:
                {
                    if (GameCore.Core.chosenAccount.level >= reqValue)
                        return true;
                } break;
            case 1:
                {
                    if (GameCore.Core.chosenAccount.myProgress.missionComplete[reqValue])
                        return true;
                }
                break;
        }
        return false;
    }

    public string GetText()
    {
        string temp = "";

        //if (!((reqType == 0) && (reqValue == 0)))
        //    {
            if (CheckIt())
                temp += "<color=#00ff00>";
            else
                temp += "<color=#ff0000>";

            switch (reqType)
            {
                case 0:
                    {
                        temp += "Level: " + reqValue.ToString();
                    }
                    break;
                case 1:
                    {
                        temp += WorldMapCore.WMCore.adventureInfoHandler.adventureInfo[reqValue].MissionTitle;
                    }
                    break;
            }

            temp += "</color> \n";
       // }
        return temp;
    }
}