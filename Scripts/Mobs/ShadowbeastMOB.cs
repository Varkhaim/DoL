using UnityEngine;
using System.Collections;

public class ShadowbeastMOB : Mob
{

    public ShadowbeastMOB() : base()
    {
        maxHealth = 800f;
        currHealth = maxHealth;
        name = "The Shadowbeast";
        mobClass = MOBCLASS.BOSS;

        myPortrait = LoadMobPortrait(MOB.SHADOWBEAST);

        AddSpell(HOSTILESPELL.SHADOWFANG);
        AddSpell(HOSTILESPELL.BRUTAL_BITE);
        AddSpell(HOSTILESPELL.ROAR_OF_TERROR);
        AddSpell(HOSTILESPELL.CURSED_SWIPE);
        AddSpell(HOSTILESPELL.SUMMON_SPAWNLING);

        timerHandler.AddTimer(mySpell[0], true);
        timerHandler.AddTimer(mySpell[1], true);
        timerHandler.AddTimer(mySpell[2], true);
        timerHandler.AddTimer(mySpell[4], true);

        spellQueue.SetFillerSpell(mySpell[3]);

        myBossTalk = new BossTalk(2);

        myBossTalk.AddText("Roar!", Color.blue);
        myBossTalk.SetDeathTalk("Master...", Color.yellow);
    }

    public override void Update()
    {
        base.Update();
        if (phase == 0)
        {
            if (HealthPercentage < 0.5f)
            {
                phase = 1;
                val1 += 2;
            }
        }
    }
}