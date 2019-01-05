using UnityEngine;
using System.Collections;

public class Shadowfang : Debuff
{
    public Shadowfang(Soldier _myParent) : base(DEBUFF.SHADOWFANG, _myParent)
    {
        ID = DEBUFF.SHADOWFANG;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.SHADOWFANG);
        InitDebuff();
    }

    public override void RefreshAction()
    {
        multiplier += 1f;
        icon.GetComponent<DebuffScript>().Animate();
    }

    public override void Execute()
    {
        float temp = (myParent.maxHealth - myParent.health) * ((float)einfo.baseValue2) / 100f;
        myParent.Damage(temp, temp, 0f, DAMAGESOURCE.SHADOWFANG);
    }
}
