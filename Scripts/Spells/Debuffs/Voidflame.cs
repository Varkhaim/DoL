using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Voidflame : Debuff
{
    public Voidflame(Soldier _myParent) : base(DEBUFF.VOIDFLAME, _myParent)
    {
        ID = DEBUFF.VOIDFLAME;
        einfo = GameCore.Core.espellHandler.GetESpellInfo(HOSTILESPELL.VOIDFLAME);
        InitDebuff();
        multiplier = 3f;
    }

    public override void Execute()
    {
        float temp = (einfo.baseValue2 / einfo.ticksCount) * multiplier;
        myParent.Damage(temp, temp, 0f, DAMAGESOURCE.VOIDFLAME);
        multiplier += 0.2f;
        RefreshIcon();

        Refresh(0);
    }

    public void RefreshIcon()
    {
        if (icon != null)
        {
            if (multiplier <= 1f)
                icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("EnemySpells/VoidflameWeak");
            else
                icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("EnemySpells/Voidflame");
        }
    }
}
