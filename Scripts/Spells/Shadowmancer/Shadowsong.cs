using UnityEngine;
using System.Collections;

public class Shadowsong : SpellEffect
{
    public Shadowsong() : base()
    {

    }

    public override void OnCast(Caster who, Soldier target)
    {
        int pickedRow = target.myID % 4;

        Soldier[] _targets = new Soldier[4];

        for (int i = 0; i < 4; i++)
        {
            _targets[i] = GameCore.Core.troopsHandler.soldier[pickedRow + i * 4];
        }

        foreach (Soldier _Soldier in _targets)
        {
            if (!_Soldier.isDead)
                _Soldier.CastFinished(this, who);
        }
    }

    public override void OnCastFinished(Caster who, Soldier target, int val=0)
    {
        Spell.Cast(this, target, who);
    }

    public override void Execute(Caster who, Soldier target, int val=0)
    {
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.SHADOWSONG);
 
        Healing _heal = target.Heal(who, spellInfo, HEALSOURCE.SHADOWSONG, spellInfo.healtype);
    }

}