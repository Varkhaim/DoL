using UnityEngine;
using System.Collections;

public class CursedSwipe : Debuff
{
    public CursedSwipe(Soldier _myParent) : base(DEBUFF.CURSED_SWIPE, _myParent)
    {
        ID = DEBUFF.CURSED_SWIPE;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.CURSED_SWIPE);
        InitDebuff();
    }

    public override void Execute()
    {
        //myParent.Damage(einfo.baseValue2 / einfo.ticksCount, einfo.baseValue2 / einfo.ticksCount, 0f, DAMAGESOURCE.CURSED_SWIPE);
    }
}
