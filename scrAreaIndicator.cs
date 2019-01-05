using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrAreaIndicator : MonoBehaviour {

    Soldier[] targets = new Soldier[16];
    public Spell mySpell;
    int tarAmount = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        if (tarAmount > 0)
        {
            if (Input.GetMouseButtonDown(0))
                CastSpell();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SoldierFrame")
        {
            if (isTaken(collision.gameObject.GetComponent<scrUnitPanel>().soldier) == -1)
            {
                collision.gameObject.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("soldierFrameHighlight");
                targets[tarAmount++] = collision.gameObject.GetComponent<scrUnitPanel>().soldier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SoldierFrame")
        {
            int num = isTaken(collision.gameObject.GetComponent<scrUnitPanel>().soldier);
            if (num != -1)
            {

                targets[num].frame.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("soldierFrame");
                targets[num] = null;
                SortTargets();

            }
        }
    }

    private void SortTargets()
    {
        for (int i = 0; i < tarAmount; i++)
        {
            if (i + 1 < tarAmount)
            {
                if (targets[i] == null)
                {
                    Soldier temp = targets[i + 1];
                    targets[i] = temp;
                    targets[i + 1] = null;
                }
            }
            else
            {
                tarAmount--;
            }
        }
    }

    private int isTaken(Soldier obj)
    {
        for (int i=0; i<tarAmount; i++)
        {
            if (targets[i] == obj)
                return i;
        }
        return -1;
    }

    private void CastSpell()
    {
        GameCore.Core.spellCastHandler.PrepareSpell(targets, mySpell, GameCore.Core.myCaster);
    }

    /*
    private Soldier[] GetSoldiers(GameObject[] objs)
    {
        Soldier[] temp = new Soldier[16];
        for (int i=0; i<objs.Length; i++)
        {
            if (null != objs[i])
            temp[i] = objs[i].GetComponent<scrUnitPanel>().soldier;
        }
        return temp;
    }
    */

    public void Clear()
    {
        for (int i = 0; i < tarAmount; i++)
        {
            targets[i].frame.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("soldierFrame");
        }

                
    }
}
