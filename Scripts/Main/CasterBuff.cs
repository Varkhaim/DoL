using UnityEngine;
using System.Collections;

public class CasterBuff
{
    public CASTERBUFF ID;
    public float duration;
    public float maxDuration;
    public GameObject icon = null;
    public float multiplier = 1f;
    public GameObject myText;
    public GameCore myParent;
    public Caster myCaster;
    public int refreshCount = 0;
    public int stacks = 0;
    public int maxStacks = 999;

    public static Sprite GetCasterBuffResource(CASTERBUFF _buff)
    {
        switch (_buff)
        {
            case 0: return Resources.Load<Sprite>("CasterBuffsIcons/VisionsOfAncientKings");
            case CASTERBUFF.VISIONS_OF_ANCIENT_KINGS: return Resources.Load<Sprite>("TalentIcons/VisionsOfAncientKings");
            case CASTERBUFF.DIVINE_INTERVENTION: return Resources.Load<Sprite>("CasterBuffsIcons/DivineIntervention");
            case CASTERBUFF.BOOK_OF_PRIME_SHADOWS: return Resources.Load<Sprite>("CasterBuffsIcons/BookOfPrimeShadows");
            case CASTERBUFF.HAND_OF_LIGHT: return Resources.Load<Sprite>("TalentIcons/HandOfLight");
            case CASTERBUFF.CONSECRATION: return Resources.Load<Sprite>("TalentIcons/Consecration");
            case CASTERBUFF.FLASH_OF_FUTURE_FAITH: return Resources.Load<Sprite>("CasterBuffsIcons/FlashOfFutureFaith");
            case CASTERBUFF.FLASH_OF_FUTURE_LIGHT: return Resources.Load<Sprite>("CasterBuffsIcons/FlashOfFutureLight");
            default: return Resources.Load<Sprite>("CasterBuffsIcons/VisionsOfAncientKings");
        }
    }
    
    public CasterBuff(CASTERBUFF _ID, float _duration, Caster _caster)
    {
        ID = _ID;
        maxDuration = _duration;
        duration = maxDuration;
        myParent = GameCore.Core;
        myCaster = _caster;
    }

    public void Refresh(float _value)
    {
        // 0 - odswiez do pelnego czasu
        if (_value == 0)
        {
            duration = maxDuration;
            if (ID == CASTERBUFF.BOOK_OF_PRIME_SHADOWS)
                stacks++;
        }
        else
        {
            duration += _value;
        }
    }

    public void TakeStacks(int _amount)
    {
        stacks = Mathf.Max(0, stacks - _amount);
    }

    public void AddStacks(int _amount, bool whetherRefresh = true)
    {
        stacks = Mathf.Min(maxStacks, stacks + _amount);
        if (whetherRefresh)
            Refresh(0);
    }

    public void Remove()
    {
        duration = 0;
        Object.Destroy(myText);
        Object.Destroy(icon);
    }

    public void Update()
    {
        duration = Mathf.Max(0, --duration);
        if (myText != null)
        {
            if (ID == CASTERBUFF.BOOK_OF_PRIME_SHADOWS)
                myText.GetComponent<TextMesh>().text = stacks.ToString();
            else
                myText.GetComponent<TextMesh>().text = Mathf.Floor(duration / 60f).ToString();
        }

        if (duration <= 0)
        {
            Object.Destroy(myText);
            Object.Destroy(icon);
        }
    }

    public void Execute()
    {
        switch (ID)
        {
            case CASTERBUFF.VISIONS_OF_ANCIENT_KINGS:
                {

                }
                break;
            default: break;
        }
    }
}

