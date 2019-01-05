using UnityEngine;
using System.Collections;

public class SoothingVoid : SpellEffect
{
    public SoothingVoid() : base()
    {
    }

    public override void OnCast(Caster who, Soldier target)
    {
        target.CastFinished(this, who);
        if (who.myTalentTree.GetTalentPointsByName("Book of Prime Shadows") > 0)
        {
            GameCore.Core.buffSystem.BuffMe(CASTERBUFF.BOOK_OF_PRIME_SHADOWS, 900f, who);
        }

    }

    public override void OnCastFinished(Caster who, Soldier target, int val=0)
    {
        Spell.Cast(this, target, who);
    }

    public override void Execute(Caster who, Soldier target, int val=0)
    {
        if (target.effectSystem.FindBuff(BUFF.SOOTHING_VOID) == null)
        {
            SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.SOOTHING_VOID);


            //Object.Instantiate(Resources.Load("Visuals/WanShaParticle"), target.frame.transform.position + new Vector3(1.4f, -0.5f), Quaternion.Euler(0, 0, 0));

            //target.Heal(_value, _value + 5f, GameCore.Core.criticalStrikeChance, who, this, HEALSOURCE.SOOTHING_VOID, spellInfo.healtype);
            target.BuffMe(BUFF.SOOTHING_VOID, spellInfo.ticksCount * spellInfo.HoTgap, who, spellInfo, spellInfo.HoTgap);
        }
    }

}