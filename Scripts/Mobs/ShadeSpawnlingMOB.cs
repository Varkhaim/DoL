using UnityEngine;
using System.Collections;

public class ShadeSpawnlingMOB : Mob
{

    public ShadeSpawnlingMOB() : base()
    {
        maxHealth = 300f;
        currHealth = maxHealth;
        name = "Shade Spawnling";

        myPortrait = LoadMobPortrait(MOB.SHADOWBEAST);

        AddSpell(HOSTILESPELL.SCRATCH);

        spellQueue.SetFillerSpell(mySpell[0]);
    }

    public override void Update()
    {
        base.Update();
    }
}