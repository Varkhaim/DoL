using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrDescendantSceneController : MonoBehaviour
{
    void Start()
    {
        DescendantPanelCore.DPCore.InitUIReferences();
        DescendantPanelCore.DPCore.InstantLoadUIContent();
        GameCore.Core.chosenAccount.ApplyTempIncreases();
    }

    public void LoadPreviousScene()
    {
        ScenesController.SControl.SwitchScene("AdventureScene");
    }

    public void LoadTalentScene()
    {
        ScenesController.SControl.SwitchScene("TalentsScene");
    }

    public void GoToFightScene()
    {
        ScenesController.SControl.SwitchScene("FightScene");
    }

    public void GoToTroopsScene()
    {
        ScenesController.SControl.SwitchScene("TroopsScene");
    }

    public void GoToScoutScene()
    {
        ScenesController.SControl.SwitchScene("ScoutScene");
    }

    public void OnStartClick()
    {
        GoToFightScene();
    }

    public void OnLeftArrowClick()
    {
        DescendantPanelCore.DPCore.PreviousChampion();
    }

    public void OnRightArrowClick()
    {
        DescendantPanelCore.DPCore.NextChampion();
    }

    void Update()
    {
        if (DescendantPanelCore.DPCore.UILoading)
        {
            DescendantPanelCore.DPCore.UILoadHandler();
        }
    }
}
