using UnityEngine;
using System.Collections;

public class Absorb
{
    public float value;
    public HEALSOURCE source;

    public Absorb(float _val, HEALSOURCE _source)
    {
        value = _val;
        source = _source;
    }
}
