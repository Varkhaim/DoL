using UnityEngine;
using System.Collections;

public class Recount
{
    public Entry[] entry = new Entry[20];
    public int amount = 0;
    private int totalhealing = 1;

    public Recount()
    {

    }

    public int FindEntry(string _name)
    {
        for (int i=0; i<amount; i++)
        {
            if (entry[i].name == _name)
                return i;
        }
        return -1;
    }

    public void AddEntry(HEALSOURCE _name, int _value, int _overheal)
    {
        int _ent = FindEntry(GetNameBySource(_name));
        if (_ent == -1)
        {
            entry[amount] = new Entry(GetNameBySource(_name), _value, _overheal);
            amount++;
        }
        else
        {
            entry[_ent].healing += _value;
            entry[_ent].overhealing += _overheal;
        }
        totalhealing += _value;
        SortEntries();
    }

    private string GetNameBySource(HEALSOURCE source)
    {
        switch (source)
        {
            case HEALSOURCE.DIVNITY_SHIELD: return "Divinity - Shield";
            case HEALSOURCE.FADING_LIGHT: return "Fading Light";
            case HEALSOURCE.SHADOWMEND: return "Shadowmend";
            case HEALSOURCE.TWILIGHT_BEAM: return "Twilight Beam";
            case HEALSOURCE.WOK_FAITH: return "Word of Kings: Faith";
            case HEALSOURCE.WOK_LOYALTY: return "Word of Kings: Loyalty";
            case HEALSOURCE.WOK_COURAGE: return "Word of Kings: Courage";
            case HEALSOURCE.WOK_LIGHT: return "Word of Kings: Light";
            case HEALSOURCE.SOOTHING_VOID: return "Soothing Void";
            case HEALSOURCE.EMPATHY: return "Empathy";
            case HEALSOURCE.DREAM: return "Dream";
            case HEALSOURCE.MOONLIGHT: return "Moonlight";
            case HEALSOURCE.SHADOWSURGE: return "Shadowsurge";
            case HEALSOURCE.ROYALTY: return "Royalty";
            case HEALSOURCE.GUIDANCE_OF_RAELA: return "Guidance of Raela";
            case HEALSOURCE.SCROLL_OF_ALAPHI: return "Scroll of Renew";
            default: return "_MISSING_";
        }
    }

    private void SortEntries()
    {
        Entry temp = null;
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < amount - i - 1; j++)
                if (entry[j].healing < entry[j + 1].healing)
                {
                    temp = entry[j];

                    entry[j] = entry[j + 1];

                    entry[j + 1] = temp;
                }
        }
    }

    public string GetRecount(bool simple=false)
    {
        string _temp = "";

        for (int i=0; i<amount; i++)
        {
            switch (i)
            {
                case 0: _temp += "<color=#00ff00ff>"; break;
                case 1: _temp += "<color=#ffff00ff>"; break;
                case 2: _temp += "<color=#00ffffff>"; break;
                case 3: _temp += "<color=#ff0000ff>"; break;
                case 4: _temp += "<color=#ff00ffff>"; break;
                case 5: _temp += "<color=#0080ffff>"; break;
                default: _temp += "<color=#ffffffff>"; break;
            }
            string percentString = "";
            string overhealString = "";

            float _percent = (int)(((float)entry[i].healing / (float)totalhealing) * 100);
            if (entry[i].overhealing > 0)
                overhealString = " (" + entry[i].overhealing.ToString() + ")";

            if (!simple)
            percentString = "(" + _percent.ToString() + "%)";
            _temp += entry[i].name + ":</color> " + entry[i].healing.ToString() + " " + percentString + overhealString + System.Environment.NewLine;
        }

        return _temp;
    }
}
