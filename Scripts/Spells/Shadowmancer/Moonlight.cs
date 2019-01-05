using UnityEngine;
using System.Collections;

public class Moonlight : SpellEffect
{
    public Moonlight() : base()
    {

    }

    public override void OnCast(Caster who, Soldier target)
    {
        target.CastFinished(this, who);
    }

    public override void OnCastFinished(Caster who, Soldier target, int val=0)
    { 
        Spell.Cast(this, target, who, val);
    }

    public override void Execute(Caster who, Soldier target, int val=0)
    {
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.MOONLIGHT);
        target.Heal(who, spellInfo, HEALSOURCE.MOONLIGHT, spellInfo.healtype);
    }

}