using UnityEngine;
using System.Collections;

public class ScrollOfAlaphi : SpellEffect
{
    public ScrollOfAlaphi() : base()
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
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.SCROLL_OF_ALAPHI);

        target.Heal(who, (int)(target.maxHealth*0.5f), 0f, HEALSOURCE.SCROLL_OF_ALAPHI, spellInfo.healtype);

        //target.SpawnEffect(Resources.Load("Animations/lightexplosion_0") as GameObject);        
    }

}