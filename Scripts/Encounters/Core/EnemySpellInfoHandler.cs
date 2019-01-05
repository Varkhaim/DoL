using UnityEngine;
using System.Collections;

public class EnemySpellInfoHandler
{
    public EnemySpellInfo[] espellInfo = new EnemySpellInfo[100];
    int espellsAmount = 0;

    private const float CD_MOD = 1;
    private const float CAST_MOD = 1;
    private const float MULTIP_MOD = 0.25f;
    private const float GAP_MOD = 1;

    public EnemySpellInfoHandler()
    {
        AddESpellInfo(HOSTILESPELL.FIREBOLT, "Firebolt",
            200, 100, 120, 180,
            "EnemySpells/Firebolt",
            90, 5, DAMAGETYPE.DIRECT, 3, TARGETTYPE.RANDOM,
            DAMAGESOURCE.FIREBOLT, DEBUFF.FIREBOLT,
            "Deals damage to 3 random targets.", false);
        AddESpellInfo(HOSTILESPELL.RAIN_OF_FIRE, "Rain of Fire",
            0, 300, 180, 600,
            "EnemySpells/RainOfFire",
            90, 6, DAMAGETYPE.PERIODIC, 6, TARGETTYPE.RANDOM,
            DAMAGESOURCE.RAIN_OF_FIRE, DEBUFF.RAIN_OF_FIRE,
            "Sends rain of fire on 6 targets dealing damage over 3 seconds.", false);
        AddESpellInfo(HOSTILESPELL.CRY, "Cry",
            0, 150, 1, 1,
            "EnemySpells/Cry",
            120, 10, DAMAGETYPE.PERIODIC, 16, TARGETTYPE.EVERYONE,
            DAMAGESOURCE.CRY, DEBUFF.CRY,
            "Atlanta's cry deals damage to all enemies until someone bring her peace.", false);
        AddESpellInfo(HOSTILESPELL.SCREAM, "Scream",
            100, 0, 120, 1200,
            "EnemySpells/Scream",
            0, 0, DAMAGETYPE.DIRECT, 16, TARGETTYPE.EVERYONE,
            DAMAGESOURCE.SCREAM, DEBUFF.NONE,
            "Deals damage to all enemies. Damage increased to injured targets.", false);
        AddESpellInfo(HOSTILESPELL.VOID_DISCHARGE, "Void Discharge",
            100, 0, 60, 400,
            "EnemySpells/VoidDischarge",
            0, 0, DAMAGETYPE.DIRECT, 2, TARGETTYPE.RANDOM,
            DAMAGESOURCE.VOID_DISCHARGE, DEBUFF.NONE,
            "Deals damage to 2 random targets.", false);
        AddESpellInfo(HOSTILESPELL.TANTRUM, "Tantrum",
            0, 3000, 240, 600,
            "EnemySpells/Tantrum",
            120, 30, DAMAGETYPE.PERIODIC, 1, TARGETTYPE.RANDOM,
            DAMAGESOURCE.TANTRUM, DEBUFF.TANTRUM_DOT,
            "Deals high damage to random target over 30 seconds.", false);
        AddESpellInfo(HOSTILESPELL.CURSED_SWIPE, "Cursed Swipe",
            100, 0, 90, 500,
            "EnemySpells/CursedSwipe",
            30, 1, DAMAGETYPE.DIRECT, 6, TARGETTYPE.RANDOM,
            DAMAGESOURCE.CURSED_SWIPE, DEBUFF.CURSED_SWIPE,
            "Constantly slashes random |target| targets dealing |damage1| damage and reducing all healing by 50% for |time| seconds.", true);
        AddESpellInfo(HOSTILESPELL.BRUTAL_BITE, "Brutal Bite",
            200, 10, 60, 600,
            "EnemySpells/BrutalBite",
            90, 6, DAMAGETYPE.DIRECT, 1, TARGETTYPE.RANDOM,
            DAMAGESOURCE.BRUTAL_BITE, DEBUFF.BRUTAL_BITE,
            "Brutally bites random target to deal |damage1| damage and then |damage2|% of target's missing health every |gap| seconds until healed above 90% health.", true);
        AddESpellInfo(HOSTILESPELL.ROAR_OF_TERROR, "Roar of Terror",
            300, 0, 150, 1200,
            "EnemySpells/RoarOfTerror",
            0, 0, DAMAGETYPE.DIRECT, 16, TARGETTYPE.EVERYONE,
            DAMAGESOURCE.ROAR_OF_TERROR, DEBUFF.NONE,
            "Shadowbeast focuses for a while to release massive roar dealing |damage1| damage to all enemies. Afterwards, it has to rest for a moment.", false);
        AddESpellInfo(HOSTILESPELL.SHADOWFANG, "Shadowfang",
            0, 33, 60, 600,
            "EnemySpells/Shadowfang",
            120, 10, DAMAGETYPE.PERIODIC, 4, TARGETTYPE.FIRSTCOLUMN,
            DAMAGESOURCE.SHADOWFANG, DEBUFF.SHADOWFANG,
            "Bites frontal soldiers with shadowforged fangs which deals damage equal to |damage2|% of target's max health over |time| seconds. That effect stacks. Effect weakens with targets amount.", false);
        AddESpellInfo(HOSTILESPELL.SOUL_TOMB, "Soul Tomb",
            0, 200, 60, 800,
            "EnemySpells/SoulTomb",
            120, 8, DAMAGETYPE.PERIODIC, 5, TARGETTYPE.RANDOM,
            DAMAGESOURCE.SOUL_TOMB, DEBUFF.SOUL_TOMB,
            "Throws soul tombs on |target| random targets. Soul Tomb deals minor damage every second until target fall under 50% health. Sool copies 10% of healing done to target to himself.", false);
        AddESpellInfo(HOSTILESPELL.MIND_BOMB, "Mind Bomb",
            0, 300, 60, 600,
            "EnemySpells/MindBomb",
            600, 1, DAMAGETYPE.DIRECT, 2, TARGETTYPE.RANDOM,
            DAMAGESOURCE.MIND_BOMB, DEBUFF.MIND_BOMB,
            "Throw 2 Mind Bombs on enemies. Mind Bomb explodes after 5 seconds dealing high damage to its target and minor damage to 8 most injured soldiers. Mind Bomb disappears when its target is healed above 95% health.", false);
        AddESpellInfo(HOSTILESPELL.SHADOWBOLT, "Shadowbolt",
            175, 0, 90, 800,
            "EnemySpells/Shadowbolt",
            0, 0, DAMAGETYPE.DIRECT, 5, TARGETTYPE.RANDOM,
            DAMAGESOURCE.SHADOWBOLT, DEBUFF.NONE,
            "Deals medium damage to 5 random targets.", true);
        AddESpellInfo(HOSTILESPELL.SCRATCH, "Scratch",
            200, 0, 90, 300,
            "EnemySpells/Scratch",
            0, 0, DAMAGETYPE.DIRECT, 3, TARGETTYPE.RANDOM,
            DAMAGESOURCE.SCRATCH, DEBUFF.NONE,
            "Deals medium damage to 3 random targets.", false);
        AddESpellInfo(HOSTILESPELL.VOIDFLAME, "Voidflame",
            0, 100, 90, 800,
            "EnemySpells/Voidflame",
            120, 5, DAMAGETYPE.PERIODIC, 1, TARGETTYPE.RANDOM,
            DAMAGESOURCE.VOIDFLAME, DEBUFF.VOIDFLAME,
            "Engulfs random target in voidflame dealing damage every second. Every tick increases power of voidflame and every healing received weakens the flame.", false);
        AddESpellInfo(HOSTILESPELL.DARK_ROSE, "Dark Rose",
            0, 350, 90, 800,
            "EnemySpells/DarkRose",
            60, 5, DAMAGETYPE.DIRECT, 1, TARGETTYPE.RANDOM,
            DAMAGESOURCE.DARK_ROSE, DEBUFF.DARK_ROSE,
            "Pins Dark Rose to random target dealing damage every second when target is under 60% health.", false);
        AddESpellInfo(HOSTILESPELL.BLOODBOIL, "Bloodboil",
            0, 0, 90, 1200,
            "EnemySpells/Bloodboil",
            0, 0, DAMAGETYPE.DIRECT, 16, TARGETTYPE.EVERYONE,
            DAMAGESOURCE.BLOODBOIL, DEBUFF.NONE,
            "Boils blood in veins of all enemies causing them to take damage equal to 33% of their current health.", false);
        AddESpellInfo(HOSTILESPELL.SUMMON_SPAWNLING, "Summon Spawnling",
            0, 0, 150, 1200,
            "EnemySpells/Bloodboil",
            0, 0, DAMAGETYPE.SUMMON, 1, TARGETTYPE.SUMMON,
            DAMAGESOURCE.SUMMON, DEBUFF.NONE,
            "Summons Spawnling.", false, MOB.SHADESPAWNLING);
    }

    private void AddESpellInfo(HOSTILESPELL _id, string _name, int _basevalue, int _basevalue2, int _castTime, int _cooldown, string _icon, int _DoTgap, int _tickCount, DAMAGETYPE _damagetype, int _targetsAmount, TARGETTYPE _targettype, DAMAGESOURCE _damagesource, DEBUFF _debuff, string _desc, bool _misdirectable, MOB _summon = MOB.NUL)
    {
        espellInfo[(int)_id] = new EnemySpellInfo(
            _id, _name, (int)(_basevalue * MULTIP_MOD), (int)(_basevalue2 * MULTIP_MOD),
            (int)(_castTime * CAST_MOD), (int)(_cooldown * CD_MOD), _icon,
            (int)(_DoTgap * GAP_MOD), _tickCount, _damagetype, _targetsAmount, _targettype, _damagesource, _debuff, _desc, _misdirectable, _summon);
    }

    public EnemySpellInfo GetESpellInfo(HOSTILESPELL _ID)
    {
        return espellInfo[(int)_ID];
    }
}
