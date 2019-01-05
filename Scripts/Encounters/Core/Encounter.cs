using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Encounter
{
    public Mob[] encMob = new Mob[5];
    private int mobAmount = 0;
    public MOB[] mobType = new MOB[5];

    public void isAlive()
    {
        bool result = true;
        for (int i=0; i<5; i++)
        {
            if (!encMob[i].isAlive)
            {
                result = false;
                break;
            }
        }
        finished = result;
    }

    public int difficulty;
    public int phase = 0;
    public GameCore core = GameCore.Core;

    public SpellQueue spellQueue;

    public bool finished = false;    

    public Timer[] myTimer = new Timer[10];
    public BossTalk myBossTalk;
    public GameObject progressBar;

    public EnemySpellInfo[] mySpell = new EnemySpellInfo[10];
    public int spellsAmount = 0;

    public int val1 = 0;

    public string name = "NULL";

    public static Encounter GetBossAI(ENC _encID)
    {
        switch (_encID)
        {
            case ENC.SHADOWBEAST: return new ShadowbeastEncounter();
            default: return null;
        }
    }

    public Encounter() 
    {
    }

    public void ClearTargets()
    {
        for (int i=0; i<mobAmount; i++)
        {
            encMob[i].mobGO.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("MobSlot");
        }
    }

    public Sprite GetMainMobPortrait()
    {
        return Mob.LoadMobPortrait(encMob[0].mobType);
    }

    /*
    public void AddSpell(HOSTILESPELL _spell)
    {
        mySpell[spellsAmount++] = core.espellHandler.espellInfo[(int)_spell];
    }
    */

    public virtual void Update()
    {
        for (int i=0; i<mobAmount; i++)
        {
            encMob[i].Update();
        }
    }
        
    /*
    protected void CastSpell(EnemySpellInfo _spell)
    {
        if (!core.GameFinished)
        {
            spellQueue.AddSpell(_spell);
        }
    }
    */

    public virtual void InitEncounter()
    {
        // metoda do tworzenia mobkow
    }

    private int uniqueID = 0;
    public void SpawnMob(MOB myMob)
    {
        if (mobAmount < 5)
        {
            encMob[mobAmount] = Mob.SpawnMob(myMob);
            encMob[mobAmount].myEncounter = this;
            encMob[mobAmount].ID = uniqueID++;
            mobAmount++;
            RepositionMobs();
        }
    }

    private void RepositionMobs()
    {
        float width = 2f;
        for (int i = 0; i < mobAmount; i++)
        {
            encMob[i].mobGO.transform.position = new Vector3(3.5f - width / 2f - (mobAmount * width / 2f) + i * width, 4.25f);
        }
    }

    public void KillMob(int _ID)
    {
        for (int i = 0; i < mobAmount; i++)
        {
            if (encMob[i].ID == _ID)
            {
                encMob[i].mobGO.GetComponent<scrMobPanel>().dying = true;
            }
        }
    }

    public void RemoveMob(int _ID)
    {
        for (int i = 0; i < mobAmount; i++)
        {
            if (encMob[i].ID == _ID)
            {
                GameObject.Destroy(encMob[i].mobGO);
                encMob[i] = null;
                SortMobs();
            }
        }
    }

    public void SortMobs()
    {
        for (int i = 0; i < mobAmount; i++)
        {
            if (i + 1 < mobAmount)
            {
                if (encMob[i] == null)
                {
                    Mob temp = encMob[i + 1];

                    encMob[i + 1] = null;
                    encMob[i] = temp;
                }
            }
            else
            {
                mobAmount--;                
                if (mobAmount <= 0)
                {
                    core.Victory();
                }
                else
                {
                    RepositionMobs();
                }
            }
        }
    }
}
