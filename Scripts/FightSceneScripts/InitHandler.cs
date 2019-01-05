using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InitHandler
{
    private void InitCaster(GameCore core)
    {
        if (core.chosenAccount.myTalentTree != null)
            core.myTree = core.chosenAccount.myTalentTree;
        else
        {
            core.myTree = new TalentTree(GameCore.chosenChampion);
            core.myTree.DefaultTree();
        }

        core.myCaster = new Caster(core.myTree, core.chosenAccount);

        core.myCaster.ManaMax = 500f + core.chosenAccount.statKNG * 40;
        core.myCaster.ManaCurrent = core.myCaster.ManaMax;
    }

    private void InitSpells(GameCore core)
    {
        core.PlayerSpell = new Spell[core.spellsAmount];
        core.PlayerSpell = Spell.GenerateSpellKit(GameCore.chosenChampion, core.myCaster);
        for (int i = 0; i < core.spellsAmount; i++)
        {
            core.PlayerSpell[i] = new Spell(core.PlayerSpell[i].ID);
            core.PlayerSpell[i].myIcon = GameObject.Find("SpellMask" + (i + 1).ToString());
            core.PlayerSpell[i].myIcon.transform.GetChild(0).GetComponent<Image>().sprite = core.PlayerSpell[i].iconSprite;
            core.PlayerSpell[i].myIcon.transform.GetChild(0).GetComponent<SpellScript>().myID = i;
            core.PlayerSpell[i].myChargeCounter = GameObject.Find("ChargesText" + (i + 1).ToString());
        }
        core.interuptIcon = GameObject.Find("InteruptSpellMask");
        core.interuptText = GameObject.Find("InteruptCDText");

        core.DispelIcon = GameObject.Find("DispelMask");
        core.DispelText = GameObject.Find("DispelCDText");

        core.RefreshPickedSpellFrame();
    }

    // itemy
    private void InitItems(GameCore core)
    {
        core.combatItem[0] = core.itemRepository.GetObject((int)COMBATITEM.MANA_POTION);
        core.combatItem[1] = core.itemRepository.GetObject((int)COMBATITEM.SCROLL_OF_ALAPHI);
        core.combatItem[2] = core.itemRepository.GetObject((int)COMBATITEM.SCROLL_OF_ALAPHI);

        core.combatItemIcon[0] = GameObject.Find("CombatItemIcon1").GetComponent<Image>();
        core.combatItemIcon[1] = GameObject.Find("CombatItemIcon2").GetComponent<Image>();
        core.combatItemIcon[2] = GameObject.Find("CombatItemIcon3").GetComponent<Image>();

        core.RefreshItemIcons();
    }

    public void InitMainScene(GameCore core)
    {
        core.buffSystem = new BuffHandler(GameObject.Find("GCDBar").transform.position);

        core.chosenAccount.RefreshStats();
        core.myBlackScreen = GameObject.Find("BlackScreen");
        core.mySpellIcon = GameObject.Find("MySpellIcon");
        core.recount = new Recount();
        core.myEnemy = WorldMapCore.WMCore.adventureInfoHandler.GetEncounter(GameCore.Core.chosenAdventure, GameCore.difficulty);
        core.myEnemy.InitEncounter();

        GameCore.chosenChampion = DescendantPanelCore.DPCore.pickedChampion;

        InitClassSystems(core);

        core._mytip = GameObject.Find("Tooltip");
        
        GameObject.Find("ChampionPortrait").GetComponent<Image>().sprite = Champion.GetPortrait(GameCore.chosenChampion);

        InitCaster(core);
        InitSpells(core);
        InitItems(core);

        core.UpdateStats();
        core.ApplyAuras();
    }

    private void InitClassSystems(GameCore core)
    {
        switch (GameCore.chosenChampion)
        {
            case CHAMPION.PALADIN:
                {
                    core.paladinSparkHandler = new PaladinSparkHandler(core);
                }
                break;
            default: break;
        }
    }
}
