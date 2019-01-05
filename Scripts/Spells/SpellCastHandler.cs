using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellCastHandler
{
    private int baseGCD = 60;
    public int GCD = 0;

    public GameObject myGCDbar;
    public GameObject myCastBar;
    //private GameCore core = GameCore.Core;

    private int castProgress = 0;
    private int castTime = 0;
    private int manaToSpend;

    private bool isCasting = false;

    private Spell spell;
    private Soldier[] targets;
    private Caster caster;
    private bool channeling;
    
    private Soldier[] incCastSoldiers;
    private Spell incCastSpell;
    private Caster incCastCaster;
    private bool incChanneling;

    public SpellCastHandler()
    {

    }
    
    public bool IsCasting()
    {
        return isCasting;
    }

    public void Update()
    {
        myCastBar.GetComponent<CastBar>().SetGCD(1f - (float)GCD / (float)baseGCD);

        if (GCD > 0)
            GCD--;

        if (castTime <= 0)
            myCastBar.GetComponent<CastBar>().SetFill(0);
        else
        {
            if (channeling)
                myCastBar.GetComponent<CastBar>().SetFill(1f - (float)castProgress / (float)castTime);
            else
                myCastBar.GetComponent<CastBar>().SetFill((float)castProgress / (float)castTime);
        }

        if (isCasting)
        {
            if (channeling)
            {
                if (castProgress++ % 30 == 0)
                {
                    SpellTick();
                    if (castProgress >= castTime)
                    {
                        isCasting = false;
                        castProgress = 0;
                        myCastBar.GetComponent<CastBar>().Clear();
                    }
                }
            }
            else
            if (castProgress++ >= castTime)
            {
                //GameCore.Core.mySpellIcon.GetComponent<Image>().color = Color.clear;
                myCastBar.GetComponent<CastBar>().Clear();
                isCasting = false;
                CastSpell();
                castProgress = 0;
            }
        }
        else
        if (incCastSpell != null)
        {
            PrepareSpell(incCastSoldiers, incCastSpell, incCastCaster);
            this.incCastSoldiers = null;
            this.incCastSpell = null;
            this.incCastCaster = null;
            this.incChanneling = false;
        }
    }

    private bool CheckMana()
    {
        int spellManaCost = GameCore.Core.spellRepository.Get(spell.ID).manaCost;
        manaToSpend = spellManaCost;
        
        if (spell.ID == SPELL.WORD_OF_KINGS_COURAGE)
            if (GameCore.Core.buffSystem.FindBuff(CASTERBUFF.DIVINE_INTERVENTION) != null)
            {
                manaToSpend = 0;
                return true;
            }

        if (GameCore.Core.myCaster.ManaCurrent - spellManaCost <= 0)
            return false;
                
        return true;
    }

    public void PrepareSpell(Soldier[] _targets, Spell spell, Caster caster)
    {
        SpellInfo info = GameCore.Core.spellRepository.Get(spell.ID);
        if (isCasting)
        {
            if (castProgress < 60)
            {
                this.incCastSoldiers = _targets;
                this.incCastSpell = spell;
                this.incCastCaster = caster;
                this.incChanneling = info.channeling;
            }
            return;
        }

        this.targets = _targets;
        this.spell = spell;
        this.caster = caster;
        this.channeling = info.channeling;

        StartCast(info, targets);

    }   

    private void StartCast(SpellInfo info, Soldier[] _targets)
    {
        foreach (Soldier tar in _targets)
        {           
            if (tar != null)
            {
                if (IsAbleToBeCast(tar))
                {
                    int _cast = info.castTime;
                    
                    GCD = baseGCD;

                    _cast = CheckCastBuffers(_cast);
                    
                    if (_cast > 0)
                        castTime = Mathf.Max(_cast, baseGCD);
                    else
                        castTime = _cast;

                    if (castTime == 0)
                    {
                        CastSpell();
                    }
                    else
                    {
                        myCastBar.GetComponent<CastBar>().SetCast(spell.iconSprite, spell.name);
                        isCasting = true;
                    }
                }
            }
        }
    }

    private bool IsAbleToBeCast(Soldier _tar)
    {
        return (!_tar.isDead) && (CheckMana()) && (!spell.isOnCooldown()) && ((GCD <= 0) && (castProgress <= 0));
    }

    private int CheckCastBuffers(int _cast)
    {
        CasterBuff temp = GameCore.Core.buffSystem.FindBuff(CASTERBUFF.CONSECRATION);
        if ((temp != null) && (_cast > 0))
        {
            if (spell.ID == SPELL.WORD_OF_KINGS_FAITH)
            {
                _cast /= 2;
                temp.Remove();
            }
        }

        if (caster.myAura[(int)AURA.FLASH_OF_FUTURE].isActive)
        {
            if (spell.ID == SPELL.WORD_OF_KINGS_FAITH)
            {
                CasterBuff myb = GameCore.Core.buffSystem.FindBuff(CASTERBUFF.FLASH_OF_FUTURE_FAITH);
                if (myb != null)
                {
                    myb.Remove();
                    _cast = 0;
                }
            }
        }
        return _cast;
    }

    public void CastSpell()
    {
        GameCore.Core.troopsHandler.SortSoldiers();

        spell.SetCooldown();

        GameCore.Core.myCaster.SpendMana(manaToSpend); // zuzywamy mane

        foreach (Soldier _tar in targets)
        {
            if (null != _tar)
            {
                spell.mEffect.OnCast(caster, _tar);
            }
        }
    }

    public void SpellTick()
    {
        GameCore.Core.troopsHandler.SortSoldiers();

        foreach (Soldier _tar in targets)
            spell.mEffect.OnCast(caster, _tar);
    }
}
