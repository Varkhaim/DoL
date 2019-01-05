using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WordOfKingsFaith_Buff : Buff
{
    public WordOfKingsFaith_Buff(BUFF _ID, int _duration, Soldier _myParent, Caster _caster, SpellInfo _info, int _gap, int _val=0) : base(_ID, _duration, _myParent, _caster, _info, _gap, _val)
    {
        myIcon = Resources.Load<Sprite>("BuffsIcons/WordOfKingsFaith");
    }

    public override void Execute()
    {
        int _value = 0;
        _value = spellInfo.baseValue2 / spellInfo.ticksCount;
        _value += (int)(myCaster.GetPower() * spellInfo.coeff2 / spellInfo.ticksCount);

        myParent.Heal(myCaster, _value, myCaster.GetCritChance(), HEALSOURCE.WOK_FAITH, HEALTYPE.PERIODIC_SINGLE);

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

