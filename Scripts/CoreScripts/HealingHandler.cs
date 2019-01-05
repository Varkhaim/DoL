using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealingHandler
{
    private Absorb[] absorb = new Absorb[20]; // tablica absorbow
    private int absAmount = 0;
    public float totalAbsorb = 0;

    public Soldier soldier;
    private GameCore core;

    public HealingHandler(Soldier _soldier, GameCore _core)
    {
        soldier = _soldier;
        core = _core;
    }

    private void ExecuteDebuffsTriggeredByHealing(Healing myHeal)
    {
        Debuff mindbomb = soldier.effectSystem.FindDebuff(DEBUFF.MIND_BOMB);
        if (mindbomb != null)
        {
            if (soldier.health / soldier.maxHealth > 0.95f)
                mindbomb.Remove();
        }

        Debuff voidflame = soldier.effectSystem.FindDebuff(DEBUFF.VOIDFLAME);
        if (voidflame != null)
        {
            voidflame.multiplier = Mathf.Max(0.2f, voidflame.multiplier - 0.6f);
            ((Voidflame)voidflame).RefreshIcon();
        }

        //if (FindDebuff(DEBUFF.SOUL_TOMB) != null)
        //    GameCore.Core.myEnemy.Heal((int)(myHeal.value / 10f));
    }

    public int Shield(int _value, HEALSOURCE _source)
    {
        absorb[absAmount++] = new Absorb(_value, _source);
        totalAbsorb += _value;
        soldier.frame.GetComponent<scrUnitPanel>().RefreshHealthInfo();
        return _value;
    }

    private Healing ApplyHealingModifiersBySource(Healing baseHealing, Caster caster, HEALSOURCE source)
    {
        Healing newHealing = baseHealing;

        return newHealing;
    }


    private void CriticalHealTrigger(Healing myHeal)
    {
        if ((myHeal.healtype != HEALTYPE.PERIODIC_SINGLE) && (myHeal.healtype != HEALTYPE.PERIODIC_MULTI))
        {
            if (myHeal.isCrit)
            {
                core.CriticalHealOccured();
            }
        }
    }

    private Healing ApplyHealingModifiersByBuff(HEALSOURCE source, Healing baseHeal, Caster _caster)
    {
        Healing newHealing = baseHeal;
        if (source == HEALSOURCE.WOK_LIGHT)
        {
            Buff _myb = soldier.effectSystem.FindBuff(BUFF.FAITH);
            if ((_myb != null))
            {
                SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.WORD_OF_KINGS_FAITH);
                int _dur = spellInfo.ticksCount * spellInfo.HoTgap;
                int _gap = spellInfo.HoTgap;
                GameCore.Core.paladinSparkHandler.AddSparks(1);
                soldier.effectSystem.BuffMe(BUFF.WORD_OF_KINGS_FAITH, _dur, _caster, spellInfo, _gap);
            }

            CasterBuff _casterBuff = GameCore.Core.buffSystem.FindBuff(CASTERBUFF.DIVINE_INTERVENTION);
            if (baseHeal.isCrit)
            {
                if (_casterBuff != null)
                    baseHeal.value = (int)((float)baseHeal.value * (1f + (float)_caster.GetFocus() / 100f));
            }
        }



        return newHealing;
    }

    private int ApplyOverhealingModifiers(int overheal_value, Caster _caster, HEALSOURCE source)
    {
        return overheal_value;
    }

    private void WoKLoyaltyBeaconHealing(Healing myHeal, HEALSOURCE source, HEALTYPE healtype)
    {
        if ((GameCore.chosenChampion == CHAMPION.PALADIN) && (source != HEALSOURCE.WOK_LOYALTY) && (source != HEALSOURCE.GUIDANCE_OF_RAELA))
        {
            float perc = VALUES.WORD_OF_KINGS_LOYALTY_TRANSFER + core.myCaster.myAura[(int)AURA.SPIRIT_BOND].stacks * VALUES.SPIRIT_BOND_INCREASE;
            if ((core.myCaster.AuraActive(AURA.SPIRIT_BOND)) && ((healtype == HEALTYPE.DIRECT_MULTI) || (healtype == HEALTYPE.PERIODIC_MULTI)))
                perc *= 0.3f;
            core.BeaconHeal((int)(myHeal.value * perc), 4, soldier);
        }
    }

    private void UpdateHealthBar()
    {
        soldier.frame.GetComponent<scrUnitPanel>().RefreshHealthInfo();
    }

    public Healing Heal(Caster _caster, SpellInfo info, HEALSOURCE source, HEALTYPE healtype, int whichValue = 0)
    {
        try
        {
            return ApplyHeal(_caster, info.baseValue + (int)(info.coeff * _caster.GetPower()), _caster.GetCritChance(), source, healtype);
        }
        catch
        {
            Debug.Log("Source: " + source);
            return null;
        }
    }

    public Healing Heal(Caster _caster, int val, float crit, HEALSOURCE source, HEALTYPE healtype)
    {
        return ApplyHeal(_caster, val, crit, source, healtype);
    }

    private Healing ApplyHeal(Caster _caster, int val, float crit, HEALSOURCE source, HEALTYPE healtype)
    {
        if (!soldier.isDead)
        {
            Healing myHeal = new Healing(0, false, healtype);

            myHeal = CalculateHealing(val, crit, healtype);

            myHeal.value = (int)(myHeal.value * _caster.HealingMultiplier());
            myHeal.value = (int)(myHeal.value * soldier.healingTakenBoost);

            myHeal = ApplyHealingModifiersBySource(myHeal, _caster, source);

            WoKLoyaltyBeaconHealing(myHeal, source, healtype);

            myHeal = ApplyHealingModifiersByBuff(source, myHeal, _caster);

            float heal_value = Mathf.Min(myHeal.value, soldier.maxHealth - soldier.health);
            heal_value *= Random.Range(95f, 105f) / 100f;
            int overheal_value = 0;
            overheal_value = Mathf.Max(0, (int)((myHeal.value + soldier.health) - soldier.maxHealth));

            overheal_value = ApplyOverhealingModifiers(overheal_value, _caster, source);
            myHeal.overhealing = overheal_value;

            soldier.health = soldier.health + heal_value;
            core.recount.AddEntry(source, (int)heal_value, overheal_value);

            ExecuteDebuffsTriggeredByHealing(myHeal);

            CriticalHealTrigger(myHeal);

            UpdateHealthBar();

            LogHealing((int)heal_value, myHeal.isCrit);
            return myHeal;
        }
        return null;
    }

    private Healing CalculateHealing(int value, float critchance, HEALTYPE healtype)
    {
        bool isCrit = false;

        if (critchance > 0)
        {
            if (Random.Range(0, 100) < critchance)
            {
                value *= 2;
                isCrit = true;
            }
        }

        Healing myHeal = new Healing(value, isCrit, healtype);
        return myHeal;
    }

    public void LogHealing(int value, bool isCrit, int type = 0)
    {
        if (value > 10)
        {
            float posx;
            Color mycolor;

            switch (type)
            {
                case 0:
                    {
                        posx = 0f;
                        mycolor = Color.green;  // healing
                    }
                    break;
                case 1:
                    {
                        posx = 0.2f;
                        mycolor = Color.white; // absorb
                    }
                    break;
                default:
                    {
                        posx = 0.1f;
                        mycolor = Color.green;  // healing
                    }
                    break;
            }

            if (soldier.frame != null)
            {
                GameObject myLog = Object.Instantiate(Resources.Load("CombatText"), soldier.frame.transform.position + new Vector3(posx, 0, -2), soldier.frame.transform.rotation) as GameObject;
                myLog.GetComponent<Text>().text = value.ToString();
                myLog.GetComponent<Text>().color = mycolor;

                myLog.GetComponent<CombatLogScript>().isCrit = isCrit;
            }

        }
    }
}


