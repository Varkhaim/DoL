using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoldierEffectSystem
{
    public Soldier soldier;

    public Buff[] Buffs = new Buff[10];
    public Debuff[] Debuffs = new Debuff[10];
    public int myID;

    public SoldierEffectSystem(Soldier _soldier)
    {
        soldier = _soldier;
    }

    //******************************************** START BUFFY ******************************************** \\

    private int BuffsAmount = 0;

    public void BuffMe(BUFF bufftype, int buffdur, Caster _caster, SpellInfo spellInfo, int _gap, int val=0)
    {
        Buff myBuff = FindBuff(bufftype);

        if (myBuff == null)
        {
            Buffs[BuffsAmount] = GetBuffEffect(bufftype, buffdur, _caster, spellInfo, _gap, val);
            Buffs[BuffsAmount].icon = Object.Instantiate(Resources.Load("BuffIcon"), soldier.frame.transform.position + new Vector3(1.5f, -0.6f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            Buffs[BuffsAmount].SetIcon();
            Buffs[BuffsAmount].icon.transform.SetParent(GameObject.Find("Canvas").transform);
            Buffs[BuffsAmount].icon.transform.localScale = new Vector3(1, 1, 1);

            Buffs[BuffsAmount].icon.GetComponent<BuffScript>().myParent = soldier.frame;
            Buffs[BuffsAmount].icon.GetComponent<BuffScript>().myid = BuffsAmount;

            BuffsAmount++;

            if (bufftype == BUFF.WORD_OF_KINGS_LOYALTY)
            {
                soldier.core.AddBeaconedTarget(soldier);
            }

            return;
        }

        myBuff.Refresh(0);
    }

    private Buff GetBuffEffect(BUFF bufftype, int buffdur, Caster _caster, SpellInfo spellInfo, int _gap, int val = 0)
    {
        switch (bufftype)
        {
            case BUFF.WORD_OF_KINGS_FAITH: return new WordOfKingsFaith_Buff(bufftype, buffdur, soldier, _caster, spellInfo, _gap, val);
            case BUFF.FAITH: return new Faith_Buff(bufftype, buffdur, soldier, _caster, spellInfo, _gap, val);
            case BUFF.WORD_OF_KINGS_LOYALTY: return new WordOfKingsLoyalty_Buff(bufftype, buffdur, soldier, _caster, spellInfo, _gap, val);
            default: { Debug.Log(bufftype); return null; }
        }
        
    }

    public static Sprite GetBuffResource(BUFF _buff)
    {
        switch (_buff)
        {
            case 0: return Resources.Load<Sprite>("BuffsIcons/");
            case BUFF.WORD_OF_KINGS_FAITH: return Resources.Load<Sprite>("BuffsIcons/WordOfKingsFaith");
            case BUFF.WORD_OF_KINGS_LOYALTY: return Resources.Load<Sprite>("BuffsIcons/WordOfKingsLoyalty");
            case BUFF.ROYALTY: return Resources.Load<Sprite>("BuffsIcons/Royalty");
            case BUFF.SHADOWMEND: return Resources.Load<Sprite>("BuffsIcons/Shadowmend");
            case BUFF.FLASH_OF_FUTURE: return Resources.Load<Sprite>("BuffsIcons/FlashofFuture");
            case BUFF.SOOTHING_VOID: return Resources.Load<Sprite>("BuffsIcons/SoothingVoid");
            case BUFF.TWILIGHT_BEAM: return Resources.Load<Sprite>("BuffsIcons/TwilightBeam");
            default: return Resources.Load<Sprite>("effect");
        }
    }

    private void RepositionIcons()
    {
        for (int i = 0; i < BuffsAmount; i++)
        {
            if (Buffs[i].icon != null)
            {
                float basex = -0.45f;
                Buffs[i].icon.transform.position = soldier.frame.transform.position + new Vector3(basex, 0.4f - 0.3f * i, -0.5f);
            }
        }
        for (int i = 0; i < DebuffsAmount; i++)
        {
            if (Debuffs[i].icon != null)
            {
                float basex = 0.45f;
                Debuffs[i].icon.transform.position = soldier.frame.transform.position + new Vector3(basex, -0.1f - 0.3f * i, -0.5f);
            }
        }
    }

    public void SortBuffs()
    {
        for (int i = 0; i < BuffsAmount; i++)
        {
            if (Buffs[i] != null)
            {
                if (Buffs[i].duration <= 0)
                {
                    Buffs[i].Remove();
                    Buffs[i] = null;
                }
            }

            if (Buffs[i] == null)
            {
                if (i < BuffsAmount - 1)
                {
                    Buffs[i] = Buffs[i + 1];
                    Buffs[i + 1] = null;
                }
                else
                {
                    BuffsAmount--;
                    soldier.UpdateDamageDoneBoost();
                    soldier.UpdateDamageTakenBoost();
                }
            }
        }
        RepositionIcons();
    }

    public void ActivateBuffs()
    {
        if (BuffsAmount > 0)
        {
            for (int i = 0; i < BuffsAmount; i++)
            {
                ExecuteBuff(i);
            }
        }
    }

    public Buff FindBuff(BUFF bufftype)
    {
        if (BuffsAmount <= 0)
        {
            return null;
        }

        for (int i = 0; i < BuffsAmount; i++)
        {
            if (Buffs[i].ID == bufftype)
            {
                return Buffs[i];
            }
        }

        return null;
    }

    public void ExecuteBuff(int buffid)
    {
        Buff _myb = Buffs[buffid];

        _myb.Update();
        if (_myb.gap > 0)
        {
            if ((int)_myb.duration % _myb.gap == 0)
            {
                _myb.Execute();
            }
        }
    }
    //******************************************** KONIEC BUFFY ******************************************** \\

    //******************************************** START DEBUFFY ******************************************** \\

    private int DebuffsAmount = 0;

    public Debuff DebuffMe(Debuff _myDebuff)
    {
        Debuff myDebuff = FindDebuff(_myDebuff.ID);

        if (myDebuff == null)
        {
            Debuffs[DebuffsAmount] = _myDebuff;
            Debuffs[DebuffsAmount].icon = Object.Instantiate(Resources.Load("DebuffIcon"), soldier.frame.transform.position + new Vector3(0f, 0f, 0), soldier.frame.transform.rotation) as GameObject;
            Debuffs[DebuffsAmount].icon.transform.SetParent(GameObject.Find("Canvas").transform);
            Debuffs[DebuffsAmount].icon.transform.localScale = new Vector3(1, 1, 1);
            Debuffs[DebuffsAmount].icon.GetComponent<DebuffScript>().myParent = soldier.frame;
            Debuffs[DebuffsAmount].icon.GetComponent<DebuffScript>().myid = DebuffsAmount;
            Debuffs[DebuffsAmount].SetIcon(GetDebuffIcon(_myDebuff.ID));
            Debuffs[DebuffsAmount].icon.GetComponent<DebuffScript>().SetParent(Debuffs[DebuffsAmount]);

            DebuffsAmount++;
        }
        else
        {
            myDebuff.Refresh(0);
        }

        soldier.UpdateDamageDoneBoost();
        soldier.UpdateDamageTakenBoost();
        soldier.UpdateHealingTakenBoost();

        return myDebuff;
    }

    private Sprite GetDebuffIcon(DEBUFF _debuff)
    {
        switch (_debuff)
        {
            case 0: return Resources.Load<Sprite>("");
            case DEBUFF.RAIN_OF_FIRE: return Resources.Load<Sprite>("EnemySpells/Tantrum");
            case DEBUFF.CONSUMING_DARKNESS: return Resources.Load<Sprite>("EnemySpells/Tantrum");
            case DEBUFF.TANTRUM_DOT: return Resources.Load<Sprite>("EnemySpells/Tantrum");
            case DEBUFF.CRY: return Resources.Load<Sprite>("EnemySpells/Cry");
            case DEBUFF.CURSED_SWIPE: return Resources.Load<Sprite>("EnemySpells/CursedSwipe");
            case DEBUFF.BRUTAL_BITE: return Resources.Load<Sprite>("EnemySpells/BrutalBite");
            case DEBUFF.SHADOWFANG: return Resources.Load<Sprite>("EnemySpells/Shadowfang");
            case DEBUFF.SOUL_TOMB: return Resources.Load<Sprite>("EnemySpells/SoulTomb");
            case DEBUFF.MIND_BOMB: return Resources.Load<Sprite>("EnemySpells/MindBomb");
            case DEBUFF.VOIDFLAME: return Resources.Load<Sprite>("EnemySpells/Voidflame");
            case DEBUFF.DARK_ROSE: return Resources.Load<Sprite>("EnemySpells/DarkRose");
            default: return Resources.Load<Sprite>("effect");
        }
    }

    /*
    public Buff[] sortCostam(int buffToRemove, Buff[] currentBuffs)
    {
        if (currentBuffs.GetValue(buffToRemove)
        {
            currentBuffs.Remove(buffToRemove);
        }


        return currentBuffs;
    }
    */

    public void SortDebuffs()
    {
        for (int i = 0; i < DebuffsAmount; i++)
        {
            if (Debuffs[i] != null)
            {
                if (Debuffs[i].duration <= 0)
                {
                    Debuffs[i].Remove();
                    Debuffs[i] = null;
                }
            }

            if (Debuffs[i] == null)
            {
                if (i < DebuffsAmount - 1)
                {
                    Debuffs[i] = Debuffs[i + 1];
                    Debuffs[i + 1] = null;
                }
                else
                {
                    DebuffsAmount--;

                    soldier.UpdateDamageDoneBoost();
                    soldier.UpdateDamageTakenBoost();
                    soldier.UpdateHealingTakenBoost();
                }
            }
        }
        RepositionIcons();
    }

    public void ActivateDebuffs()
    {
        if (DebuffsAmount > 0)
        {
            for (int i = 0; i < DebuffsAmount; i++)
            {
                Debuffs[i].Update();
            }
        }
    }

    public Debuff FindDebuff(DEBUFF debufftype)
    {
        if (DebuffsAmount > 0)
        {
            for (int i = 0; i < DebuffsAmount; i++)
            {
                if (Debuffs[i].ID == debufftype)
                {
                    return Debuffs[i];
                }
            }
        }
        return null;
    }

    //******************************************** KONIEC DEBUFFY ******************************************** \\

        /*
    private Debuff GetDebuffByID(DEBUFF _id)
    {
        switch (_id)
        {
            case DEBUFF.TANTRUM_DOT: return new Tantrum(soldier);
            case DEBUFF.CRY: return new Cry(soldier);
            case DEBUFF.CURSED_SWIPE: return new CursedSwipe(soldier);
            case DEBUFF.BRUTAL_BITE: return new BrutalBite(soldier);
            case DEBUFF.SHADOWFANG: return new Shadowfang(soldier);
            case DEBUFF.SOUL_TOMB: return new SoulTomb(soldier);
            case DEBUFF.MIND_BOMB: return new MindBomb(soldier);
            case DEBUFF.VOIDFLAME: return new Voidflame(soldier);
            case DEBUFF.DARK_ROSE: return new DarkRose(soldier);
            default: return null;
        }
    }*/

    public void Clear()
    {
        if (DebuffsAmount > 0)
        {
            for (int i = 0; i < DebuffsAmount; i++)
            {
                if (Debuffs[i].icon != null)
                {
                    GameObject.Destroy(Debuffs[i].icon);
                }
                Debuffs[i] = null;
                DebuffsAmount = 0;
            }
        }
        if (BuffsAmount > 0)
        {
            for (int i = 0; i < BuffsAmount; i++)
            {
                if (Buffs[i].icon != null)
                {
                    GameObject.Destroy(Buffs[i].icon);
                }
                Buffs[i] = null;
                BuffsAmount = 0;
            }
        }
    }
}


