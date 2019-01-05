using UnityEngine;
using System.Collections;

public class TwilightBeam : SpellEffect
{
    public TwilightBeam() : base()
    {

    }

    public override void OnCast(Caster who, Soldier target)
    {

        target.CastFinished(this, who);
    }

    public override void OnCastFinished(Caster who, Soldier target, int val=0)
    {
        Spell.Cast(this, target, who);
    }

    public override void Execute(Caster who, Soldier target, int val=0)
    {
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.TWILIGHT_BEAM);
        int _tempgap = spellInfo.HoTgap - who.myTalentTree.GetTalentPointsByName("Dawn") * 6;
        int _dur = spellInfo.ticksCount * _tempgap;
        target.BuffMe(BUFF.TWILIGHT_BEAM, _dur, who, spellInfo, _tempgap);
    }

}