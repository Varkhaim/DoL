using UnityEngine;
using System.Collections;
using System.Linq;

public class Talent
{
    public int Points; // ilosc punktow wrzuconych w talent
    public int MaxPoints; // max ilosc punktow
    public bool Unlocked = false; // czy mozna dac w niego punkty
    public string Name; // nazwa talentu
    public TalentTree myParent; // drzewko do ktorego nalezy
    public int tier;
    public int[] linkedWith = new int[3];
    public int linkAmount = 0;

    public Talent(int _tier, string _name, TalentTree _talenttree, params int[] _linkedWith)
    {
        Points = 0;
        MaxPoints = 4;
        Name = _name;
        myParent = _talenttree;
        tier = _tier;
        if (tier == 0)
            Unlocked = true;
        for (int i = 0; i < 3; i++)
            linkedWith[i] = -1;
        for (int i=0; i<_linkedWith.Length; i++)
        {
            linkedWith[i] = _linkedWith[i];
            linkAmount++;
        }
    }

    public void AddPoint()
    {
        if (Points < 1)
        {
            if (myParent.CheckTotalPoints() + 1 <= myParent.PointsCap)
            {
                if (Unlocked)
                {
                    if (Points + 1 <= MaxPoints)
                    {
                        Points++;
                        myParent.CheckTalents();
                    }
                }
            }
        }
        else
        {
            if ((myParent.CheckTotalPoints() >= 13) && (myParent.CheckTotalPoints() + 1 <= myParent.PointsCap))
            {
                if (Points + 1 <= MaxPoints)
                {
                    Points++;
                    myParent.CheckTalents();
                }
            }
        }
    }

    public void CheckTalent()
    {
        Unlocked = true;
        if (tier > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (linkedWith[i] >= 0)
                {
                    if (myParent.talents[linkedWith[i]].Points == 0)
                    {
                        Unlocked = false;
                        break;
                    }
                }
            }
        }
    }

    public string GetTalentString()
    {
        return "<size=20><color=#00ffff>" + Name + "</color></size>\n\n" + GetDescription() + "";
    }

    private string ConvPoints(float multip, bool _percent = false)
    {
        float _value;
        _value = (int)((Mathf.Max(1, Points-1) * multip) * 10);
        _value /= 10f;
        if (_percent)
            return "<color=#ffffffff>" + _value.ToString() + "%</color>";
        else
            return "<color=#ffffffff>" + _value.ToString() + "</color>";
    }

    public string GetDescription()
    {
        switch (Name)
        {
            //--- PALADIN ---\\\

            case "Hand of Light": return "<color=#ffff00>Word of Kings: Light</color> has 30% (+"+ConvPoints(10,true)+ ") chance to make your next <color=#ffff00>Word of Kings: Faith</color> free to cast." + SecondEffect("Chance increased by "+ConvPoints(10,true)+".");
            case "Iron Faith": return "<color=#ffff00>Word of Kings: Faith</color> is refreshed by <color=#ffff00>Word of Kings: Light</color>." + SecondEffect("Targets affected by <color=#ffff00>Word of Kings: Faith</color> take " + ConvPoints(10,true)+" less damage.");

            case "Spirit Bond": return "<color=#ffff00>Word of Kings: Loyalty</color> now also copies healing from <color=#ffff00>Word of Kings: Courage</color> but effect is decreased by 66%." + SecondEffect("Increases amount of copied healing of <color=#ffff00>Word of Kings: Loyalty</color> by " + ConvPoints(3.34f,true) + ".");
            case "Aura of Light": return "<color=#ffff00>Word of Kings: Light</color> has a 10% chance to reset cooldown on <color=#ffff00>Word of Kings: Courage</color>." + SecondEffect("Critical hits of <color=#ffff00>Word of Kings: Light</color> have " + ConvPoints(10,true)+ " higher chance to trigger the effect.");
            case "Consecration": return "<color=#ffff00>Word of Kings: Faith</color> reduces cast time of your next <color=#ffff00>Word of Kings: Faith</color> by 50%." + SecondEffect("Increases duration of <color=#ffff00>Word of Kings: Loyalty</color> by " + ConvPoints(5)+".");

            case "Royalty": return "Your <color=#ffff00>Word of Kings: Loyalty</color> now causes target to absorb 100% of all damage taken for 3 seconds." + SecondEffect("Reduces mana cost of <color=#ffff00>Word of Kings: Loyalty</color> by " + ConvPoints(33,true) + ".");
            case "Divinity": return "Critical hits of <color=#ffff00>Words of Kings: Light</color> and <color=#ffff00>Faith</color> now place an absorb shield on their target equal to 20% of their healing." + SecondEffect("Increases critical strike chance by "+ConvPoints(3,true)+".");
            case "Empathy": return "25% of overhealing done by <color=#ffff00>Word of Kings: Faith</color> is converted into absorb shield." + SecondEffect("<color=#ffff00>Word of Kings: Faith</color> heals " + ConvPoints(15,true)+" faster.");
            case "Generousity": return "<color=#ffff00>Word of Kings: Courage</color> casts reduce cooldown of <color=#ffff00>Divine Intervention</color> by 2 seconds." + SecondEffect("Reduces cooldown of <color=#ffff00>Word of Kings: Courage</color> by " + ConvPoints(1)+".");
            case "Modesty": return "<color=#ffff00>Word of Kings: Light</color> doesn't interupt <color=#0080ff>Tranquility</color> state." + SecondEffect("Increases effect of <color=#0080ff>Tranquility</color> by "+ConvPoints(10, true)+".");

            case "Visions of Ancient Kings": return "<color=#ffff00>Word of Kings: Light</color>'s critical hits increase your healing by 30% (+" + ConvPoints(5,true)+") for 3 seconds. "+SecondEffect("Effected increased by "+ConvPoints(5,true)+".");
            case "Guidance of Raela": return "During <color=#ffff00>Divine Intervention</color> all overhealing done is copied to most injured soldier as healing." + SecondEffect("Duration of <color=#ffff00>Divine Intervention</color> is increased by " + ConvPoints(1)+" sec.");
            case "Flash of Future": return "<color=#ffff00>Word of Kings: Light</color> has " + VALUES.FLASH_OF_FUTURE_PROC1+ "% chance to make your next <color=#ffff00>Word of Kings: Faith</color> an instant cast.\n<color=#ffff00>Word of Kings: Faith</color> has " + VALUES.FLASH_OF_FUTURE_PROC2 + "% chance to make your next <color=#ffff00>Word of Kings: Light</color> 100% stronger." + SecondEffect("Chances increased by "+ConvPoints(1, true)+".");

            //--- SHADOWMANCER ---\\

            case "Dusk": return "Soothing Void last " + ConvPoints(1f) + " seconds less.";
            case "Dawn": return "Twilight Beam heals " + ConvPoints(10f, true) + " faster.";

            case "Moonlight": return "Twilight Beam now also heals nearby allies for " + ConvPoints(5, true) + " of healing done.";
            case "Shadowforce": return "Shadowsurge has " + ConvPoints(20, true) + " chance to not consume Soothing Void.";
            case "Shadowmend": return "Shadowsong’s critical strikes heal for additional " + ConvPoints(20, true) + " of initial value over 6 seconds.";

            case "Fading Light": return "Soothing Void now also heal for " + ConvPoints(20, true) + " of initial value when they expire.";
            case "Dream": return "Twilight Beam now consumes Soothing Void to shield target for " + ConvPoints(75f, true) + " of Twilight Beam's total healing.";
            case "Silence": return "Shadowsong has a " + ConvPoints(50, true) + " chance when cast on target affected by Soothing Void to spread them on every target.";
            case "Peace": return "Shadowsurge's critical strike chance is increased by 50% on targets below " + ConvPoints(25, true) + " health.";
            case "Awakening": return "Every time you cast Twilight Beam the cooldown of your Shadowsong is reduced by " + ConvPoints(0.5f) + " second.";

            case "Ascetic": return "Shadowsurge reduces Sacrifice's cooldown by 1 second per target healed.";
            case "Trauma": return "Your healing is increased by 0.5% for every 1% missing mana.";
            case "Book of Prime Shadows": return "Everytime you cast Soothing Void your healing and mana costs are increased by 1%.";
            default: return "";
        }
    }

   /* public void TakePoint()
    {
        if (Unlocked)
        {
            if (Points > 0)
            {
                Points--;
                myParent.CheckTalents();
            }
        }
    }*/
    private string SecondEffect(string _eff)
    {
        if (Points <= 1)
            return "\n\n<color=#808080>" + _eff + "</color>";
        else
            return "\n\n"+_eff;
    }

    static public Sprite GetSpriteByTalent(Talent _mytalent)
    {
        switch (_mytalent.Name)
        {
            case "Hand of Light": return Resources.Load<Sprite>("TalentIcons/HandOfLight");
            case "Iron Faith": return Resources.Load<Sprite>("TalentIcons/IronFaith");

            case "Spirit Bond": return Resources.Load<Sprite>("TalentIcons/SpiritBond");
            case "Aura of Light": return Resources.Load<Sprite>("TalentIcons/AuraOfLight");
            case "Consecration": return Resources.Load<Sprite>("TalentIcons/Consecration");

            case "Royalty": return Resources.Load<Sprite>("TalentIcons/Royalty");
            case "Divinity": return Resources.Load<Sprite>("TalentIcons/Divinity");
            case "Empathy": return Resources.Load<Sprite>("TalentIcons/Empathy");
            case "Generousity": return Resources.Load<Sprite>("TalentIcons/Generousity");
            case "Modesty": return Resources.Load<Sprite>("TalentIcons/Modesty");

            case "Visions of Ancient Kings": return Resources.Load<Sprite>("TalentIcons/VisionsOfAncientKings");
            case "Guidance of Raela": return Resources.Load<Sprite>("TalentIcons/GuidanceOfRaela");
            case "Flash of Future": return Resources.Load<Sprite>("TalentIcons/HopeForFuture");

            //  Shadowmancer

            case "Dusk": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Dusk");
            case "Dawn": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Dawn");

            case "Moonlight": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Moonlight");
            case "Shadowforce": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Shadowforce");
            case "Shadowmend": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Shadowmend");

            case "Fading Light": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/FadingLight");
            case "Dream": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Dream");
            case "Silence": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Silence");
            case "Peace": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Peace");
            case "Awakening": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Awakening");

            case "Ascetic": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Ascetic");
            case "Trauma": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/Trauma");
            case "Book of Prime Shadows": return Resources.Load<Sprite>("TalentIcons/Shadowmancer/BookOfPrimeShadows");

            default: return Resources.Load<Sprite>("TalentIcons/HandOfLight");
        }
    }
}

