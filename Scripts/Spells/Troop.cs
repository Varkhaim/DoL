using UnityEngine;
using System.Collections;

public class Troop
{
    public int amount;
    public Soldier[] Soldier;

    public Troop(int _amount, Soldier[] _Soldier)
    {
        amount = _amount;
        Soldier = _Soldier;
    }
}
