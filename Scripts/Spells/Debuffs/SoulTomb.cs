using UnityEngine;
using System.Collections;

public class SoulTomb : Debuff
{
    public SoulTomb(Soldier _myParent) : base(DEBUFF.SOUL_TOMB, _myParent)
    {
        ID = DEBUFF.SOUL_TOMB;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.SOUL_TOMB);
        InitDebuff();
    }

    public override void Execute()
    {
        if (myParent.health / myParent.maxHealth < 0.5f)
            Remove();
        float temp = (einfo.baseValue2 / einfo.ticksCount)*multiplier;
        multiplier += 0.1f;
        myParent.Damage(temp, temp, 0f, DAMAGESOURCE.SOUL_TOMB);

    }
}
