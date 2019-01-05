using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Repozytorium Spelli gracza
/// </summary>

public class SpellRepository
{
    private Dictionary<SPELL, SpellInfo> spellRepo = new Dictionary<SPELL, SpellInfo>();
    private GameCore core;

    public SpellRepository(GameCore _core)
    {
        core = _core;
        FillRepository();
    }

    public SpellInfo Get(SPELL ID)
    {
        try
        {
            return spellRepo[ID];
        }
        catch
        {
            Debug.Log(ID);
            return null;
        }
    }

    private void Add(SPELL ID, SpellInfo info)
    {
        spellRepo.Add(ID, info);
    }

    public void FillRepository()
    {
        spellRepo.Add(SPELL.WORD_OF_KINGS_LIGHT, new SpellInfo(SPELL.WORD_OF_KINGS_LIGHT, "Word of Kings: Light", 50, 0.75f, 0, 0, 60, 0, 1, 1, "SpellIcons/Paladin/WordOfKingsLight", 0, 0, 0, HEALTYPE.DIRECT_SINGLE, false, new WordOfKingsLight(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.WORD_OF_KINGS_COURAGE, new SpellInfo(SPELL.WORD_OF_KINGS_COURAGE, "Word of Kings: Courage", 200, 1f, 0, 0, 0, 600, 1, 1, "SpellIcons/Paladin/WordOfKingsCourage", 0, 0, 80, HEALTYPE.DIRECT_MULTI, false, new WordOfKingsCourage(), SPELL_TYPE.AOE_INDICATOR));
        spellRepo.Add(SPELL.WORD_OF_KINGS_FAITH, new SpellInfo(SPELL.WORD_OF_KINGS_FAITH, "Word of Kings: Faith", 0, 0, 300, 2f, 60, 0, 1, 1, "SpellIcons/Paladin/wordOfKingsFaith", 60, 3, 50, HEALTYPE.PERIODIC_SINGLE, false, new WordOfKingsFaith(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.WORD_OF_KINGS_LOYALTY, new SpellInfo(SPELL.WORD_OF_KINGS_LOYALTY, "Word of Kings: Loyalty", 0, 0, 0, 0, 0, 3600, 4, 1, "SpellIcons/Paladin/WordOfKingsLoyalty", 3600, 1, 50, HEALTYPE.OTHER, false, new WordOfKingsLoyalty(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.WORD_OF_KINGS_LOYALTY_HEALING, new SpellInfo(SPELL.WORD_OF_KINGS_LOYALTY_HEALING, "WoKHealing", 0, 0, 0, 0, 0, 0, 0, 0, "SpellIcons/Paladin/Consecrate", 0, 0, 0, HEALTYPE.OTHER, false, new WoKL_Healing(), SPELL_TYPE.HIDDEN));
        spellRepo.Add(SPELL.DIVINE_INTERVENTION, new SpellInfo(SPELL.DIVINE_INTERVENTION, "Divine Intervention", 0, 0, 0, 0, 0, 7200, 1, 1, "SpellIcons/Paladin/DivineIntervention", 900, 1, 0, HEALTYPE.OTHER, false, new DivineIntervention(), SPELL_TYPE.HIDDEN));

        spellRepo.Add(SPELL.SOOTHING_VOID, new SpellInfo(SPELL.SOOTHING_VOID, "Soothing Void", 0, 0, 900, 10f, 0, 0, 1, 1, "SpellIcons/Shadowmancer/SoothingVoid", 180, 10, 20, HEALTYPE.PERIODIC_SINGLE, false, new SoothingVoid(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.SHADOWSURGE, new SpellInfo(SPELL.SHADOWSURGE, "Shadowsurge", 200, 1.5f, 0, 0, 0, 300, 1, 1, "SpellIcons/Shadowmancer/Shadowsurge", 0, 0, 30, HEALTYPE.DIRECT_MULTI, false, new Shadowsurge(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.TWILIGHT_BEAM, new SpellInfo(SPELL.TWILIGHT_BEAM, "Twilight Beam", 0, 0, 300, 2f, 0, 0, 1, 1, "SpellIcons/Shadowmancer/TwilightBeam", 12, 5, 50, HEALTYPE.PERIODIC_SINGLE, true, new TwilightBeam(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.SHADOWSONG, new SpellInfo(SPELL.SHADOWSONG, "Shadowsong", 220, 2.2f, 0, 0, 30, 0, 1, 1, "SpellIcons/Shadowmancer/Shadowsong", 0, 0, 25, HEALTYPE.DIRECT_MULTI, false, new Shadowsong(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.SACRIFICE, new SpellInfo(SPELL.SACRIFICE, "Sacrifice", 200, 1.5f, 0, 0, 0, 0, 1, 1, "SpellIcons/Shadowmancer/Sacrifice", 0, 0, 0, HEALTYPE.DIRECT_MULTI, false, new Sacrifice(), SPELL_TYPE.NORMAL));
        spellRepo.Add(SPELL.SOOTHING_VOID_JUMP, new SpellInfo(SPELL.SOOTHING_VOID_JUMP, "Soothing Void: Jump", 120, 1.2f, 0, 0, 0, 0, 0, 0, "SpellIcons/Shadowmancer/TwilightBeam", 0, 0, 0, HEALTYPE.OTHER, false, new SoothingVoid(), SPELL_TYPE.HIDDEN));
        spellRepo.Add(SPELL.MOONLIGHT, new SpellInfo(SPELL.MOONLIGHT, "Moonlight", 0, 0, 0, 0, 0, 0, 0, 0, "SpellIcons/Shadowmancer/TwilightBeam", 0, 0, 0, HEALTYPE.OTHER, false, new Moonlight(), SPELL_TYPE.HIDDEN));

        spellRepo.Add(SPELL.SCROLL_OF_ALAPHI, new SpellInfo(SPELL.SCROLL_OF_ALAPHI, "Scroll of Alaphi", 0, 0, 0, 0, 0, 0, 0, 0, "Items/CombatItems/ScrollOfAlaphi", 0, 0, 0, HEALTYPE.OTHER, false, new ScrollOfAlaphi(), SPELL_TYPE.HIDDEN));
    }
}

public enum HEALTYPE
{
    DIRECT_SINGLE,
    DIRECT_MULTI,
    PERIODIC_SINGLE,
    PERIODIC_MULTI,
    OTHER
}

public enum HEALSOURCE
{
    DIVNITY_SHIELD,
    FADING_LIGHT,
    WOK_FAITH,
    TWILIGHT_BEAM,
    SHADOWMEND,
    WOK_LOYALTY,
    WOK_COURAGE,
    WOK_LIGHT,
    SOOTHING_VOID,
    MOONLIGHT,
    SACRIFICE,
    SHADOWSONG,
    SHADOWSURGE,
    DREAM,
    EMPATHY,
    ROYALTY,
    GUIDANCE_OF_RAELA,
    SCROLL_OF_ALAPHI
}

public class VALUES
{
    public const float WORD_OF_KINGS_LOYALTY_TRANSFER = 0.2f;
    public const float SPIRIT_BOND_INCREASE = 0.04f;
    public const int AURA_OF_LIGHT_INCREASE = 10;
    public const int CONSECRATION_DUR_INCREASE = 5;
    public const float ROYALTY_PERCENT = 0.34f;
    public const float DIVINITY_PERCENT = 0.2f;
    public const int DIVINITY_CRIT_INCREASE = 3;
    public const float EMPATHY_PERCENT = 0.25f;
    public const float EMPATHY_PERCENT2 = 0.15f;
    public const int GENEROUSITY_REDUCTION = -120;
    public const int GENEROUSITY_VALUE = 60;
    public const float MODESTY_INCREASE = 0.1f;
    public const float BASE_MANA_REGEN = 0.2f;
    public const float VOAK_INCREASE = 0.05f;
    public const float FLASH_OF_FUTURE_PROC1 = 25;
    public const float FLASH_OF_FUTURE_PROC2 = 25;
}

public enum SPELL
{
    // --- PALADIN SPELLS ---
    /// <summary>Leczy cel za X.</summary>
    WORD_OF_KINGS_LIGHT,
    /// <summary>Leczy kilka celow za X.</summary>
    WORD_OF_KINGS_COURAGE,
    /// <summary>Leczy cel za X co sekunde, otrzymany dmg wydluza czas trwania zaklecia o 25% az do 100% max.</summary>
    WORD_OF_KINGS_FAITH,
    /// <summary>Kopiuje X% healingu na cel.</summary>
    WORD_OF_KINGS_LOYALTY,
    /// <summary>Healing czyniony przez WoK:Loyalty.</summary>
    WORD_OF_KINGS_LOYALTY_HEALING,
    /// <summary>Przez 15 sekund twoj healing jest zwiekszony o X%.</summary>
    DIVINE_INTERVENTION,
    // --- SHADOWMANCER SPELLS ---
    /// <summary>Leczy cel za X w ciagu Y sekund. Po uplywie czasu Soothing Void przeskakuja na losowy inny cel.</summary>
    SOOTHING_VOID,
    /// <summary>Leczy wszystkie cele z Soothing Void za X.</summary>
    SHADOWSURGE,
    /// <summary>Leczy cel za X co 0.5 sekundy przez 10 sekund. Tylko jeden cel moze miec Twilight Beam aktywny.</summary>
    TWILIGHT_BEAM,
    /// <summary>Leczy wszystkie cele w rzedzie za X.</summary>
    SHADOWSONG,
    /// <summary>Poswieca polowe many by wyleczyc wszystkich za X.</summary>
    SACRIFICE,
    /// <summary>Przeskok Soothing Void</summary>
    SOOTHING_VOID_JUMP,
    /// <summary>Healing z Moonlight</summary>
    MOONLIGHT,
    /// <summary>Healing Raeli</summary>
    GUIDANCE_OF_RAELA,
    /// <summary>Scroll Leczacy</summary>
    SCROLL_OF_ALAPHI
}