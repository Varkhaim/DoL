using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescendantPanelCore
{
    // Singleton
    static private DescendantPanelCore dpCore;

    private DescendantPanelCore()
    {

    }

    static public DescendantPanelCore DPCore
    {
        get
        {
            if (dpCore == null)
                dpCore = new DescendantPanelCore();
            return dpCore;
        }
    }
    // END Singleton
    //**********************************************************************************

    public CHAMPION pickedChampion = CHAMPION.PALADIN;
    private CHAMPION lastChampion = CHAMPION.PALADIN;

    public bool UILoading = false;

    // elementy UI
    GameObject championNameText;
    GameObject championPortrait;
    GameObject championLevelText;
    GameObject championExpText;
    GameObject championStatText;

    GameObject[] AbilityIcon = new GameObject[5];
    GameObject[] AbilityName = new GameObject[5];
    GameObject[] AbilityDescription = new GameObject[5];

    // UI content
    private string championName = "";
    private Sprite championPortraitSprite;

    Sprite[] AbilityIconSprite = new Sprite[5];
    string[] AbilityNameString = new string[5];
    string[] AbilityDescriptionString = new string[5];

    public string GetSpellInfo(SPELL _spellID)
    {
        string temp = "";

        SpellInfo spInfo = GameCore.Core.spellRepository.Get(_spellID);

        temp += spInfo.GetSpellDescription();

        return temp;
    }

    public void NextChampion()
    {
        if (!UILoading)
        {
            pickedChampion += 1;
            if (pickedChampion > lastChampion)
                pickedChampion = lastChampion;
            else
                LoadUIContent();
        }
        GameCore.chosenChampion = pickedChampion;
    }

    public void PreviousChampion()
    {
        if (!UILoading)
        {
            pickedChampion -= 1;
            if (pickedChampion < (CHAMPION)1)
                pickedChampion = (CHAMPION)1;
            else
                LoadUIContent();
        }
        GameCore.chosenChampion = pickedChampion;
    }

    private void UpdateChampionInfo()
    {
        championNameText.GetComponent<Text>().text = championName;
        championPortrait.GetComponent<Image>().sprite = championPortraitSprite;
        championLevelText.GetComponent<Text>().text = GameCore.Core.chosenAccount.level.ToString();
        championExpText.GetComponent<Text>().text = GameCore.Core.chosenAccount.GetExpString();
        championStatText.GetComponent<Text>().text = GetAccountStats();

        for (int i = 0; i < 5; i++)
        {
            AbilityIcon[i].GetComponent<Image>().sprite = AbilityIconSprite[i];
            AbilityName[i].GetComponent<Text>().text = AbilityNameString[i];
            AbilityDescription[i].GetComponent<Text>().text = AbilityDescriptionString[i];
        }
    }

    private string GetAccountStats()
    {
        string temp = "";
        Account acc = GameCore.Core.chosenAccount;

        acc.RefreshStats();
        temp += "Intelligence: " + acc.statINT;
        temp += "\nKnowledge: " + acc.statKNG;
        temp += "\nFocus: " + acc.statFCS;
        temp += "\nPower: " + acc.statPWR;

        return temp;
    }

    public void LoadUIContent()
    {
        UILoading = true;
        championName = Champion.GetName(pickedChampion);
        championPortraitSprite = Champion.GetPortrait(pickedChampion);

        for (int i = 0; i < 5; i++)
        {
            AbilityIconSprite[i] = Champion.GetSpellIcon(pickedChampion, i);
            AbilityNameString[i] = Champion.GetSpellName(pickedChampion, i);
            AbilityDescriptionString[i] = Champion.GetSpellDescription(pickedChampion, i);
        }
    }

    public void InstantLoadUIContent()
    {
        championName = Champion.GetName(pickedChampion);
        championPortraitSprite = Champion.GetPortrait(pickedChampion);

        for (int i = 0; i < 5; i++)
        {
            AbilityIconSprite[i] = Champion.GetSpellIcon(pickedChampion, i);
            AbilityNameString[i] = Champion.GetSpellName(pickedChampion, i);
            AbilityDescriptionString[i] = Champion.GetSpellDescription(pickedChampion, i);
        }

        UpdateChampionInfo();
    }

    public void InitUIReferences()
    {
        championNameText = GameObject.Find("ChampionNameText");
        championPortrait = GameObject.Find("ChampionPortrait");
        championLevelText = GameObject.Find("LevelText");
        championExpText = GameObject.Find("ExpText");
        championStatText = GameObject.Find("AttributesText");

        for (int i = 0; i < 5; i++)
        {
            AbilityIcon[i] = GameObject.Find("AbilityIcon" + i.ToString());
            AbilityName[i] = GameObject.Find("AbilityName" + i.ToString());
            AbilityDescription[i] = GameObject.Find("AbilityDescription" + i.ToString());
        }

    }

    float alpha = 1f;
    bool isChanged = false;
    public void UILoadHandler()
    {
        if (UILoading)
        {
            if (isChanged)
            {
                alpha += 0.04f;
                if (alpha >= 1f)
                {
                    isChanged = false;
                    UILoading = false;
                }
            }
            else
            {
                alpha -= 0.04f;
                if (alpha <= 0f)
                {
                    isChanged = true;
                    UpdateChampionInfo();
                }
            }
        }
        SetUIAlpha(alpha);
    }

    private void SetUIAlpha(float _alpha)
    {
        championNameText.GetComponent<CanvasRenderer>().SetAlpha(_alpha);
        championPortrait.GetComponent<CanvasRenderer>().SetAlpha(_alpha);

        for (int i = 0; i < 5; i++)
        {
            AbilityIcon[i].GetComponent<CanvasRenderer>().SetAlpha(_alpha);
            AbilityName[i].GetComponent<CanvasRenderer>().SetAlpha(_alpha);
            AbilityDescription[i].GetComponent<CanvasRenderer>().SetAlpha(_alpha);
        }
    }
}
