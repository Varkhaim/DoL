using UnityEngine;
using System.Collections;

public class Item
{
    public string name;
    public string desc;
    public int diff = 0;
    public Sprite mySprite;
    public int slot = 0; // typ itemu (ktore miejsce zajmuje)

    public int statINT = 0; // INTelligence - redukuje cooldown spelli o x%
    public int statKNG = 0; // KNowledGe - zwieksza pule many o x%
    public int statWSD = 0; // WiSDom - zwieksza szanse na proce o x%
    public int statFCS = 0; // FoCuS - zwieksza szanse na krytyczne uderzenie o x%
    public int statPWR = 0; // PoWeR - zwieksza sile leczenia o x%

    public int ID;

    public const int HEAD = 0;
    public const int NECKLACE = 1;
    public const int CLOAK = 2;
    public const int CHEST = 3;
    public const int GLOVES = 4;
    public const int MAINHAND = 5;
    public const int OFFHAND = 6;
    public const int LEGS = 7;
    public const int BOOTS = 8;
    public const int TRINKET = 9;
    public const int RING = 10;

    public Item(int _ID, int _diff)
    {
        ID = _ID;
        name = GetName(ID);
        desc = GetDesc(ID);
        diff = _diff;
        SetStatsByToken(GetToken(ID));
        mySprite = GetSprite(ID);
        slot = GetSlot(ID);
    }

    public string GetDescription()
    {
        string _text = "";

        _text += name + System.Environment.NewLine;

        _text += GetQuality();

        _text += GetDetails();

        _text += "<color=#DAA520>" + System.Environment.NewLine + GetDesc(ID) + "</color>" + System.Environment.NewLine + System.Environment.NewLine;

        _text += GetSlotName();

        return _text;
    }

    private void SetStatsByToken(string token)
    {
        string[] parts = token.Split('|');

        int.TryParse(parts[0], out statINT);
        int.TryParse(parts[1], out statKNG);
        int.TryParse(parts[2], out statWSD);
        int.TryParse(parts[3], out statFCS);
        int.TryParse(parts[4], out statPWR);

        statINT *= (diff + 1);
        statKNG *= (diff + 1);
        statWSD *= (diff + 1);
        statFCS *= (diff + 1);
        statPWR *= (diff + 1);
    }

    public string GetDetails()
    {
        string _temp = "";

        if (statINT > 0)
            _temp += "INT +" + statINT.ToString() + System.Environment.NewLine;
        if (statKNG > 0)
            _temp += "KNG +" + statKNG.ToString() + System.Environment.NewLine;
        if (statWSD > 0)
            _temp += "WSD +" + statWSD.ToString() + System.Environment.NewLine;
        if (statFCS > 0)
            _temp += "FCS +" + statFCS.ToString() + System.Environment.NewLine;
        if (statPWR > 0)
            _temp += "PWR +" + statPWR.ToString();

        return _temp;
    }

    public string GetName()
    {
        string _temp = "";

        switch (diff)
        {
            case 0: _temp = "<color=#ffffffff>"; break;
            case 1: _temp = "<color=#008800ff>"; break;
            case 2: _temp = "<color=#0080ffff>"; break;
            case 3: _temp = "<color=#ff00ffff>"; break;
            case 4: _temp = "<color=#ff8800ff>"; break;
            default: _temp = "<color=#00ff00ff>"; break;
        }

        _temp += name + "</color>";

        return _temp;
    }

    public string GetQuality()
    {
        string _temp = "";

        switch (diff)
        {
            case 0: _temp = "<color=#ffffffff>" + System.Environment.NewLine + "Common" + System.Environment.NewLine + "</color>"; break;
            case 1: _temp = "<color=#008800ff>" + System.Environment.NewLine + "Enchanted" + System.Environment.NewLine + "</color>"; break;
            case 2: _temp = "<color=#0080ffff>" + System.Environment.NewLine + "Rare" + System.Environment.NewLine + "</color>"; break;
            case 3: _temp = "<color=#ff00ffff>" + System.Environment.NewLine + "Epic" + System.Environment.NewLine + "</color>"; break;
            case 4: _temp = "<color=#ff8000ff>" + System.Environment.NewLine + "Legendary" + System.Environment.NewLine + "</color>"; break;
            default: _temp = "<color=#00ff00ff>" + System.Environment.NewLine + "Common" + System.Environment.NewLine + "</color>"; break;
        }

        return _temp + System.Environment.NewLine;
    }

    public Color GetColor()
    {
        switch (diff)
        {
            case 0: return new Color(1, 1, 1, 0.5f);
            case 1: return new Color(0, 0.5f, 0, 0.5f);
            case 2: return new Color(0, 0.5f, 1, 0.5f);
            case 3: return new Color(1, 0, 1, 0.5f);
            case 4: return new Color(1, 0.5f, 0, 0.5f);
            default: return new Color(1, 1, 1, 0.5f);
        }
    }

    

    public string GetSlotName()
    {
        string _slot = "<i>";

        switch (slot)
        {
            case HEAD: _slot += "Head"; break;
            case NECKLACE: _slot += "Neck"; break;
            case CLOAK: _slot += "Back"; break;
            case CHEST: _slot += "Chest"; break;
            case GLOVES: _slot += "Hands"; break;
            case MAINHAND: _slot += "Main Hand"; break;
            case OFFHAND: _slot += "Off Hand"; break;
            case LEGS: _slot += "Legs"; break;
            case BOOTS: _slot += "Feet"; break;
            case TRINKET: _slot += "Trinket"; break;
            case RING: _slot += "Finger"; break;
        }

        _slot += "</i>";

        return _slot;
    }

    /// ////////////////////////////////////////////////

    public enum DB
    {
        TEDDY_BEAR,
        STEEL_HELM,
        SOUTHEAST_CHESTPIECE
    }

    static public int GetSlot(int _ID)
    {
        switch (_ID)
        {
            case (int)DB.TEDDY_BEAR: return Item.TRINKET;
            case (int)DB.STEEL_HELM: return Item.HEAD;
            case (int)DB.SOUTHEAST_CHESTPIECE: return Item.CHEST;
            default: return Item.TRINKET;
        }
    }

    static public string GetName(int _ID)
    {
        switch (_ID)
        {
            case (int)DB.TEDDY_BEAR: return "Teddy Bear";
            case (int)DB.STEEL_HELM: return "Steel Helm";
            case (int)DB.SOUTHEAST_CHESTPIECE: return "Southeast's Chestpiece";
            default: return "Wirt's Fourth Leg";
        }
    }

    static public string GetDesc(int _ID)
    {
        switch (_ID)
        {
            case (int)DB.TEDDY_BEAR: return "Atlanta's teddy bear soaked in mysterious magic. She was hugging this bear when strange energy haunted her home. It looks like she linked that horrible experience with bear and she doesn't want it anymore.";
            case (int)DB.STEEL_HELM: return "Just simple steel helm forged by unknown blacksmith. Close contact to magic caused it to gain some healing properties...";
            case (int)DB.SOUTHEAST_CHESTPIECE: return "Mr Southeast's armor. Probably he wore it during some dangerous journeys over the world to fight evil forces so innocent people can sleep calmy... or he just bought it.";
            default: return "You damn bugger!";
        }
    }

    static public Sprite GetSprite(int _ID)
    {
        switch (_ID)
        {
            case (int)DB.TEDDY_BEAR: return Resources.Load<Sprite>("Items/TeddyBear");
            case (int)DB.STEEL_HELM: return Resources.Load<Sprite>("Items/SteelHelm");
            case (int)DB.SOUTHEAST_CHESTPIECE: return Resources.Load<Sprite>("Items/SoutheastChestpiece");
            default: return Resources.Load<Sprite>("Items/TeddyBear");
        }
    }

    private string GetToken(int _ID)
    {
        switch (_ID)
        {
            case (int)DB.TEDDY_BEAR: return "0|0|0|0|1";
            case (int)DB.STEEL_HELM: return "1|0|1|2|0";
            case (int)DB.SOUTHEAST_CHESTPIECE: return "1|1|1|1|1";
            default: return "0|0|0|0|0";
        }
    }
}