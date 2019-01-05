using UnityEngine;
using System.Collections;

public enum DB
{
    NUL,
    WHISPERS_OF_DESPAIR,
    SHADOWFORGE_UMBARR,
    EUGENE_THE_WITCH
}

public class Progress
{
    public bool[] missionComplete = new bool[100];

    public Progress()
    {
        for (int i=0; i<100; i++)
        {
            missionComplete[i] = false;
        }
    }

    public void Update(int _advID)
    {
        missionComplete[_advID] = true;
    }
}
