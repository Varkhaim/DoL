using UnityEngine;
using System.Collections;

public enum SPELL_TYPE
{
    NORMAL,
    AOE_INDICATOR,
    HIDDEN
}

public class SpellInfo
{
    public SPELL ID { get; set; }
    public string name { get; set; }
    public int baseValue { get; set; }
    public float coeff { get; set; }
    public int baseValue2 { get; set; }
    public float coeff2 { get; set; }
    public int castTime { get; set; }
    public int cooldown { get; set; }
    public int charges { get; set; }
    public int chargesGain { get; set; }
    public string icon { get; set; }
    public int HoTgap { get; set; }
    public int ticksCount { get; set; }
    public int manaCost { get; set; }
    public HEALTYPE healtype { get; set; }
    public bool channeling { get; set; }
    public SpellEffect effect { get; set; }
    public SPELL_TYPE spellType { get; set; }

    public SpellInfo(SPELL _id, string _name, int _basevalue, float _coeff, int _basevalue2,
        float _coeff2, int _castTime, int _cooldown, int _charges, int _chargesGain,
        string _icon, int _HoTgap, int _ticksCount, int _manaCost, HEALTYPE _healtype, bool _channeling, SpellEffect _effect, SPELL_TYPE _spellType)
    {
        ID = _id;
        name = _name;
        baseValue = _basevalue;
        coeff = _coeff;
        baseValue2 = _basevalue2;
        coeff2 = _coeff2;
        castTime = _castTime;
        cooldown = _cooldown;
        charges = _charges;
        chargesGain = _chargesGain;
        icon = _icon;
        HoTgap = _HoTgap;
        ticksCount = _ticksCount;
        manaCost = _manaCost;
        healtype = _healtype;
        channeling = _channeling;
        effect = _effect;
        spellType = _spellType;
    }

    public SpellInfo()
    {

    }

    public string GetSpellDescription()
    {
        string temp = "";

        SpellInfo spInfo = this;

        switch (ID)
        {
            // PALADIN
            case SPELL.WORD_OF_KINGS_LIGHT: temp += "Heals target for " + GetStringValues(spInfo) + ". " + GetStringDetails(spInfo); break;
            case SPELL.WORD_OF_KINGS_COURAGE: temp += "Heals 5 most injured soldiers for " + GetStringValues(spInfo) + ". " + GetStringDetails(spInfo); break;
            case SPELL.WORD_OF_KINGS_FAITH: temp += "Heals target for "+ GetStringValues(spInfo) + " instantly and then for " + GetStringValues(spInfo, 1) + " over " + GetStringDuration(spInfo) + ". " + GetStringDetails(spInfo); break;
            case SPELL.WORD_OF_KINGS_LOYALTY: temp += "Marks target for " + GetStringDuration(spInfo) + ". " + NumberValue(VALUES.WORD_OF_KINGS_LOYALTY_TRANSFER * 100) + " of all single target healing you do to other soldiers is copied to marked target. " + GetStringDetails(spInfo); break;
            case SPELL.DIVINE_INTERVENTION: temp += "Empowers caster increasing healing done by 100% for " + GetStringDuration(spInfo) + ". " + GetStringDetails(spInfo); break;
            // SHADOWMANCER
            case SPELL.SOOTHING_VOID: temp += "Applies Soothing Void to the target healing them for " + GetStringValues(spInfo,1) + ". Soothing Void last " + GetStringDuration(spInfo) + ". " + GetStringDetails(spInfo); break;
            case SPELL.SHADOWSURGE: temp += "Heals all soldiers with Soothing Void for " + GetStringValues(spInfo) + " and consumes them [Soothing Void]. " + GetStringDetails(spInfo); break;
            case SPELL.TWILIGHT_BEAM: temp += "Heals target for " + GetStringValues(spInfo, 1) + " over " + GetStringDuration(spInfo) + ". " + GetStringDetails(spInfo); break;
            case SPELL.SHADOWSONG: temp += "Heals all soldiers in targeted row for " + GetStringValues(spInfo) + ". " + GetStringDetails(spInfo); break;
            case SPELL.SACRIFICE: temp += "Heals all soldiers for " + GetStringValues(spInfo) + ". " + GetStringDetails(spInfo); break;
        }

        return temp;
    }

    private string NumberValue(float _val)
    {
        return "<color=#ffffff>" + ((int)_val).ToString() + "%</color>";
    }

    private string GetStringValues(SpellInfo spInfo, int which = 0)
    {
        if (which == 0)
            return "<color=#ffffff>" + spInfo.baseValue.ToString() + "</color> <color=#00ff00>(+" + (spInfo.coeff * 100f).ToString() + "% Power)</color>";
        else
            return "<color=#ffffff>" + spInfo.baseValue2.ToString() + "</color> <color=#00ff00>(+" + (spInfo.coeff2 * 100f).ToString() + "% Power)</color>";
    }

    private string GetStringDetails(SpellInfo spInfo)
    {
        string temp = "\n";

        if (spInfo.cooldown > 0)
        {
            float temp2 = spInfo.cooldown / 60f;
            string timetype = " sec";
            if (temp2 >= 60f)
            {
                temp2 /= 60f;
                timetype = " min";
            }
            temp += "Cooldown: <color=#ffffff>" + temp2 + timetype + "</color>. ";
        }
        if (spInfo.charges > 1)
            temp += "Charges: <color=#ffffff>" + spInfo.charges.ToString() + "</color> (<color=#ffffff>+" + spInfo.chargesGain.ToString() + "</color> per cooldown). ";
        if (spInfo.castTime > 0)
            temp += "Cast time: <color=#ffffff>" + (spInfo.castTime / 60f).ToString() + "</color> sec. ";
        if (spInfo.manaCost > 0)
            temp += "Mana cost: <color=#00ffff>" + spInfo.manaCost.ToString() + "</color>. ";

        return temp;
    }

    private string GetStringDuration(SpellInfo spInfo)
    {
        float temp = spInfo.HoTgap * spInfo.ticksCount / 60f;
        string timetype = " sec";
        if (temp >= 60f)
        {
            temp /= 60f;
            timetype = " min";
        }
        return "<color=#ffffff>" + temp.ToString() + timetype + "</color>";
    }
}