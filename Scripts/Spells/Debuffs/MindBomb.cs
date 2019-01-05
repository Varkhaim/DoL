using UnityEngine;
using System.Collections;

public class MindBomb : Debuff
{
    public MindBomb(Soldier _myParent) : base(DEBUFF.MIND_BOMB, _myParent)
    {
        ID = DEBUFF.MIND_BOMB;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.MIND_BOMB);
        InitDebuff();
    }

    public override void Execute()
    {
        myParent.Damage(einfo.baseValue2, einfo.baseValue2, 0f, DAMAGESOURCE.MIND_BOMB);
        Soldier[] nearbies = GameCore.Core.troopsHandler.GetTargets(TARGETTYPE.BY_HEALTH, 8, null, myParent).Soldier;
        foreach (Soldier sol in nearbies)
        {
            sol.Damage(einfo.baseValue2 / 6, einfo.baseValue2 / 6, 0f, DAMAGESOURCE.MIND_BOMB);
        }
    }
}
