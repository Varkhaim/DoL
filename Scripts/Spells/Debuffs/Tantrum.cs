using UnityEngine;
using System.Collections;

public class Tantrum : Debuff
{
    public Tantrum(Soldier _myParent) : base(DEBUFF.TANTRUM_DOT, _myParent)
    {
        ID = DEBUFF.TANTRUM_DOT;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.TANTRUM);
        InitDebuff();
    }

    public override void Execute()
    {
        myParent.Damage(einfo.baseValue2 / einfo.ticksCount, einfo.baseValue2 / einfo.ticksCount, 0f, DAMAGESOURCE.TANTRUM);
    }
}
