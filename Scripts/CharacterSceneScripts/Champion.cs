using UnityEngine;
using System.Collections;

public enum CHAMPION
{
    NOTHING,
    PALADIN,
    SHADOWMANCER
}

public class Champion
{


    public Champion()
    {

    }

    public static string GetName(CHAMPION classID)
    {
        switch (classID)
        {
            case CHAMPION.PALADIN: return "Roderick <color=#ffff88>the Paladin</color>";
            case CHAMPION.SHADOWMANCER: return "Yamiran <color=#8800ff>the Shadowmancer</color>";
            default: return "NULL";
        }
    }

    public static Sprite GetPortrait(CHAMPION classID)
    {
        switch (classID)
        {
            case CHAMPION.PALADIN: return Resources.Load<Sprite>("SplashArts/paladin"); 
            case CHAMPION.SHADOWMANCER: return Resources.Load<Sprite>("SplashArts/shadowmancer");
            default: return Resources.Load<Sprite>("");
        }
    }

    public static Sprite GetSpellIcon(CHAMPION classID, int num)
    {
        if (classID == CHAMPION.PALADIN)
        {
            switch (num)
            {
                case 0: return Resources.Load<Sprite>("SpellIcons/Paladin/WordOfKingsLight");
                case 1: return Resources.Load<Sprite>("SpellIcons/Paladin/WordOfKingsCourage");
                case 2: return Resources.Load<Sprite>("SpellIcons/Paladin/WordOfKingsFaith");
                case 3: return Resources.Load<Sprite>("SpellIcons/Paladin/WordOfKingsLoyalty");
                case 4: return Resources.Load<Sprite>("SpellIcons/Paladin/DivineIntervention");
            }
        }
        else
        if (classID == CHAMPION.SHADOWMANCER)
        {
            switch (num)
            {
                case 0: return Resources.Load<Sprite>("SpellIcons/Shadowmancer/SoothingVoid");
                case 1: return Resources.Load<Sprite>("SpellIcons/Shadowmancer/Shadowsurge");
                case 2: return Resources.Load<Sprite>("SpellIcons/Shadowmancer/TwilightBeam");
                case 3: return Resources.Load<Sprite>("SpellIcons/Shadowmancer/Shadowsong");
                case 4: return Resources.Load<Sprite>("SpellIcons/Shadowmancer/Sacrifice");
            }
        }
        return null;
    }

    public static string GetSpellName(CHAMPION classID, int num)
    {
        if (classID == CHAMPION.PALADIN)
        {
            switch (num)
            {
                case 0: return "Word of Kings: Light";
                case 1: return "Word of Kings: Courage";
                case 2: return "Word of Kings: Faith";
                case 3: return "Word of Kings: Loyalty";
                case 4: return "Divine Intervention";
            }
        }
        else
        if (classID == CHAMPION.SHADOWMANCER)
        {
            switch (num)
            {
                case 0: return "Soothing Void";
                case 1: return "Shadowsurge";
                case 2: return "Twilight Beam";
                case 3: return "Shadowsong";
                case 4: return "Sacrifice";
            }
        }
        return "";
    }

    public static string GetDescription(CHAMPION classID)
    {
        switch (classID)
        {
            case CHAMPION.PALADIN: return "<color=#ff00ffff>Paladin Roderick</color>\nWhen the orcs first arrived in Azeroth and launched their initial attack on Stormwind Keep, Lothar aggressively advocated taking the battle to them."+
                    "King Adamant Wrynn III shared Lothar's view and therefore pledged to rid his beloved land of the orcs. He died, however, before that pledge could ever be fulfilled, and was succeeded by his son, Llane Wrynn I."+
                    "The new king continued his father's work, and the battles against the orcs raged on. During this time, the invaders were held back to the Swamp of Sorrows. "+
                    "\n\nAt one point during the war, the Tome of Divinity, a book of great value to the Clerics of Northshire, was stolen by a rogue band of ogres, led by the ogre lord, Turok."+
                    "Lothar led an expedition into the ogres' hideout, the Deadmines in Westfall, but were completely overrun and held captive to be killed slowly. "+
                    "Lothar remained imprisoned within the caves for twenty months before he and his few surviving men were saved by Azerothian troops. He retrieved the Tome of Divinity and returned to Stormwind, safeguarding the book at Northshire Abbey. "+
                    "Reintroduced to the war, Lothar continued leading the forces of Stormwind. ";
            case CHAMPION.SHADOWMANCER: return "<color=#5555ffff>Shadowmancer Yamiran</color>\nis really long sentence and I try to make it longer so i can check whether script is working or not.";
            default: return "";
        }
    }

    public static string GetSpellDescription(CHAMPION classID, int num)
    {
        SpellRepository spellRepo = GameCore.Core.spellRepository;
        if (classID == CHAMPION.PALADIN)
        {
            switch (num)
            {
                case 0: return spellRepo.Get(SPELL.WORD_OF_KINGS_LIGHT).GetSpellDescription();
                case 1: return spellRepo.Get(SPELL.WORD_OF_KINGS_COURAGE).GetSpellDescription();
                case 2: return spellRepo.Get(SPELL.WORD_OF_KINGS_FAITH).GetSpellDescription();
                case 3: return spellRepo.Get(SPELL.WORD_OF_KINGS_LOYALTY).GetSpellDescription();
                case 4: return spellRepo.Get(SPELL.DIVINE_INTERVENTION).GetSpellDescription();
            }
        }
        else
        if (classID == CHAMPION.SHADOWMANCER)
        {
            switch (num)
            {
                case 0: return spellRepo.Get(SPELL.SOOTHING_VOID).GetSpellDescription();
                case 1: return spellRepo.Get(SPELL.SHADOWSURGE).GetSpellDescription();
                case 2: return spellRepo.Get(SPELL.TWILIGHT_BEAM).GetSpellDescription();
                case 3: return spellRepo.Get(SPELL.SHADOWSONG).GetSpellDescription();
                case 4: return spellRepo.Get(SPELL.SACRIFICE).GetSpellDescription();
            }
        }
        return "";
    }
}