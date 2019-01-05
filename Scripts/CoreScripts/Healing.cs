using UnityEngine;
using System.Collections;

public class Healing
{
	public int value;
	public bool isCrit;
    public HEALTYPE healtype;
    public int overhealing = 0;

    public Healing (int _value, bool _isCrit, HEALTYPE _healtype)
	{
		value = _value;
		isCrit = _isCrit;
        healtype = _healtype;
	}
}


