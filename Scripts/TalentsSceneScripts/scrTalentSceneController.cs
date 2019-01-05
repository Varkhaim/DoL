using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrTalentSceneController : MonoBehaviour
{

    TalentsHandler talentsHandler = new TalentsHandler();

	void Start ()
    {
        //GameCore.Core.chosenAccount.myTalentTree.PointsCap = GameCore.Core.chosenAccount.level - 1;
        GameCore.Core.chosenAccount.LoadTemplates();
        talentsHandler.ShowTalentTree(GameCore.Core.chosenAccount.GetTemplate(GameCore.chosenChampion));
        talentsHandler.tree.CheckTalents();
        talentsHandler.tree.PointsCap = GameCore.Core.chosenAccount.level - 1;
        if (talentsHandler.tree.CheckTotalPoints() > talentsHandler.tree.PointsCap)
        talentsHandler.tree.Reset();
    }

    static public void SetTalentPointsText(string _text)
    {
        GameObject obj = GameObject.Find("TalentPointsText");
        if (obj != null)
        obj.GetComponent<Text>().text = _text;
    }

    public void ResetButtonClick()
    {
        talentsHandler.ResetTalents();
    }

    public void AcceptButtonClick()
    {
        GameCore.Core.chosenAccount.myTalentTree = talentsHandler.tree;
        GameCore.Core.chosenAccount.SaveTemplates();
        ScenesController.SControl.SwitchScene("DescendantScene");
    }
}
