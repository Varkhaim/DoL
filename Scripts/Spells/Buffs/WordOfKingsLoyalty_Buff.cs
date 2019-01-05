using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WordOfKingsLoyalty_Buff : Buff
{
    public WordOfKingsLoyalty_Buff(BUFF _ID, int _duration, Soldier _myParent, Caster _caster, SpellInfo _info, int _gap, int _val=0) : base(_ID, _duration, _myParent, _caster, _info, _gap, _val)
    {
        myIcon = Resources.Load<Sprite>("BuffsIcons/WordOfKingsLoyalty");
    }

    public override void Execute()
    {

    }
}

