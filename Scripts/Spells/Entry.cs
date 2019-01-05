using UnityEngine;
using System.Collections;

public class Entry
{
    public string name; // nazwa zrodla healingu
    public int healing; // wartosc healingu
    public int overhealing; // wartosc overheala

    public Entry(string _name, int _healing, int _overhealing)
    {
        name = _name;
        healing = _healing;
        overhealing = _overhealing;
    }
}
