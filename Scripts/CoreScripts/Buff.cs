using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum BUFF
{
    NOTHING,
    WORD_OF_KINGS_FAITH,
    FAITH,
    WORD_OF_KINGS_LOYALTY,
    ROYALTY,
    FLASH_OF_FUTURE,
    SOOTHING_VOID,
    TWILIGHT_BEAM,
    SHADOWMEND
}

public class Buff
{


    public BUFF ID;
    public float duration;
    public float maxDuration;
    public GameObject icon = null;
    public float multiplier = 1f;
    public GameObject myText;
    public Soldier myParent;
    public Caster myCaster;
    public int refreshCount = 0;
    public SpellEffect mEffect;
    public SpellInfo spellInfo;
    public int val;
    public int gap;
    protected Sprite myIcon;
    

    public Buff(BUFF _ID, int _duration, Soldier _myParent, Caster _caster, SpellInfo _info, int _gap, int _val=0)
    {
        ID = _ID;
        maxDuration = _duration;
        duration = maxDuration;
        myParent = _myParent;
        myCaster = _caster;
        spellInfo = _info;
        mEffect = spellInfo.effect;
        val = _val;
        gap = _gap;
    }

    public virtual void Refresh(float _value)
    {
        // 0 - odswiez do pelnego czasu
        if (_value == 0)
        {
            duration = maxDuration;
        }
        else
        {
            duration += _value;
        }
    }

    public void SetIcon(Sprite _icon)
    {
        icon.transform.GetChild(0).GetComponent<Image>().sprite = _icon;
    }

    public void SetIcon()
    {
        icon.transform.GetChild(0).GetComponent<Image>().sprite = myIcon;
    }

    public virtual void Remove()
    {
        /*
        if (ID ==(int)DB.WORD_OF_KINGS_LOYALTY)
        {
            GameCore.Core.RemoveBeaconedTarget(myParent);
        }
        */

        Object.Destroy(myText);
        Object.Destroy(icon);
    }

    public void Update()
    {
        if (myParent.isDead)
        {
            Remove();
        }
        else
        {
            duration = Mathf.Max(0, --duration);
            if (icon != null)
            {
                icon.transform.GetChild(3).GetComponent<Image>().fillAmount = (float)duration / (float)maxDuration;
            }
        }

        if (myText != null)
            myText.GetComponent<TextMesh>().text = Mathf.Ceil(duration / 60f).ToString();
    }

    public virtual void Execute()
    {
        /*
        int _value = 0;
        _value = spellInfo.baseValue2 / spellInfo.ticksCount;
        _value += (int)(GameCore.Core.chosenAccount.statPWR * spellInfo.coeff2 / spellInfo.ticksCount);

        switch (ID)
        {
            case 0:
                {
                    // no buff
                }
                break;
            case BUFF.WORD_OF_KINGS_FAITH:
                {
                    Healing temp = myParent.Heal(myCaster, spellInfo, HEALSOURCE.WOK_FAITH, HEALTYPE.PERIODIC_SINGLE);
                    if (temp.isCrit)
                    {
                        if (myCaster.myAura[(int)AURA.DIVINITY].isActive)
                            myParent.Shield((int)(temp.value * VALUES.DIVINITY_PERCENT), HEALSOURCE.DIVNITY_SHIELD);
                    }
                    if (temp.overhealing > 0)
                    {
                        if (myCaster.myAura[(int)AURA.EMPATHY].isActive)
                        myParent.Shield((int)(temp.overhealing * VALUES.EMPATHY_PERCENT), HEALSOURCE.EMPATHY);
                    }
                }
                break;
            case BUFF.SHADOWMEND:
                {
                    myParent.Heal(myCaster, spellInfo, HEALSOURCE.SHADOWMEND, HEALTYPE.PERIODIC_MULTI);
                }
                break;
            case BUFF.SOOTHING_VOID:
                {
                    myParent.Heal(myCaster, spellInfo, HEALSOURCE.SOOTHING_VOID, HEALTYPE.PERIODIC_SINGLE);
                }
                break;
            case BUFF.TWILIGHT_BEAM:
                {
                    float _pen = 1.5f - (multiplier*0.1f);
                    int _val = (int)(_value * _pen);

                    Healing _heal = myParent.Heal(myCaster, _val, myCaster.GetCritChance(), HEALSOURCE.TWILIGHT_BEAM, HEALTYPE.PERIODIC_SINGLE);
                    multiplier += 1;
                }
                break;
        }
        */
    }
}

