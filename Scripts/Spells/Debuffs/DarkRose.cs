using UnityEngine;
using System.Collections;

public class DarkRose : Debuff
{
    public DarkRose(Soldier _myParent) : base(DEBUFF.DARK_ROSE, _myParent)
    {
        ID = DEBUFF.DARK_ROSE;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.DARK_ROSE);
        InitDebuff();
    }

    public override void Execute()
    {
        if (myParent.GetPercentHealth() < 0.6f)
        {
            float temp = (einfo.baseValue2 / einfo.ticksCount);
            myParent.Damage(temp, temp, 0f, DAMAGESOURCE.DARK_ROSE);
        }
        Refresh(0);
    }
}
