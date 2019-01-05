using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrScoutScene : MonoBehaviour
{
    public Encounter myEnc;
    public Image encounterPortrait;
    public Text myTip;
    public Text myBossName;

    void Start()
    {
        AdventureInfo myInfo = WorldMapCore.WMCore.adventureInfoHandler.adventureInfo[GameCore.Core.chosenAdventure.ID];
        myEnc = WorldMapCore.WMCore.adventureInfoHandler.GetEncounter(GameCore.Core.chosenAdventure, GameCore.difficulty);
        encounterPortrait.sprite = EncountersInfo.GetPortrait(myInfo.encountersInfo.encInf[GameCore.Core.chosenAdventure.progress]);
        myBossName.text = myEnc.name;

        for (int i = 0; i < myEnc.spellsAmount; i++)
        {
            GameObject myIcon = Instantiate(Resources.Load("BossAbilityIcon"), new Vector3(-8f, 4.25f - (i * 1.25f), 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            myIcon.transform.parent = GameObject.Find("Canvas").transform;
            myIcon.transform.localScale = new Vector3(1, 1, 1);
            myIcon.GetComponent<scrBossAbility>().espell = myEnc.mySpell[i];
            myIcon.GetComponent<scrBossAbility>().myText = myTip;
            myIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(myEnc.mySpell[i].icon);
        }
    }

    public void AcceptButtonClick()
    {
        ScenesController.SControl.SwitchScene("DescendantScene");
    }
}
