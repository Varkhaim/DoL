using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spell
{
    // ------------
    // STATYCZNE //
    // ------------

    public static Spell[] GenerateSpellKit(CHAMPION classID, Caster _mycaster = null)
    {
        SPELL[] _myIDs = new SPELL[5];
        Spell[] _mykit = new Spell[5];      

        switch (classID)
        {
            case CHAMPION.PALADIN:
                {
                    _myIDs[0] = SPELL.WORD_OF_KINGS_LIGHT;
                    _myIDs[1] = SPELL.WORD_OF_KINGS_COURAGE;
                    _myIDs[2] = SPELL.WORD_OF_KINGS_FAITH;
                    _myIDs[3] = SPELL.WORD_OF_KINGS_LOYALTY;
                    _myIDs[4] = SPELL.DIVINE_INTERVENTION;
                }
                break;
            case CHAMPION.SHADOWMANCER:
                {
                    _myIDs[0] = SPELL.SOOTHING_VOID;
                    _myIDs[1] = SPELL.SHADOWSURGE;
                    _myIDs[2] = SPELL.TWILIGHT_BEAM;
                    _myIDs[3] = SPELL.SHADOWSONG;
                    _myIDs[4] = SPELL.SACRIFICE;
                }
                break;
        }

        for (int i = 0; i < 5; i++)
        {
            _mykit[i] = new Spell(_myIDs[i]);
        }

        // , Spell.GetCooldown(_myIDs[i], _mycaster), Spell.GetIcon(_myIDs[i]), Spell.GetName(_myIDs[i]), Spell.GetManaCost(_myIDs[i])

        return _mykit;
    }

    public static void CreateEffect(GameObject _effect, Soldier _Soldier)
    {
        Object.Instantiate(_effect, _Soldier.frame.transform.position + new Vector3(0.75f, -0.25f, 0), _Soldier.frame.transform.rotation);
    }

    /*public static int GetSpellID(string name)
    {
        switch (name)
        {
            case "Word of Kings: Light": return (int)SPELL.WORD_OF_KINGS_LIGHT;
            case "Word of Kings: Courage": return (int)SPELL.WORD_OF_KINGS_COURAGE;
            case "Word of Kings: Faith": return (int)SPELL.WORD_OF_KINGS_FAITH;
            case "Word of Kings: Loyalty": return (int)SPELL.WORD_OF_KINGS_LOYALTY;
            case "Divine Interveniton": return (int)SPELL.DIVINE_INTERVENTION;
            case "WoKLoyaltyHealing": return (int)SPELL.WORD_OF_KINGS_LOYALTY_HEALING;

            case "Soothing Void": return (int)SPELL.SOOTHING_VOID;
            case "Shadowsurge": return (int)SPELL.SHADOWSURGE;
            case "Twilight Beam": return (int)SPELL.TWILIGHT_BEAM;
            case "Shadowsong": return (int)SPELL.SHADOWSONG;
            case "Sacrifice": return (int)SPELL.SACRIFICE;
            case "SoothingVoidJump": return (int)SPELL.SOOTHING_VOID_JUMP;

            default: return 0;
        }
    }

    public static SpellEffect GetSpellEffectFromID(int _ID)
    {
        switch (_ID)
        {
            case (int)SPELL.WORD_OF_KINGS_LIGHT: return new WordOfKingsLight();
            case (int)SPELL.WORD_OF_KINGS_COURAGE: return new WordOfKingsCourage();
            case (int)SPELL.WORD_OF_KINGS_FAITH: return new WordOfKingsFaith();
            case (int)SPELL.WORD_OF_KINGS_LOYALTY: return new WordOfKingsLoyalty();
            case (int)SPELL.WORD_OF_KINGS_LOYALTY_HEALING: return new WoKL_Healing();
            case (int)SPELL.DIVINE_INTERVENTION: return new DivineIntervention();

            case (int)SPELL.SOOTHING_VOID: return new SoothingVoid();
            case (int)SPELL.SHADOWSURGE: return new Shadowsurge();
            case (int)SPELL.TWILIGHT_BEAM: return new TwilightBeam();
            case (int)SPELL.SHADOWSONG: return new Shadowsong();
            case (int)SPELL.SACRIFICE: return new Sacrifice();
            default: return new WordOfKingsLight();
        }
    }*/

    public static void Cast(SpellEffect _meffect, Soldier _target, Caster _caster, int val=0)
    {
        if (!_target.isDead)
        _meffect.Execute(_caster, _target, val);
    }

    // KONIEC STATYCZNYCH

    public SPELL ID;
    private float cooldown;
    private float maxCooldown;
    public string name;
    public float manacost;
    public Sprite iconSprite;
    public GameObject myIcon;
    public GameObject myChargeCounter;
    public Caster myCaster;
    public int charges = 1;
    public int maxCharges = 1;
    public int chargeLoad = 1; // ilosc ladunkow co cd
    public int castTime;

    public SpellEffect mEffect;
    public SpellInfo spellInfo;

    public int baseValue; // bazowa wartosc
    public float coeff = 1f; // przelicznik z Mocy na efektywnosc

    // , float _maxCooldown, Sprite _iconSprite, string _name, float _manacost
    public Spell(SPELL _ID)
    {
        ID = _ID;
        spellInfo = GameCore.Core.spellRepository.Get(_ID);
        maxCooldown = spellInfo.cooldown;
        cooldown = maxCooldown;
        iconSprite = Resources.Load<Sprite>(spellInfo.icon);
        name = spellInfo.name;
        manacost = spellInfo.manaCost;
        myIcon = null;
        mEffect = GameCore.Core.spellRepository.Get(ID).effect;
        maxCharges = spellInfo.charges;
        charges = maxCharges;
        chargeLoad = spellInfo.chargesGain;
        castTime = spellInfo.castTime;
    }

    public void Update()
    {
        GameObject _myt = myIcon.transform.GetChild(0).GetComponent<SpellScript>().mytext;

        if (cooldown > 0)
        {
            if (charges < maxCharges)
                cooldown = Mathf.Max(0, --cooldown);
        }
        else
        {
            if (charges < maxCharges)
            {
                cooldown = maxCooldown;
                charges = Mathf.Min(maxCharges, charges+chargeLoad);
                if (charges>0)
                    myIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        if (maxCooldown > 0)
        {
            if (charges == 0)
                myIcon.transform.GetChild(0).GetComponent<SpellScript>().myShadow.fillAmount = cooldown / maxCooldown;
            else
                myIcon.transform.GetChild(0).GetComponent<SpellScript>().myShadow.fillAmount = 0;
        }

        if (maxCharges > 1)
            myChargeCounter.GetComponent<Text>().text = charges.ToString();

        if (myIcon)
        {
            if (myIcon.transform.GetChild(0).GetComponent<SpellScript>().mytext != null)
            {
                if (charges >= maxCharges)
                {
                    _myt.GetComponent<Text>().text = "";
                }
                else
                {
                    string mytext;
                    mytext = Mathf.Round(cooldown / 60f).ToString();
                    _myt.GetComponent<Text>().text = mytext;
                }
            }
            
        }
    }

    public void ChangeCooldown(float value)
    {
        if (value == 0)
        {
            if (charges < maxCharges)
            {
                cooldown = 0;
                myIcon.GetComponent<scrSpellButton>().Animate(2f);
            }
        }
        else
        {
            if (charges < maxCharges)
            {
                cooldown = Mathf.Max(0, cooldown + value);
                myIcon.GetComponent<scrSpellButton>().Animate(1.5f);
            }
            //myIcon.GetComponent<SpellScript>().mytext.GetComponent<CooldownTextScript>().Animate();
        }
    }

    public void ApplyAuras()
    {
        if (ID == SPELL.WORD_OF_KINGS_COURAGE)
        {
            maxCooldown -= GameCore.Core.myCaster.AuraStacks(AURA.GENEROUSITY) * VALUES.GENEROUSITY_VALUE;
        }
    }

    public void SetCooldown()
    {
        charges--;
        //if (charges <= 0)
        //    myIcon.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1f);
    }

    public bool isOnCooldown()
    {
        return (charges <= 0);
    }
}

