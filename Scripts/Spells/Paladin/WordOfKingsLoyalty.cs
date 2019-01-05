using UnityEngine;
using System.Collections;

public class WordOfKingsLoyalty : SpellEffect
{
    public WordOfKingsLoyalty() : base()
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
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.WORD_OF_KINGS_LOYALTY);
        int dur = spellInfo.HoTgap * spellInfo.ticksCount;
        target.BuffMe(BUFF.WORD_OF_KINGS_LOYALTY, dur, who, spellInfo, 0);

        GameCore.Core.paladinSparkHandler.AddSparks(4);

        if (!target.frame.GetComponent<AudioSource>().isPlaying)
            target.frame.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/WoKLoyaltySound"));
    }

}