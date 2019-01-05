using UnityEngine;
using System.Collections;

public class WordOfKingsFaith : SpellEffect
{
    public WordOfKingsFaith() : base()
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

    public override void Execute(Caster who, Soldier target, int val = 0)
    {
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.WORD_OF_KINGS_FAITH);
        int _dur = spellInfo.ticksCount * spellInfo.HoTgap;
        int _gap = spellInfo.HoTgap;
    
        target.BuffMe(BUFF.WORD_OF_KINGS_FAITH, _dur, who, spellInfo, _gap);
        target.BuffMe(BUFF.FAITH, 900, who, spellInfo, 900);

        GameCore.Core.paladinSparkHandler.AddSparks(1);

        if (!target.frame.GetComponent<AudioSource>().isPlaying)
            target.frame.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/WoKFaithSound"));
    }

}