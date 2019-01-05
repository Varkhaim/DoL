using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Mob
{    
    public Sprite myPortrait; // portret

    public GameCore core = GameCore.Core;

    public SpellQueue spellQueue;

    public GameObject mobGO;

    public Encounter myEncounter;

    public int phase = 0;
    public int ID;
    public float maxHealth;
    public float currHealth;
    public float HealthPercentage = 0;
    public bool isAlive = true;
    protected MobTimerHandler timerHandler;
    public MOBCLASS mobClass = MOBCLASS.NORMAL;

    public Timer[] myTimer = new Timer[10];
    public BossTalk myBossTalk;
    public GameObject progressBar;

    public EnemySpellInfo[] mySpell = new EnemySpellInfo[10];
    public int spellsAmount = 0;
    public MOB mobType = MOB.NUL;

    public int val1 = 0;

    public string name = "NULL";

    public static Mob SpawnMob(MOB _mobID)
    {
        Mob _spawned = null;
        switch (_mobID)
        {
            case MOB.SHADOWBEAST: _spawned = new ShadowbeastMOB(); break;
            case MOB.SOOL: _spawned = new SoolTheWarlockMOB(); break;
            case MOB.UMBARR: _spawned = new VoidmistressUmbarrMOB(); break;
            case MOB.SHADESPAWNLING: _spawned = new ShadeSpawnlingMOB(); break;
        }

        _spawned.mobGO = GameObject.Instantiate(Resources.Load("EnemyCore")) as GameObject;
        _spawned.mobGO.transform.SetParent(GameObject.Find("Canvas").transform);
        _spawned.mobGO.transform.localScale = new Vector3(1, 1, 1);
        _spawned.mobGO.GetComponent<EnemyScript>().myMob = _spawned;
        _spawned.mobType = _mobID;
        //_spawned.mobGO.transform.GetChild(4).GetComponent<Image>().sprite = Mob.GetMobClassIcon(_spawned.mobClass);
        _spawned.mobGO.GetComponent<scrMobPanel>().myMob = _spawned;

        return _spawned;   
    } 

    static public Sprite LoadMobPortrait(MOB _mobID)
    {
        switch (_mobID)
        {
            case MOB.SHADOWBEAST: return Resources.Load<Sprite>("Portraits/ShadowforgeUmbarr/Shadowbeast");
            case MOB.SOOL: return Resources.Load<Sprite>("Portraits/ShadowforgeUmbarr/SoolTheWarlock");
            case MOB.UMBARR: return Resources.Load<Sprite>("Portraits/ShadowforgeUmbarr/VoidmistressUmbarr");
            case MOB.SHADESPAWNLING: return Resources.Load<Sprite>("Portraits/ShadowforgeUmbarr/spawnling");
            default: return null;
        }
    }

    public Sprite LoadPortrait()
    {
        return LoadMobPortrait(mobType);
    }

    public Mob()
    {
        spellQueue = new SpellQueue(this);
        timerHandler = new MobTimerHandler(this);        
    }

    public void AddSpell(HOSTILESPELL _spell)
    {
        mySpell[spellsAmount++] = core.espellHandler.espellInfo[(int)_spell];
    }

    public void Heal(int val)
    {
        currHealth = Mathf.Min(maxHealth, currHealth + val);
    }

    public void ClearTimers()
    {
        timerHandler.Clear();
    }

    public virtual void Update()
    {
        timerHandler.Update();
        spellQueue.Update();
        HealthPercentage = currHealth / maxHealth;
    }

    protected void CastSpell(EnemySpellInfo _spell)
    {
        if (!core.GameFinished)
        {
            spellQueue.AddSpell(_spell);
        }
    }

    public void SetAsTarget()
    {
        mobGO.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("MobSlotTargeted");
    }

    public void Pause(int _timer)
    {
        timerHandler.Pause(_timer);
    }

    public void TakeDamage(int val)
    {
        if (isAlive)
        {
            currHealth = Mathf.Max(0, currHealth - val);
            if (currHealth <= 0)
                Die();
        }
    }

    public void Interupt()
    {
        if (!core.isInteruptOnCD)
        {
            core.isInteruptOnCD = true;
            spellQueue.Interupt();
        }
    }

    public void Die()
    {
        isAlive = false;
        myEncounter.KillMob(ID);
    }

    public void Remove()
    {
        myEncounter.RemoveMob(ID);
    }

    public static Sprite GetMobClassIcon(MOBCLASS _class)
    {
        switch (_class)
        {
            case MOBCLASS.BOSS: return Resources.Load<Sprite>("Boss");
            case MOBCLASS.BIGBOSS: return Resources.Load<Sprite>("BigBoss");
            default: return Resources.Load<Sprite>("NULL");
        }
    }
}
