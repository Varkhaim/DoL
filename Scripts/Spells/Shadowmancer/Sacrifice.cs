using UnityEngine;
using System.Collections;

public class Sacrifice : SpellEffect
{
    public Sacrifice() : base()
    {
    }

    public override void OnCast(Caster who, Soldier target)
    {
        foreach (Soldier _Soldier in GameCore.Core.troopsHandler.soldier)
        {
            _Soldier.CastFinished(this, who);
        }
    }

    public override void OnCastFinished(Caster who, Soldier target, int val=0)
    {
        Spell.Cast(this, target, who);
    }

    public override void Execute(Caster who, Soldier target, int val=0)
    {
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.SACRIFICE);

        target.Heal(who, spellInfo, HEALSOURCE.SACRIFICE, spellInfo.healtype);
    }

}