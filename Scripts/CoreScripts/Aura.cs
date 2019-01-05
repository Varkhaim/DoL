using UnityEngine;
using System.Collections;

public enum AURA
{
    HAND_OF_LIGHT,
    IRON_FAITH,
    SPIRIT_BOND,
    AURA_OF_LIGHT,
    CONSECRATION,
    ROYALTY,
    DIVINITY,
    EMPATHY,
    GENEROUSITY,
    MODESTY,
    VISIONS_OF_ANCIENT_KINGS,
    GUIDANCE_OF_RAELA,
    FLASH_OF_FUTURE
}

public class Aura
{
    public bool isActive;
    public int stacks;

    public Aura(bool _act, int _stacks)
    {
        isActive = _act;
        stacks = _stacks;
    }

    public Aura(Talent _tal)
    {
        isActive = (_tal.Points>0);
        stacks = Mathf.Max(0,_tal.Points-1);
    }
}
