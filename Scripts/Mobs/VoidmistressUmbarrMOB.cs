using UnityEngine;
using System.Collections;

public class VoidmistressUmbarrMOB : Mob
{

    public VoidmistressUmbarrMOB() : base()
    {
        maxHealth = 900f;
        currHealth = maxHealth;
        name = "Voidmistress Umbarr";

        myPortrait = LoadMobPortrait(MOB.UMBARR);

        AddSpell(HOSTILESPELL.BLOODBOIL);
        AddSpell(HOSTILESPELL.DARK_ROSE);
        AddSpell(HOSTILESPELL.VOIDFLAME);
        AddSpell(HOSTILESPELL.SCRATCH);

        timerHandler.AddTimer(mySpell[0], true);
        timerHandler.AddTimer(mySpell[1], true);
        timerHandler.AddTimer(mySpell[2], true);
        spellQueue.SetFillerSpell(mySpell[3]);

        myBossTalk = new BossTalk(4);

        myBossTalk.AddText("You're making me hot!", Color.red);
        myBossTalk.AddText("A rose? For me?", Color.red);
        myBossTalk.AddText("Can you feel my... void?", Color.red);
        myBossTalk.AddText("You know what? Or never mind. Nothing.", Color.red);
        myBossTalk.SetDeathTalk("We shall meet again hotty...", Color.yellow);
    }

    public override void Update()
    {
        base.Update();
                
    }
}