using UnityEngine;
using System.Collections;

public class BrutalBite : Debuff
{
    public BrutalBite(Soldier _myParent) : base(DEBUFF.BRUTAL_BITE, _myParent)
    {
        ID = DEBUFF.BRUTAL_BITE;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.BRUTAL_BITE);
        InitDebuff();
    }

    public override void Execute()
    {
        if (myParent.health / myParent.maxHealth > 0.9f)
            Remove();
        else
            Refresh(0);
        float temp = (myParent.maxHealth - myParent.health)*((float)einfo.baseValue2)/100f;
        myParent.Damage(temp, temp, 0f, DAMAGESOURCE.BRUTAL_BITE);

    }
}
