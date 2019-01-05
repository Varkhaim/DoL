using UnityEngine;
using System.Collections;

public class MobTimerHandler
{
    public BossTimer[] timer = new BossTimer[10];
    private bool paused = false;
    public bool isActive = true;
    int timerAmount = 0;
    int resumeTimer = 0;
    public Mob myMob;

    public MobTimerHandler(Mob myParent)
    {
        myMob = myParent;
    }

    public void Update()
    {
        if (!paused)
        {
            for (int i = 0; i < timerAmount; i++)
            {
                timer[i].Update();
            }
        }
        if (resumeTimer > 0)
        {
            resumeTimer--;
            if (resumeTimer <= 0)
            {
                Resume();
            }
        }
    }

    public void AddTimer(BossTimer _timer)
    {
        timer[timerAmount++] = _timer;
    }

    public BossTimer AddTimer(EnemySpellInfo _spellInfo, bool _loop)
    {
        int _timer = _spellInfo.cooldown + Random.Range(0, 30);
        timer[timerAmount] = new BossTimer(timerAmount, _spellInfo, _timer, _loop, this);
        timerAmount++;
        return timer[timerAmount - 1];
    }

    public void CastSpell(int _id)
    {
        myMob.spellQueue.AddSpell(timer[_id].spell);
    }

    public void Pause(int _timer)
    {
        paused = true;
        resumeTimer = _timer;
    }

    public void Resume()
    {
        paused = false;
    }

    public void Clear()
    {
        for (int i = 0; i < timerAmount; i++)
        {
            timer[i] = null;            
        }
        timerAmount = 0;
    }
}
