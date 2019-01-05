using UnityEngine;
using System.Collections;

public class SpellEffect
{
    public SpellEffect()
    {
    }

    public virtual void OnCast(Caster who, Soldier target)
    {
        // co sie dzieje w momencie zaczecia castowania
    }

    public virtual void OnCastFinished(Caster who, Soldier target, int val = 0)
    {
        // co sie dzieje w momencie wycastowania
    }

    public virtual void Execute(Caster who, Soldier target, int val = 0)
    {
        // wlasciwy efekt spella
    }
}
