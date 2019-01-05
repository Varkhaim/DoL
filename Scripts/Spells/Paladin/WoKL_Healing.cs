using UnityEngine;
using System.Collections;

public class WoKL_Healing : SpellEffect
{
    public WoKL_Healing() : base()
    {

    }

    public override void OnCast(Caster who, Soldier target)
    {
        target.CastFinished(this, who);
    }

    public override void OnCastFinished(Caster who, Soldier target, int val = 0)
    {
        if (target.effectSystem.FindBuff(BUFF.WORD_OF_KINGS_LOYALTY) != null)
            Spell.Cast(this, target, who, val);
        // healing z beaconow (spelltype 4) dziala tylko na cele z buffkiem (buff 2)
    }

    public override void Execute(Caster who, Soldier target, int val=0)
    {
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.WORD_OF_KINGS_LOYALTY_HEALING);
        target.Heal(who, val, 0, HEALSOURCE.WOK_LOYALTY, HEALTYPE.OTHER);
    }

}