using UnityEngine;
using System.Collections;
using System.IO;

public class Account
{
    private int basestatINT = 10; // INTelligence - redukuje cooldown spelli o x%
    public int statINT = 10;

    private int basestatKNG = 10; // KNowledGe - zwieksza pule many o x%
    public int statKNG = 10;

    private int basestatWSD = 10; // WiSDom - zwieksza szanse na proce o x%
    public int statWSD = 10;

    private int basestatFCS = 10; // FoCuS - zwieksza szanse na krytyczne uderzenie o x%
    public int statFCS = 10;

    private int basestatPWR = 10; // PoWeR - zwieksza sile leczenia o x%
    public int statPWR = 10;

    private int experience = 0;
    public int maxExperience = 1000;
    public int level = 14;
    public int talentPoints = 0;
    public TalentTree myTalentTree;

    public string tempLevelUp = "\n"; // info o zyskanych statach
    private int[] lvlUpIncrease = new int[5];

    public int Exp
    {
        get { return experience; }
        set
        {
            if (value > maxExperience)
            {
                int ending = value - maxExperience;
                maxExperience = (int)(maxExperience * 1.2f);
                LevelUp();
                experience = ending;
            }
            else
                experience = value;
        }
    }

    public Progress myProgress;
    public Inventory myInventory;
    public TalentTree[] treeTemplate = new TalentTree[3]; // wzory drzewek talentow

    public Account()
    {
        myProgress = new Progress();
        myInventory = new Inventory();
        myTalentTree = new TalentTree(CHAMPION.PALADIN);
    }

    public TalentTree GetTemplate(CHAMPION champ)
    {
        return treeTemplate[(int)champ];
    }

    public void SaveTemplates()
    {
        string _content = "";

        for (int c = 1; c < 3; c++)
        {
            _content += c + ",";
            for (int i = 0; i < 13; i++)
            {
                _content += myTalentTree.talents[i].Points.ToString() + ",";
            }
            _content += "|";
        }

        File.WriteAllText("TalentTreesTemplates.dat", _content);
    }

    public void LoadTemplates()
    {
        if (File.Exists("TalentTreesTemplates.dat"))
        {
            string _content = File.ReadAllText("TalentTreesTemplates.dat");

            string[] _records = _content.Split('|');

            for (int c = 1; c < 3; c++)
            {
                treeTemplate[c] = GetTalentTreeFromCode(_records[c-1]);
                treeTemplate[c].PointsCap = level - 1;
                if (treeTemplate[c].CheckTotalPoints() > treeTemplate[c].PointsCap)
                    treeTemplate[c].Reset();
            }            
            Debug.Log("Talent Tree Templates loaded");
        }
        else
        {
            for (int c = 0; c < 3; c++)
            {
                treeTemplate[c] = new TalentTree(GameCore.chosenChampion);
                treeTemplate[c].DefaultTree();
            }
        }
    }

    private void LevelUp()
    {
        level += 1;
        int chosenStat = Random.Range(0, 5);
        lvlUpIncrease[chosenStat]++;
        tempLevelUp = "\n";
        if (lvlUpIncrease[0] > 0)
            tempLevelUp += "\nIntelligence +" + lvlUpIncrease[0];
        if (lvlUpIncrease[1] > 0)
            tempLevelUp += "\nKnowledge +" + lvlUpIncrease[1];
        if (lvlUpIncrease[2] > 0)
            tempLevelUp += "\nWisdom +" + lvlUpIncrease[2];
        if (lvlUpIncrease[3] > 0)
            tempLevelUp += "\nFocus +" + lvlUpIncrease[3];
        if (lvlUpIncrease[4] > 0)
            tempLevelUp += "\nPower +" + lvlUpIncrease[4];
    }

    public void ApplyTempIncreases()
    {
        basestatINT += lvlUpIncrease[0];
        basestatKNG += lvlUpIncrease[1];
        basestatWSD += lvlUpIncrease[2];
        basestatFCS += lvlUpIncrease[3];
        basestatPWR += lvlUpIncrease[4];
        for (int i = 0; i < 5; i++)
            lvlUpIncrease[i] = 0;       
    }

    public string GetExpString()
    {
        string _exp = "";
        string _maxexp = "";

        float myExp = experience;
        float myMaxExp = maxExperience;

        if (experience > 999)
        {
            myExp /= 100;
            myExp = (int)myExp;
            _exp = (myExp / 10) + "K";
        }
        else
            _exp = experience.ToString();
        if (maxExperience > 999)
        {
            myMaxExp /= 100;
            myMaxExp = (int)myMaxExp;
            _maxexp = (myMaxExp / 10) + "K";
        }
        else
            _maxexp = maxExperience.ToString();

        return _exp + " / " + _maxexp;
    }

    public void RefreshStats()
    {
        ApplyTempIncreases();
        statINT = basestatINT + myInventory.GetINTIncrease();
        statKNG = basestatKNG + myInventory.GetKNGIncrease();
        statWSD = basestatWSD + myInventory.GetWSDIncrease();
        statFCS = basestatFCS + myInventory.GetFCSIncrease();
        statPWR = basestatPWR + myInventory.GetPWRIncrease();
    }

    private string GetAccContent()
    {
        string _content = "";

        _content += level + "|";
        _content += experience + "|";
        _content += maxExperience + "|";
        _content += basestatINT + "|";
        _content += basestatKNG + "|";
        _content += basestatWSD + "|";
        _content += basestatFCS + "|";
        _content += basestatPWR + "|";

        _content += ((int)myTalentTree.classID).ToString()+",";
        for (int i=0; i<13; i++)
        {
            _content += myTalentTree.talents[i].Points.ToString() + ",";
        }

        return _content;
    }

    public void SaveAccData()
    {
        File.WriteAllText("AccData.dat", GetAccContent());
        Debug.Log("Account Saved");
    }

    public void LoadAccData()
    {
        if (File.Exists("AccData.dat"))
        {
            string _content = File.ReadAllText("AccData.dat");

            string[] _records = _content.Split('|');

            level = int.Parse(_records[0]);
            experience = int.Parse(_records[1]);
            maxExperience = int.Parse(_records[2]);
            basestatINT = int.Parse(_records[3]);
            basestatKNG = int.Parse(_records[4]);
            basestatWSD = int.Parse(_records[5]);
            basestatFCS = int.Parse(_records[6]);
            basestatPWR = int.Parse(_records[7]);
            myTalentTree = GetTalentTreeFromCode(_records[8]);
            myTalentTree.PointsCap = level - 1;
            if (myTalentTree.CheckTotalPoints() > myTalentTree.PointsCap)
                myTalentTree.Reset();
            Debug.Log("Account Loaded");
        }
        LoadTemplates();
        myTalentTree = GetTemplate(CHAMPION.PALADIN);
    }

    private TalentTree GetTalentTreeFromCode(string _code)
    {
        string[] temp = _code.Split(',');
        TalentTree tree = new TalentTree((CHAMPION)int.Parse(temp[0]));
        for (int i = 0; i < 13; i++)
        {
            tree.talents[i].Points = int.Parse(temp[i+1]);
        }
        return tree;
    }
}
