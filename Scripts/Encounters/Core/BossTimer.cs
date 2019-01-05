using UnityEngine;
using System.Collections;

public class BossTimer
{
    public EnemySpellInfo spell;
    public int timer;
    public int maxTimer;
    private bool loop;
    private bool paused = false;
    public bool isActive = true;
    public MobTimerHandler hand;
    private int ID;

    public BossTimer(int _id, EnemySpellInfo _spellInfo, int _maxTimer, bool _loop, MobTimerHandler _hand)
    {
        ID = _id;
        spell = _spellInfo;
        maxTimer = _maxTimer;
        loop = _loop;
        hand = _hand;
    }

    public void Update()
    {
        if ((!paused) && (isActive))
        {
            timer++;
            if (timer >= maxTimer)
            {
                hand.CastSpell(ID);
                if (loop)
                {
                    timer = 0;
                }
                else
                {
                    isActive = false;
                }
            }
        }
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }
}
