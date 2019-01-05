using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Caster
{
    public TalentTree myTalentTree;
    public GameCore core = GameCore.Core;
    public Aura[] myAura = new Aura[100];

    private int basestatINT = 10; // INTelligence - redukuje cooldown spelli o x%
    private int basestatKNG = 10; // KNowledGe - zwieksza regen many o x%
    private int basestatFCS = 10; // FoCuS - zwieksza szanse na krytyczne uderzenie o x%
    private int basestatPWR = 10; // PoWeR - zwieksza sile leczenia o x%
    private float critChance = 0f;

    public float ManaMax;
    public float ManaCurrent;
    public float manaRegen = 1f;

    public bool isTSR = true; // three seconds rule
    private int TSRtimer = 0;

    public void RegenerateMana(float amount)
    {
        ManaCurrent = Mathf.Min(ManaMax, ManaCurrent + amount);
    }

    public void SpendMana(int realManaCost)
    {
        bool spellException = false;

        if ((realManaCost > 0) && (!spellException))
        {
            TSRtimer = 0;
            if (isTSR)
            {
                isTSR = false;
                core.myManaBar.GetComponent<Image>().sprite = Resources.Load<Sprite>("manabar");
            }
        }

        ManaCurrent -= realManaCost;
    }

    public void RestoreMana(float _amount, int _type = 0)
    {
        switch (_type)
        {
            case 0: ManaCurrent = Mathf.Min(ManaCurrent + _amount, ManaMax); break; // przywrocenie konkretnej wartosci
            case 1: ManaCurrent = Mathf.Min(ManaCurrent + (_amount * 0.01f * ManaMax), ManaMax); break; // przywrocenie % max many
            case 2: ManaCurrent = Mathf.Min(ManaCurrent + (_amount * 0.01f * (ManaMax - ManaCurrent)), ManaMax); break; // przywrocenie % utraconej many
        }
    }

    // --- Metoda ktora regeneruje mane i odswieza pasek many
    public void ManaRegeneration()
    {
        ManaCurrent = Mathf.Min(ManaMax, ManaCurrent + manaRegen);
        core.myManaBar.GetComponent<ManaBarScript>().ManaMax = ManaMax;
        core.myManaBar.GetComponent<ManaBarScript>().ManaCurrent = ManaCurrent;

        if (TSRtimer < 180)
        {
            TSRtimer++;
        }
        else
        {
            isTSR = true;
            core.myManaBar.GetComponent<Image>().sprite = Resources.Load<Sprite>("manabartsr");
        }

        if (isTSR)
        {
            manaRegen = VALUES.BASE_MANA_REGEN;
            if (GameCore.chosenChampion == CHAMPION.PALADIN)
                manaRegen *= 1f + (myAura[(int)AURA.MODESTY].stacks * VALUES.MODESTY_INCREASE);
        }
        else
            manaRegen = VALUES.BASE_MANA_REGEN / 2;
    }

    public float HealingMultiplier()
    {
        float _value = 1f;
        BuffHandler buffSystem = core.buffSystem;
        if (buffSystem.FindBuff(CASTERBUFF.VISIONS_OF_ANCIENT_KINGS) != null)
            _value *= (1.3f + myAura[(int)AURA.VISIONS_OF_ANCIENT_KINGS].stacks*VALUES.VOAK_INCREASE);
        if (buffSystem.FindBuff(CASTERBUFF.DIVINE_INTERVENTION) != null)
            _value *= 1.5f;
        if (myTalentTree.GetTalentByName("Trauma") != null)
        {
            _value *= 1 + (ManaMax - ManaCurrent) / ManaMax * 0.5f;
        }
        if (myTalentTree.GetTalentByName("Book of Prime Shadows") != null)
        {
            CasterBuff myb = buffSystem.FindBuff(CASTERBUFF.BOOK_OF_PRIME_SHADOWS);
            if (myb != null)
            {
                _value *= 1 + myb.stacks * 0.01f;
            }
        }
        return _value;
    }

    public bool AuraActive(AURA which)
    {
        return myAura[(int)which].isActive;
    }
    public int AuraStacks(AURA which)
    {
        return myAura[(int)which].stacks;
    }

    public Caster(TalentTree _tree, Account _acc)
    {
        myTalentTree = _tree;
        ApplyAurasFromTree(_tree);
        CopyStats(_acc);
        RefreshStats();
    }

    private void CopyStats(Account _acc)
    {
        basestatFCS = _acc.statFCS;
        basestatINT = _acc.statINT;
        basestatKNG = _acc.statKNG;
        basestatPWR = _acc.statPWR;
    }

    public void RefreshStats()
    {
        critChance = 200 * basestatFCS / (2 * basestatFCS + 100);
    }

    private void ApplyAurasFromTree(TalentTree _tree)
    {
        switch (_tree.classID)
        {
            case CHAMPION.PALADIN:
                {
                    myAura[(int)AURA.HAND_OF_LIGHT] = new Aura(_tree.GetTalentByName("Hand of Light"));
                    myAura[(int)AURA.IRON_FAITH] = new Aura(_tree.GetTalentByName("Iron Faith"));
                    myAura[(int)AURA.SPIRIT_BOND] = new Aura(_tree.GetTalentByName("Spirit Bond"));
                    myAura[(int)AURA.AURA_OF_LIGHT] = new Aura(_tree.GetTalentByName("Aura of Light"));
                    myAura[(int)AURA.CONSECRATION] = new Aura(_tree.GetTalentByName("Consecration"));
                    myAura[(int)AURA.ROYALTY] = new Aura(_tree.GetTalentByName("Royalty"));
                    myAura[(int)AURA.DIVINITY] = new Aura(_tree.GetTalentByName("Divinity"));
                    myAura[(int)AURA.EMPATHY] = new Aura(_tree.GetTalentByName("Empathy"));
                    myAura[(int)AURA.GENEROUSITY] = new Aura(_tree.GetTalentByName("Generousity"));
                    myAura[(int)AURA.MODESTY] = new Aura(_tree.GetTalentByName("Modesty"));
                    myAura[(int)AURA.VISIONS_OF_ANCIENT_KINGS] = new Aura(_tree.GetTalentByName("Visions of Ancient Kings"));
                    myAura[(int)AURA.GUIDANCE_OF_RAELA] = new Aura(_tree.GetTalentByName("Guidance of Raela"));
                    myAura[(int)AURA.FLASH_OF_FUTURE] = new Aura(_tree.GetTalentByName("Flash of Future"));
                } break;
            case CHAMPION.SHADOWMANCER:
                {

                }
                break;
        }
    }

    public float GetCritChance()
    {
        return critChance;
    }

    public int GetPower()
    {
        return basestatPWR;
    }

    public int GetFocus()
    {
        return basestatFCS;
    }

    public string GetStatsString()
    {
        string st = basestatINT.ToString() + "\n";
        st += basestatKNG.ToString() + "\n";
        st += basestatFCS.ToString() + " (" + GetCritChance().ToString() + "%)\n";
        st += basestatPWR.ToString();
        return st;
    }
}

