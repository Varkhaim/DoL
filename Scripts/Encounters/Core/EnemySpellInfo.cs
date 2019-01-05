using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class EnemySpellInfo
{
    public HOSTILESPELL ID { get; set; }
    public string name { get; set; }
    public int baseValue { get; set; }
    public int baseValue2 { get; set; }
    public int castTime { get; set; }
    public int cooldown { get; set; }
    public string icon { get; set; }
    public int DoTgap { get; set; }
    public int ticksCount { get; set; }
    public DAMAGETYPE damagetype { get; set; }
    public TARGETTYPE targetType { get; set; }
    public int targetsAmount { get; set; }
    public DAMAGESOURCE damageSource { get; set; }
    public DEBUFF debuff { get; set; }
    public string description { get; set; }
    public bool misdirectable { get; set; }
    public MOB summon { get; set; }

    public EnemySpellInfo(HOSTILESPELL _id, string _name, int _basevalue, int _basevalue2,
        int _castTime, int _cooldown, string _icon, int _DoTgap, int _ticksCount,
        DAMAGETYPE _damagetype, int _targetsAmount, TARGETTYPE _targettype, 
        DAMAGESOURCE _damagesource, DEBUFF _debuff, string _description, bool _misdirectable, MOB _summon)
    {
        ID = _id;
        name = _name;
        baseValue = _basevalue;
        baseValue2 = _basevalue2;
        castTime = _castTime;
        cooldown = _cooldown;
        icon = _icon;
        DoTgap = _DoTgap;
        ticksCount = _ticksCount;
        damagetype = _damagetype;
        targetsAmount = _targetsAmount;
        targetType = _targettype;
        damageSource = _damagesource;
        debuff = _debuff;
        description = _description;
        misdirectable = _misdirectable;
        summon = _summon;
    }

    private string NumberValue(float _val)
    {
        return "<color=#ffffff>" + ((int)_val).ToString() + "%</color>";
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

    public string GetDescription()
    {
        string temp = "";

        temp += "<color=#ff8000ff>" + name + "</color>\n\n";
        float secs = ((int)(((float)castTime / 60f)*100))/100f;
        temp += "<color=#00ffffff>Cast time: " + secs + " sec</color>\n\n";
        temp += ParseDescription(description);
        if (misdirectable)
            temp += "\n\n<color=#ff0000ff>Ability redirectable.</color>";

        return temp;
    }

    public float ParseSeconds(int _seconds)
    {
        float seconds = (float)_seconds / 60f;

        seconds = (float)(((int)(seconds * 100))/100f);

        return seconds;
    }

    private string ParseDescription(string _text)
    {
        string parsedText = _text;

        Regex _target = new Regex("\\|target\\|");
        Regex _damage1 = new Regex("\\|damage1\\|");
        Regex _damage2 = new Regex("\\|damage2\\|");
        Regex _time = new Regex("\\|time\\|");
        Regex _gap = new Regex("\\|gap\\|");
        Regex _ticks = new Regex("\\|ticks\\|");

        parsedText = _target.Replace(parsedText, "<color=#00ffff>" + targetsAmount.ToString() + "</color>");
        parsedText = _damage1.Replace(parsedText, "<color=#ff0000>"+baseValue.ToString()+"</color>");
        parsedText = _damage2.Replace(parsedText, "<color=#ff0000>" + baseValue2.ToString() + "</color>");
        parsedText = _time.Replace(parsedText, "<color=#80ff80>" + ParseSeconds(DoTgap * ticksCount).ToString() + "</color>");
        parsedText = _gap.Replace(parsedText, "<color=#80ff80>" + ParseSeconds(DoTgap).ToString() + "</color>");
        parsedText = _ticks.Replace(parsedText, "<color=#80ff80>" + ParseSeconds(ticksCount).ToString() + "</color>");

        return parsedText;
    }
}
