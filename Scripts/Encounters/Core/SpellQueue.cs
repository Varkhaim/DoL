using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellQueue
{
    int spellAmount = 0;
    private EnemySpellInfo[] spell = new EnemySpellInfo[10];
    private EnemySpellInfo fillerSpell = null; // spell rzucany gdy boss nie castuje nic innego
    bool isCasting = false;
    public EnemySpellInfo castingSpell = null;

    int maxCast;
    int castProgress;
    bool isInterupted = false;
    int interuptTimer = 120;
    //GameObject MyIcon;
    GameObject MyCastBar;
    public Mob myMob;

    bool paused = false;
    int resumeTimer = 0;

    private GameObject myCastBar
    {
        get
        {
            if (MyCastBar == null)
                MyCastBar = myMob.mobGO.transform.GetChild(2).gameObject;
            return MyCastBar;
        }
    }

    /*
    private GameObject myIcon
    {
        get
        {
            if (MyIcon == null)
                MyIcon = GameObject.Find("ESpellIcon");
            return MyIcon;
        }
    }
    */

    public SpellQueue(Mob _mob)
    {
        myMob = _mob;
    }

    public void AddSpell(EnemySpellInfo _spell)
    {
        spell[spellAmount++] = _spell;
    }

    public void SetFillerSpell(EnemySpellInfo _spell)
    {
        fillerSpell = _spell;
    }

    public void Interupt()
    {
        if (isCasting)
        {
            if (castingSpell == spell[0])
            {
                spell[0] = null;
                SortSpells();
            }
            isCasting = false;
            isInterupted = true;
            myCastBar.transform.GetChild(0).GetComponent<Text>().text = "Interupted";
        }
    }

    public void Clear()
    {
        //MyIcon = null;
        MyCastBar = null;
        spellAmount = 0;
        fillerSpell = null;
        castingSpell = null;
        isCasting = false;
    }

    public void Update()
    {
        if (!paused)
        {
            if (isInterupted)
            {
                interuptTimer--;
                myCastBar.GetComponent<Image>().color = Color.red;
                myCastBar.GetComponent<Image>().fillAmount = (float)interuptTimer / 120f;
                if (interuptTimer<=0)
                {
                    isInterupted = false;
                    interuptTimer = 120;
                }
            }

            if (!isCasting)
            {
                if (spellAmount > 0)
                    castingSpell = spell[0];
                else
                {
                    if (fillerSpell != null)
                        castingSpell = fillerSpell;
                    else
                        castingSpell = null;
                }
            }

            if ((!isCasting) && (castingSpell != null) && (!isInterupted))
            {
                isCasting = true;
                castProgress = 0;
                maxCast = castingSpell.castTime;
                myCastBar.transform.GetChild(0).GetComponent<Text>().text = castingSpell.name;
                myCastBar.GetComponent<Image>().color = Color.white;
                //myIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(castingSpell.icon);
                //myIcon.GetComponent<Image>().color = Color.white;
            }
            else
                Casting();
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

    private void Casting()
    {
        if (isCasting)
        {
            if (castProgress < maxCast)
            {
                castProgress++;
                myCastBar.GetComponent<Image>().fillAmount = (float)castProgress / (float)maxCast;
            }
            else
            {
                SpellCast();
                myCastBar.GetComponent<Image>().fillAmount = 0;
                //myIcon.GetComponent<Image>().color = Color.clear;
            }
        }
    }

    public void SpellCast()
    {
        GameCore.Core.CastHostileSpell(castingSpell);
        if (castingSpell == spell[0])
        {
            spell[0] = null;
            SortSpells();
        }
        isCasting = false;
    }

    void SortSpells()
    {
        for (int i = 0; i < spellAmount; i++)
        {
            if (i+1 < spellAmount)
            spell[i] = spell[i + 1];
        }
        spellAmount--;
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
}
