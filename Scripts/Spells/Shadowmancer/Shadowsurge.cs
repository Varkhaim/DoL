using UnityEngine;
using System.Collections;

public class Shadowsurge : SpellEffect
{
    public Shadowsurge() : base()
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
        if (target.effectSystem.FindBuff(BUFF.SOOTHING_VOID) != null)
            Spell.Cast(this, target, who);
    }

    public override void Execute(Caster who, Soldier target, int val=0)
    {
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.SHADOWSONG);

        target.Heal(who, spellInfo, HEALSOURCE.SHADOWSURGE, spellInfo.healtype);

    }
}