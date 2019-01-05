using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCore
{
    // Singleton
    static private GameCore core;

    public SpellRepository spellRepository;
    public ItemRepository itemRepository;
    public SpellCastHandler spellCastHandler;
    public TroopsHandler troopsHandler;
    public Account chosenAccount;
    public Adventure chosenAdventure;
    public EnemySpellInfoHandler espellHandler;
    public InitHandler initHandler;

    //public SpellQueue spellQueue;

    private GameCore()
    {
        spellRepository = new SpellRepository(this);
        itemRepository = new ItemRepository();
        spellCastHandler = new SpellCastHandler();
        troopsHandler = new TroopsHandler();
        chosenAccount = new Account();
        chosenAccount.LoadAccData();
        chosenAdventure = new Adventure(-1);
        espellHandler = new EnemySpellInfoHandler();
        initHandler = new InitHandler();
        //spellQueue = new SpellQueue();
    }


    private void ClearCore()
    {
        tempRecount = null; // do przemiany na expa
        tempExpGained = 0; // zdobyty exp
        actualEntry = 0;
        overhealingPenaltyDone = false;
        expTranferDone = false;
        GameFinished = false;
        endingTimer = 600;
        //spellQueue.Clear();
        isVictorious = false;
        itemReceived = false;
        bsAlpha = 0f;
        beaconCount = 0;
        //myEnemy.ClearTimers();
    }

    static public GameCore Core
    {
        get
        {
            if (core == null)
                core = new GameCore();
            return core;
        }
    }
    // END Singleton

    static public CHAMPION chosenChampion = CHAMPION.PALADIN;
    static public ENC chosenBoss = 0;
    static public int difficulty = (int)DIFF.CASUAL;
    static public Quaternion zero = Quaternion.Euler(0, 0, 0);

    ////////////////////////
    // System spelli      //
    ////////////////////////

    public int spellsAmount = 5;
    public int actualSpell = 0; // indeks wybranego spella
    public Spell[] PlayerSpell;
    public Recount recount;
    public bool isCasting = false;
    public Spell castedSpell;
    public BuffHandler buffSystem;
    public GameObject frame;
    
    public GameObject mySpellIcon;
    

    public bool isInteruptOnCD = false;
    private int interuptCD = 480;
    public GameObject interuptIcon;
    public GameObject interuptText;

    public bool isDispelOnCD = false;
    private int DispelCD = 480;
    public GameObject DispelIcon;
    public GameObject DispelText;

    public CombatItem[] combatItem = new CombatItem[3];
    public Image[] combatItemIcon = new Image[3];

    public PaladinSparkHandler paladinSparkHandler;

    public void RefreshItemIcons()
    {
        for (int i=0; i<3; i++)
        {
            if (combatItem[i] != null)
            combatItemIcon[i].sprite = Resources.Load<Sprite>("Items/CombatItems/"+combatItem[i].icon);
            else
                combatItemIcon[i].sprite = Resources.Load<Sprite>("NULL");
        }
    }

    private void UseCombatItem(int _which)
    {
        if (combatItem[_which] != null)
        {
            combatItem[_which].TriggerEffect();
            combatItem[_which] = null;
            RefreshItemIcons();
        }
    }

    // reagowanie na QWER
    private void PC_Picking()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if (Input.GetKeyDown(KeyCode.Q))
            PickSpell(0);
        if (Input.GetKeyDown(KeyCode.W))
            PickSpell(1);
        if (Input.GetKeyDown(KeyCode.E))
            PickSpell(2);
        if (Input.GetKeyDown(KeyCode.R))
            PickSpell(3);
        if (Input.GetKeyDown(KeyCode.F))
            PickSpell(4);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseCombatItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseCombatItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UseCombatItem(2);
    }

    private void PickSpell(int _no)
    {
        actualSpell = _no;

        if (PlayerSpell[actualSpell].spellInfo.spellType == SPELL_TYPE.AOE_INDICATOR) // TESTING
            InitIndicatorSpell();
        else
            ResetIndicatorSpell();

        RefreshPickedSpellFrame();
    }

    const string UISLOT = "spellSlot";
    const string UISLOT_ACTIVE = "spellSlotPicked";
    GameObject myring = null;
    public void RefreshPickedSpellFrame()
    {
        if (myring != null)
            myring.GetComponent<Image>().sprite = Resources.Load<Sprite>(UISLOT);
        myring = GameObject.Find("Temp" + (actualSpell + 1)).gameObject;
        myring.GetComponent<Image>().sprite = Resources.Load<Sprite>(UISLOT_ACTIVE);
    }

    public Spell FindSpell(SPELL _spell)
    {
        for (int i = 0; i < spellsAmount; i++)
        {
            if (PlayerSpell[i].ID == _spell)
                return PlayerSpell[i];
        }
        return null;
    }

    // AURAS

    public void ApplyAuras()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerSpell[i].ApplyAuras();
        }
    }

    // UPDATED STATS

    public int criticalStrikeChance = 0;

    public void UpdateStats()
    {
        criticalStrikeChance = chosenAccount.statFCS;

        if (GameCore.chosenChampion == CHAMPION.PALADIN)
        criticalStrikeChance += myCaster.AuraStacks(AURA.DIVINITY) * VALUES.DIVINITY_CRIT_INCREASE;
    }

    // PALADIN UNIQUE

    public Soldier[] beaconedTarget = new Soldier[5];
    public int beaconCount = 0;

    public void AddBeaconedTarget(Soldier sol)
    {
        beaconedTarget[beaconCount] = sol;
        beaconCount++;
    }

    public void RemoveBeaconedTarget(Soldier _soldier)
    {
        int id = FindBeaconedSoldierID(_soldier.myID);
        if (id > -1)
        {
            beaconedTarget[id] = null;
            SortBeaconedTargets();
        }
    }

    public int FindBeaconedSoldierID(int id)
    {
        for (int i = 0; i < beaconCount; i++)
        {
            if (beaconedTarget[i].myID == id)
            {
                return i;
            }
        }
        return -1;
    }

    public void SortBeaconedTargets()
    {
        for (int i=0; i<beaconCount; i++)
        {
            if (beaconedTarget[i] == null)
            {
                if (i + 1 < beaconCount)
                {
                    beaconedTarget[i] = beaconedTarget[i + 1];
                    beaconedTarget[i + 1] = null;
                }
                else
                    beaconCount--;
            }
        }
    }

    //
    public Encounter myEnemy;
    public GameObject myManaBar;
    public GameObject bossEmote;
    public GameObject myPortrait;
    public GameObject myHealingDoneText;
    public Soldier ActualSoldier;

    public bool GameFinished = false;


    // ************

    public bool isVictorious;

    // --- Metoda wywolywana przy zwyciestwie, zmniejsza do nicosci portret przeciwnika, mowi ze gra jest zakonczona
    public void Victory()
    {
        if (!GameFinished)
        {
            tempRecount = recount;
            //myBoss.transform.localScale = new Vector3(0, 0, 0);
            myBlackScreen.transform.SetSiblingIndex(myBlackScreen.transform.parent.childCount - 1);
            GameFinished = true;
            isVictorious = true;
            itemReceived = false;
        }
    }

    public GameObject myBlackScreen;
    public float bsAlpha = 0f;
    private bool itemReceived;

    public void GameEnding()
    {
        bsAlpha += 0.01f;
        myBlackScreen.GetComponent<Image>().color = new Color(0, 0, 0, bsAlpha);
        if (bsAlpha >= 0.5f)
        {
            tempExpGained = 0; // zdobyty exp
            actualEntry = 0;
            overhealingPenaltyDone = false;
            expTranferDone = false;
            itemReceived = true;
        }
    }

    public GameObject lootSprite;
    public Item loot;
    public Tooltip lootTooltip;
    public float tipScale = 0;

    /*private void ReceiveReward()
    {
        string _spaces = System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;
        loot = GameCore.Core.chosenAccount.myInventory.AddItem(Random.Range(0,3), GameCore.difficulty);
        lootTooltip = new Tooltip(-2, 2, _spaces + loot.GetDescription(), true, 2f);
        lootSprite = Object.Instantiate(Resources.Load("SpriteObject"), new Vector3(0, 1.5f, -9.5f), zero) as GameObject;
        lootSprite.GetComponent<SpriteRenderer>().sprite = loot.mySprite;
        lootTooltip.myBack.transform.localScale = new Vector3(0, 0);
        lootTooltip.myTip.transform.localScale = new Vector3(0, 0);
        lootSprite.transform.localScale = new Vector3(0, 0);
    }*/

    GameObject indicator;
    private void InitIndicatorSpell()
    {
        ResetIndicatorSpell();
        indicator = GameObject.Instantiate(Resources.Load("areaIndicatorObj")) as GameObject;
        indicator.transform.parent = GameObject.Find("Canvas").transform;
        indicator.transform.localScale = new Vector3(1, 1, 1);
        indicator.GetComponent<scrAreaIndicator>().mySpell = PlayerSpell[actualSpell];
    }

    private void ResetIndicatorSpell()
    {
        if (indicator != null)
        {
            indicator.GetComponent<scrAreaIndicator>().Clear();
            GameObject.Destroy(indicator);
        }
    }

    // --- Metoda wywolywana przy porazce, mowi ze gra jest zakonczona
    public void Lose()
    {
        GameFinished = true;
        isVictorious = false;
    }

    // --- Metoda ktora wypelnia dwie tablice: tablice zycia graczy (SoldierHealth) oraz tablice graczy (SoldierOrder)


    private float GetManaCost(Spell _spell)
    {
        float _manacost;
        _manacost = _spell.manacost;

        CasterBuff myb = buffSystem.FindBuff(CASTERBUFF.BOOK_OF_PRIME_SHADOWS);
        if (myb != null)
        {
            _manacost = _manacost + _manacost * myb.stacks * 0.01f;
        }

        return _manacost;
    }

    private bool ConsumeCasterBuff(CASTERBUFF myb)
    {
        CasterBuff temp = buffSystem.FindBuff(myb);
        if (temp != null)
        {
            temp.Remove();
            return true;
        }
        return false;
    }

    

    public TalentTree myTree;
    public Caster myCaster;
    // --- Metoda ktora wywoluje rzucenie spella na ActualSoldier


    public static int[] GetAoETargets(int dummy)
    {
        int[] mytab = new int[8];
        int groupX = dummy / 4;
        int groupY = dummy % 4;

        for (int i = 0; i < 8; i++)
            mytab[i] = -1;

        if (groupY > 0)
        {
            if (groupX > 0)
                mytab[0] = (groupY - 1) + ((groupX - 1) * 4);
            mytab[1] = (groupY - 1) + ((groupX) * 4);
            if (groupX < 3)
                mytab[2] = (groupY - 1) + ((groupX + 1) * 4);
        }

        if (groupX > 0)
            mytab[3] = (groupY) + ((groupX - 1) * 4);
        if (groupX < 3)
            mytab[4] = (groupY) + ((groupX + 1) * 4);

        if (groupY < 3)
        {
            if (groupX > 0)
                mytab[5] = (groupY + 1) + ((groupX - 1) * 4);
            mytab[6] = (groupY + 1) + ((groupX) * 4);
            mytab[7] = (groupY + 1) + ((groupX + 1) * 4);
        }

        return mytab;
    }

    public void CastAutoSpell(int _spelltype, Soldier _mytarget = null, int val = 0, int DUMMY = 0)
    {
        switch (_spelltype)
        {
            case (int)SPELL.MOONLIGHT:
                {
                    foreach (int _target in GetAoETargets(DUMMY))
                    {
                        if ((_target >= 0) && (_target < 16))
                        {
                            troopsHandler.soldier[_target].CastFinished(new Moonlight(), myCaster, val);
                        }
                    }
                }
                break;
            case (int)SPELL.SCROLL_OF_ALAPHI:
                {
                    Soldier[] tars = troopsHandler.GetTargets(TARGETTYPE.EVERYONE, 16).Soldier;
                    SpellEffect alaphi = new ScrollOfAlaphi();

                    for (int i = 0; i < 16; i++)
                    {
                        if (tars[i] != null)
                            alaphi.Execute(myCaster, tars[i]);
                        //tars[i].Heal(myCaster, spellRepository.Get(SPELL.SCROLL_OF_ALAPHI), HEALSOURCE.SCROLL_OF_ALAPHI, HEALTYPE.DIRECT_MULTI);
                    }
                }
                break;
        }
    }

    // --- Obsluga systemu Beacon Healingu
    /*
     * Beacon Healing - jedne spelle naznaczaja graczy dzieki czemu ci gracze otrzymuja X% healingu robionego za pomoca innych spelli
     * */

    public void BeaconHeal(int value, int spellid, Soldier _primaryTarget)
    {
        if (beaconCount > 0)
        {
            for (int i = 0; i < beaconCount; i++)
            {
                if (beaconedTarget[i] != _primaryTarget)
                    beaconedTarget[i].CastFinished(new WoKL_Healing(), myCaster, value);
            }
        }
    }

    // --- reakcja na krytyczne heale

    public void CriticalHealOccured()
    {
        if (GameCore.chosenChampion == CHAMPION.PALADIN)
        {
            if (myCaster.myAura[(int)AURA.VISIONS_OF_ANCIENT_KINGS].isActive)
            {
                buffSystem.BuffMe(CASTERBUFF.VISIONS_OF_ANCIENT_KINGS, 180f, myCaster);
            }
        }
    }

    // --- Metoda rzucajaca ofensywny spell na gracza/y
    //public void OrderESpellCast(EnemySpellInfo _espell)
    //{
        //spellQueue.AddSpell(_espell);
    //}

    public void CastHostileSpell(EnemySpellInfo _espell)
    {
        if (_espell.damagetype == DAMAGETYPE.SUMMON)
        {
            myEnemy.SpawnMob(_espell.summon);
        }
        else
        {
            troopsHandler.SortSoldiers();

            int tarAmount = _espell.targetsAmount;

            if (_espell.ID == HOSTILESPELL.BRUTAL_BITE)
                tarAmount += myEnemy.val1;
            if (_espell.ID == HOSTILESPELL.MIND_BOMB)
            {
                tarAmount += myEnemy.val1++;
            }
            if (_espell.ID == HOSTILESPELL.ROAR_OF_TERROR)
            {
                //myEnemy.Pause(180);
                //spellQueue.Pause(180);
            }

            Soldier[] excluded = new Soldier[16];

            if (_espell.misdirectable)
            {
                Soldier[] tank = troopsHandler.getTanks();
                int _amount = Mathf.Min(tarAmount, troopsHandler.tanksAmount);

                tarAmount -= _amount;

                for (int i = 0; i < _amount; i++)
                {
                    tank[i].HostileCastFinished(_espell);
                    excluded[i] = tank[i];
                }
            }

            if (tarAmount > 0)
            {
                int realTargetsAmount = 0;

                Soldier[] targets = new Soldier[tarAmount];

                if (_espell.misdirectable)
                    targets = troopsHandler.GetTargets(_espell.targetType, tarAmount, excluded).Soldier;
                else
                    targets = troopsHandler.GetTargets(_espell.targetType, tarAmount).Soldier;

                for (int i = 0; i < tarAmount; i++)
                {
                    if (targets[i] != null)
                        realTargetsAmount += 1;
                }

                for (int i = 0; i < tarAmount; i++)
                {
                    if (targets[i] != null)
                    {
                        if (!targets[i].isDead)
                            targets[i].HostileCastFinished(_espell, realTargetsAmount);
                    }
                }
            }
        }
    }

    private void ClearScene()
    {
       // for (int i = 0; i < troopsHandler.SoldierAmount; i++)
       // {
            troopsHandler.ClearSoldiers();
      //  }
    }

    //////

    Recount tempRecount; // do przemiany na expa
    int tempExpGained; // zdobyty exp
    int actualEntry = 0;
    bool overhealingPenaltyDone = false;
    bool expTranferDone = false;
    Tooltip myTip = null;
    int endingTimer = 600;
    int[] conversionValue = new int[10];
    int conversionValue2 = 1;

    private void ExperienceTransfer()
    {
        if (myTip == null)
        {
            myTip = Tooltip.Show("", 0, new Vector3(0,0,0), false);
            for (int i = 0; i < tempRecount.amount; i++)
            {
                conversionValue[i] = Mathf.Max(8, Mathf.Min(tempRecount.entry[i].healing, tempRecount.entry[i].overhealing / 3) / 120);
            }
            ClearScene();
        }
        myTip.UpdateContent(tempRecount.GetRecount(true) + "\n\n Experience gained: " + tempExpGained + "\n\nLevel: " + chosenAccount.level + "\n" + chosenAccount.GetExpString() + chosenAccount.tempLevelUp);
        if (!overhealingPenaltyDone) // zmniejszamy healing o nasz overhealing
        {
            int doneamount = 0;

            for (int i = 0; i < tempRecount.amount; i++)
            {
                if (tempRecount.entry[i].overhealing > 0)
                {
                    if (tempRecount.entry[i].healing > 0)
                    {
                        tempRecount.entry[i].healing = Mathf.Max(tempRecount.entry[i].healing - conversionValue[i], 0);
                        tempRecount.entry[i].overhealing = Mathf.Max(tempRecount.entry[i].overhealing - conversionValue[i] * 3, 0);
                    }
                    else
                        tempRecount.entry[i].overhealing = 0;
                }
                else
                    doneamount++;
            }

            if (doneamount >= tempRecount.amount)
            {
                overhealingPenaltyDone = true;
                conversionValue2 = Mathf.Max(8, tempRecount.entry[actualEntry].healing / 120);
            }
        }
        else
            if (!expTranferDone)
        {
            if (actualEntry < tempRecount.amount)
            {
                if (tempRecount.entry[actualEntry].healing > 0)
                {
                    tempRecount.entry[actualEntry].healing = Mathf.Max(tempRecount.entry[actualEntry].healing - conversionValue2, 0);
                    tempExpGained += conversionValue2;
                    chosenAccount.Exp += conversionValue2;
                }
                else
                {
                    actualEntry++;
                    if (actualEntry < tempRecount.amount)
                        conversionValue2 = Mathf.Max(8, tempRecount.entry[actualEntry].healing / 120);
                }
            }
            else
                expTranferDone = true;
        }
    }

    public GameObject _mytip;

    public SpellInfo[] spellInfo = new SpellInfo[20];

    public void SetTarget(Mob _target)
    {
        troopsHandler.SetTarget(_target);
        myEnemy.ClearTargets();
        _target.SetAsTarget();
    }

    private void InteruptUpdate()
    {
        if (isInteruptOnCD)
        {
            interuptCD--;
            if (interuptCD <= 0)
            {
                interuptCD = 480;
                isInteruptOnCD = false;
            }
        }

        if (isInteruptOnCD)
        {
            interuptIcon.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)interuptCD / 480f;
            interuptText.GetComponent<Text>().text = (interuptCD / 60).ToString();
        }
        else
        {
            interuptIcon.transform.GetChild(1).GetComponent<Image>().fillAmount = 0;
            interuptText.GetComponent<Text>().text = "";
        }
    }

    private void DispelUpdate()
    {
        if (isDispelOnCD)
        {
            DispelCD--;
            if (DispelCD <= 0)
            {
                DispelCD = 480;
                isDispelOnCD = false;
            }
        }

        if (isDispelOnCD)
        {
            DispelIcon.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)DispelCD / 480f;
            DispelText.GetComponent<Text>().text = (DispelCD / 60).ToString();
        }
        else
        {
            DispelIcon.transform.GetChild(1).GetComponent<Image>().fillAmount = 0;
            DispelText.GetComponent<Text>().text = "";
        }
    }

    public void Update()
    {
        if (!ScenesController.SControl.isLocked)
        {
            for (int i = 0; i < 5; i++)
                PlayerSpell[i].Update();

            InteruptUpdate();
            DispelUpdate();

            PC_Picking();

            buffSystem.SortBuffs();
            buffSystem.ActivateBuffs();

            if (!GameFinished)
            {
                spellCastHandler.Update();
            }
            
            myCaster.ManaRegeneration();

            if ((troopsHandler.SoldierAmount <= 0) && (!GameFinished))
            {
                Lose();
            }

            if (GameFinished)
            {
                if (isVictorious)
                {
                    if (!itemReceived)
                        GameEnding();
                    else
                    {
                        ExperienceTransfer();
                    }

                    if (expTranferDone)
                    {
                        if (endingTimer-- <= 0)
                        {
                            chosenAdventure.progress++;

                            ClearCore();
                            chosenAccount.SaveAccData();
                            if (chosenAdventure.progress >= WorldMapCore.WMCore.adventureInfoHandler.adventureInfo[chosenAdventure.ID].encountersInfo.amount)
                            {
                                chosenAccount.myProgress.missionComplete[chosenAdventure.ID] = true;
                                ScenesController.SControl.SwitchScene("AdventureScene");
                            }
                            else
                                ScenesController.SControl.SwitchScene("DescendantScene");
                        }
                    }
                }
                else
                {
                    GameEnding();
                    if (!itemReceived)
                    {
                        ClearCore();
                        ScenesController.SControl.SwitchScene("DescendantScene");
                    }
                }
            }
            else
            {
                myEnemy.Update();
                _mytip.transform.GetChild(0).GetComponent<Text>().text = recount.GetRecount();
            }
        }
    }
}
