using UnityEngine;
using System.Collections;

public class HostileSpell
{
	public int spellid;
	public int targettype;
	public int targets;  

    public HostileSpell (int _spellid, int _targettype, int _targets)
	{
		spellid = _spellid;
		targettype = _targettype;
		targets = _targets;
	}
}

