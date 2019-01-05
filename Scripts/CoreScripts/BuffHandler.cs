using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuffHandler
{
    public GameCore core;
    private Vector3 basePos;

    public BuffHandler(Vector3 _basePos)
    {
        basePos = _basePos;
        core = GameCore.Core;
    }

    public CasterBuff[] Buffs = new CasterBuff[15];
    private int BuffsAmount = 0;

    public void BuffMe(CASTERBUFF bufftype, float buffdur, Caster _caster, int startingStacks = 1)
    {
        CasterBuff myBuff = FindBuff(bufftype);

        if (myBuff == null)
        {
            Buffs[BuffsAmount] = new CasterBuff(bufftype, buffdur, _caster);
            Buffs[BuffsAmount].stacks = startingStacks;
            
            Buffs[BuffsAmount].icon = Object.Instantiate(Resources.Load("CasterBuffIcon"), basePos + new Vector3(0.25f * BuffsAmount, 0, 0), GameCore.zero) as GameObject;
            //Buffs[BuffsAmount].myText = Object.Instantiate(Resources.Load("BuffDurationText"), basePos + new Vector3(0.1f + 0.25f * BuffsAmount, -0.1f, -1), GameCore.zero) as GameObject;
            Buffs[BuffsAmount].icon.GetComponent<Image>().sprite = CasterBuff.GetCasterBuffResource(bufftype);
            Buffs[BuffsAmount].icon.transform.SetParent(GameObject.Find("Canvas").transform);
            Buffs[BuffsAmount].icon.transform.localScale = new Vector3(1, 1, 1);
            Buffs[BuffsAmount].icon.GetComponent<BuffScript>().myid = BuffsAmount;

            BuffsAmount++;
        }
        else
        {
            myBuff.Refresh(0);
        }
    }

    private void RepositionBuffsIcons()
    {
        for (int i = 0; i < BuffsAmount; i++)
        {
            if (Buffs[i].icon != null)
            {

                Buffs[i].icon.transform.position = basePos + new Vector3(0.3f - 0.3f * BuffsAmount + 0.6f * i, 1f, -0.5f);
                //Buffs[i].myText.transform.position = basePos + new Vector3(-1.2f + 0.6f * i, -1.2f, -1);
            }
        }
    }

    public void SortBuffs()
    {
        for (int i = 0; i < BuffsAmount; i++)
        {
            if (Buffs[i].duration <= 0)
            {
                if (i < BuffsAmount - 1)
                {
                    Buffs[i] = Buffs[i + 1];
                    Buffs[i + 1].Remove();
                }
                else
                {
                    BuffsAmount--;
                }
            }
        }
        if (BuffsAmount > 0)
            RepositionBuffsIcons();
    }

    public void ActivateBuffs()
    {
        if (BuffsAmount > 0)
        {
            for (int i = 0; i < BuffsAmount; i++)
            {
                ExecuteBuff(Buffs[i].ID, i);
            }
        }
    }

    public CasterBuff FindBuff(CASTERBUFF bufftype)
    {
        if (BuffsAmount > 0)
        {
            for (int i = 0; i < BuffsAmount; i++)
            {
                if (Buffs[i].ID == bufftype)
                {
                    return Buffs[i];
                }
            }
        }
        return null;
    }

    public void ExecuteBuff(CASTERBUFF bufftype, int buffid)
    {
        Buffs[buffid].Update();
    }
}
