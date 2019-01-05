using UnityEngine;
using System.Collections;

public class Cry : Debuff
{
    public Cry(Soldier _myParent) : base(DEBUFF.CRY, _myParent)
    {
        ID = DEBUFF.CRY;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.CRY);
        InitDebuff();
    }

    public override void Execute()
    {
        myParent.Damage(einfo.baseValue2 / einfo.ticksCount, einfo.baseValue2 / einfo.ticksCount, 0f, DAMAGESOURCE.CRY);
        if (duration <= 1)
            duration = maxDuration;
    }
}
