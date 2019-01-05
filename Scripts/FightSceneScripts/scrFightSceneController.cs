using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrFightSceneController : MonoBehaviour
{
    public float basex, basey, x_spacing, y_spacing;
    private GameCore core = GameCore.Core;
    private GameObject myTip = null;
    private float volume = 0f;
    private bool fresh = true;

    void Start()
    {
        InitSoldierUnitFrames();
        core.initHandler.InitMainScene(core);
    }

    private void InitSoldierUnitFrames()
    {
        core.troopsHandler.InitSoldiers();
        for (int i = 0; i < 16; i++)
        {
            int j = i / 4;
            GameObject _frame;
            Vector3 myVec = new Vector3(basex + j * x_spacing, basey - ((i * y_spacing) - j * (4 * y_spacing)));            
            _frame = Instantiate(Resources.Load("UnitFrame"), myVec, Quaternion.Euler(0, 0, 0)) as GameObject;
            _frame.transform.SetParent(GameObject.Find("Canvas").transform);
            _frame.transform.localScale = new Vector3(1, 1, 1);
            _frame.transform.position = new Vector3(_frame.transform.position.x, _frame.transform.position.y, 0);
            core.troopsHandler.soldier[i].frame = _frame;
            _frame.GetComponent<scrUnitPanel>().soldier = core.troopsHandler.soldier[i];
            _frame.GetComponent<scrUnitPanel>().RefreshData();
            //core.troopsHandler.basePosition[i] = _frame.transform.position;
        }
    }

    public void MoveRow(int _row)
    {
        core.troopsHandler.MoveRow(_row);
    }

    public void MoveCol(int _col)
    {
        core.troopsHandler.MoveCol(_col);
    }

    void Update()
    {
        core.Update();

        if (fresh)
        {
            if (core.GameFinished)
            {
                volume = Mathf.Max(volume - 0.003f, 0f);
                if (volume <= 0f)
                    fresh = false;
            }
            else
                volume = Mathf.Min(volume + 0.001f, 0.2f);

            GetComponent<AudioSource>().volume = volume;
        }
    }

    /*
    public void ShowTooltip()
    {
        if (core.spellQueue.castingSpell != null)
        {
            if (myTip == null)
            {

                myTip = Tooltip.Create(core.spellQueue.castingSpell.description);
            }
            else
            {
                myTip.GetComponent<Image>().color = Color.white;
                myTip.transform.GetChild(0).GetComponent<Text>().text = core.spellQueue.castingSpell.description;
            }
        }
    }
    */

    public void HideTooltip()
    {
        if (myTip != null)
        {
            myTip.GetComponent<Image>().color = Color.clear;
            myTip.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }
}
