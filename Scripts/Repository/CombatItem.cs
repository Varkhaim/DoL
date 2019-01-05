using UnityEngine;
using System.Collections;

public class CombatItem
{
    public int ID { get; set; }
    public string name { get; set; }
    public string desc { get; set; }
    public int itemvalue { get; set; }
    public string icon { get; set; }

    public CombatItem(int _ID, string _name, string _desc, int _itemvalue, string _icon)
    {
        ID = _ID;
        name = _name;
        desc = _desc;
        itemvalue = _itemvalue;
        icon = _icon;
    }

    public void TriggerEffect()
    {
        switch (ID)
        {
            case (int)COMBATITEM.MANA_POTION:
                {
                    GameCore.Core.myCaster.RestoreMana(30, 2);
                } break;
            case (int)COMBATITEM.SCROLL_OF_ALAPHI:
                {
                    GameCore.Core.CastAutoSpell((int)SPELL.SCROLL_OF_ALAPHI, null, 50);
                }
                break;
        }
    }
}