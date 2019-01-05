using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class scrUnitPanel : MonoBehaviour
{

    public Soldier soldier;
    private GameCore core = GameCore.Core;

    private Transform prvName;
    public Transform myName
    {
        get
        {
            if (prvName == null)
                prvName = transform.GetChild(5);
            return prvName;
        }
        set
        {
            prvName = value;
        }
    }

    private Transform prvAbsorbBar;
    public Transform myAbsorbBar
    {
        get
        {
            if (prvAbsorbBar == null)
                prvAbsorbBar = transform.GetChild(3);
            return prvAbsorbBar;
        }
        set
        {
            prvAbsorbBar = value;
        }
    }

    private Transform prvHealthBar;
    public Transform myHealthBar
    {
        get
        {
            if (prvHealthBar == null)
                prvHealthBar = transform.GetChild(1);
            return prvHealthBar;
        }
        set
        {
            prvHealthBar = value;
        }
    }

    private Image prvDecayBar;
    public Image myDecayBar
    {
        get
        {
            if (prvDecayBar == null)
                prvDecayBar = transform.GetChild(2).GetComponent<Image>();
            return prvDecayBar;
        }
        set
        {
            prvDecayBar = value;
        }
    }

    void Start()
    {
        //EventTrigger trigger = GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerDown;
        //entry.callback.AddListener((data) => { Clicked((PointerEventData)data); });
        //trigger.triggers.Add(entry);
        baseX = transform.position.x;
        baseY = transform.position.y;
        RefreshHealthInfo();
    }

    public void RefreshData()
    {
        myName.GetComponent<Text>().text = soldier.myName;
        transform.GetChild(6).GetComponent<Image>().sprite = soldier.GetIcon();

    }

    public void Clicked()
    {
        Soldier[] temp = new Soldier[1];
        temp[0] = soldier;
        core.spellCastHandler.PrepareSpell(temp, core.PlayerSpell[core.actualSpell], core.myCaster);
    }

    public void RefreshHealthInfo()
    {
        myHealthBar.GetComponent<Image>().fillAmount = soldier.GetPercentHealth();
        myAbsorbBar.GetComponent<Image>().fillAmount = soldier.GetPercentShield();
        myDecayBar.GetComponent<Image>().color = soldier.GetHealthColor();
        //myHealthBar.GetChild(1).GetComponent<Text>().text = Mathf.Round((soldier.GetPercentHealth() * 100f)).ToString() + "%";
    }

    void Update()
    {
        soldier.Update();

        if (phase > -1)
            DyingAnimation();

        if (myDecayBar.fillAmount > soldier.GetPercentHealth())
            myDecayBar.fillAmount -= 0.01f;
        else
            myDecayBar.fillAmount = soldier.GetPercentHealth();
    }

    // "death" animation
    public int phase = -1;
    private float alpha = 1f;

    float baseX;
    float baseY;

    public void Kill()
    {
        phase = 0;
    }

    void DyingAnimation()
    {
        if (alpha > 0.5f)
        {
            GetComponent<RawImage>().color = new Color(1f, 1f, 1f, alpha);
            for (int i=0; i<6; i++)
            {
                if (i == 5)
                    transform.GetChild(i).GetComponent<Text>().color = new Color(1f, 1f, 1f, alpha);
                else
                    transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
            }
            alpha -= 0.05f;
        }
    }

}