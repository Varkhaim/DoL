using UnityEngine;
using System.Collections;

public class WordOfKingsLight : SpellEffect
{
    public WordOfKingsLight() : base()
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
        SpellInfo spellInfo = GameCore.Core.spellRepository.Get(SPELL.WORD_OF_KINGS_LIGHT);

        Healing temp;
        temp = target.Heal(who, spellInfo, HEALSOURCE.WOK_LIGHT, spellInfo.healtype);
        
        GameCore.Core.myCaster.RestoreMana(1, 2);
        GameCore.Core.paladinSparkHandler.AddSparks(1);

        if (!target.frame.GetComponent<AudioSource>().isPlaying)
        target.frame.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/WoKLightSound"));

        if (Random.Range(0,100)<25)
        {
            GameCore.Core.FindSpell(SPELL.WORD_OF_KINGS_COURAGE).ChangeCooldown(0); // reset cd na Courage
        }

        GameCore.Core.FindSpell(SPELL.WORD_OF_KINGS_LOYALTY).ChangeCooldown(-120); // skrocenie o 2 sekundy cooldownu na Loyalty

        //target.SpawnEffect(Resources.Load("Animations/lightexplosion_0") as GameObject);
    }

}