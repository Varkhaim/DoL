using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Faith_Buff : Buff
{
    public Faith_Buff(BUFF _ID, int _duration, Soldier _myParent, Caster _caster, SpellInfo _info, int _gap, int _val=0) : base(_ID, _duration, _myParent, _caster, _info, _gap, _val)
    {
        myIcon = Resources.Load<Sprite>("BuffsIcons/Faith");
    }

    public override void Execute()
    {

    }
}

