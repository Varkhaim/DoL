using UnityEngine;
using System.Collections;

public class ShadowbeastEncounter : Encounter
{
    public ShadowbeastEncounter() : base()
    {

    }

    public override void InitEncounter()
    {
        base.InitEncounter();
        SpawnMob(MOB.SHADESPAWNLING);
        SpawnMob(MOB.SHADOWBEAST);
        SpawnMob(MOB.SHADESPAWNLING);
    }
}
