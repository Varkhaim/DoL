using UnityEngine;
using System.Collections;

public class SoolTheWarlockMOB : Mob
{

    public SoolTheWarlockMOB() : base()
    {
        maxHealth = 400f;
        currHealth = maxHealth;
        name = "Sool the Warlock";

        myPortrait = LoadMobPortrait(MOB.SOOL);

        AddSpell(HOSTILESPELL.SOUL_TOMB);
        AddSpell(HOSTILESPELL.MIND_BOMB);
        AddSpell(HOSTILESPELL.SHADOWBOLT);

        timerHandler.AddTimer(mySpell[0], true);
        timerHandler.AddTimer(mySpell[1], true);
        spellQueue.SetFillerSpell(mySpell[2]);

        myBossTalk = new BossTalk(4);

        myBossTalk.AddText("Don't interfere with our plans!", Color.blue);
        myBossTalk.AddText("The void is the only answer!", Color.cyan);
        myBossTalk.AddText("Power produces darkness. Darkness consumes shadows!", Color.blue);
        myBossTalk.SetDeathTalk("<Sool disappears>", Color.white);
    }

    public override void Update()
    {
        base.Update();
    }
}