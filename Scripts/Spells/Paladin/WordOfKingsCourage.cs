using UnityEngine;
using System.Collections;

public class WordOfKingsCourage : SpellEffect
{
    public WordOfKingsCourage() : base()
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
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.WORD_OF_KINGS_COURAGE);

        //Object.Instantiate(Resources.Load("Visuals/WoKCourageParticle"), target.frame.transform.position + new Vector3(1.4f, -0.5f), Quaternion.Euler(270, 0, 0));

        target.Heal(who, spellInfo, HEALSOURCE.WOK_COURAGE, spellInfo.healtype);

        GameCore.Core.paladinSparkHandler.AddSparks(1);

        GameObject.Instantiate(Resources.Load("Animations/lightexplosion_0"), target.frame.transform.position + new Vector3(0, 0.35f, 0), Quaternion.Euler(0, 0, 0));
    }

}