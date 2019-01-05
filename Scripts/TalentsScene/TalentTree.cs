using UnityEngine;
using System.Collections;

public class TalentTree
{
    public Talent[] talents = new Talent[13];
    public int PointsCap = 16;
    public CHAMPION classID = 0;

    public TalentTree(CHAMPION _classID)
    {
        classID = _classID;
        if (_classID==CHAMPION.PALADIN) // paladin
        {
            talents[0] = new Talent(0, "Hand of Light", this);
            talents[1] = new Talent(0, "Iron Faith", this);
            talents[2] = new Talent(0, "Spirit Bond", this);

            talents[3] = new Talent(1, "Aura of Light", this, 0, 1);
            talents[4] = new Talent(1, "Consecration", this, 2);

            talents[5] = new Talent(2, "Royalty", this, 3);
            talents[6] = new Talent(2, "Divinity", this, 4);

            talents[7] = new Talent(3, "Empathy", this, 6);
            talents[8] = new Talent(3, "Generousity", this, 5);

            talents[9] = new Talent(4, "Modesty", this, 7);
            talents[10] = new Talent(4, "Visions of Ancient Kings", this, 8);
            talents[11] = new Talent(4, "Guidance of Raela", this, 8);

            talents[12] = new Talent(5, "Flash of Future", this, 9, 10, 11);
        }
        else
            if (_classID == CHAMPION.SHADOWMANCER) // shadowmancer
        {
            talents[0] = new Talent(0, "Dusk", this);
            talents[1] = new Talent(0, "Dawn", this);
            talents[2] = new Talent(0, "Moonlight", this);

            talents[3] = new Talent(1, "Shadowforce", this, 0);
            talents[4] = new Talent(1, "Shadowmend", this, 1);
            talents[5] = new Talent(1, "Fading Light", this, 2);

            talents[6] = new Talent(2, "Dream", this, 3);
            talents[7] = new Talent(2, "Silence", this, 4);
            talents[8] = new Talent(2, "Peace", this, 5);

            talents[9] = new Talent(3, "Awakening", this, 6, 7);
            talents[10] = new Talent(3, "Ascetic", this, 7, 8);

            talents[11] = new Talent(4, "Trauma", this, 9);
            talents[12] = new Talent(4, "Book of Prime Shadows", this, 10);
        }
        else
            for (int i=0; i<13; i++)
        {
            talents[i] = new Talent(0, "filler", this);
        }

        //PointsCap = GameCore.Core.chosenAccount.talentPoints;
    }

    public void Reset()
    {
        for (int i = 0; i < 13; i++)
        {
            talents[i].Points = 0;
            CheckTalents();
        }
    }

    public void CheckTalents()
    {
        for (int i=0; i<13; i++)
        {
            talents[i].CheckTalent();
            scrTalentSceneController.SetTalentPointsText("Points Spend: " + CheckTotalPoints().ToString() + "/" + PointsCap.ToString());
        }
    }

    public void DefaultTree()
    {
        for (int i=0; i<13; i++)
        {
            talents[i].Points = 4;
        }
    }

    public int GetSpellsAmountFromTier(int _tier)
    {
        int val=0;

        for (int i=0; i<13; i++)
        {
            if (talents[i].tier == _tier)
                val++;
        }

        return val;
    }

    public void InsertPoints(string _name, int _points) // ustala punkty danego talentu
    {
        for (int i = 0; i < 13; i++)
        {
            if (talents[i].Name == _name)
                talents[i].Points = _points;
        }   
    }

    public int GetTalentPointsByName(string _name)
    {
        for (int i = 0; i < 13; i++)
        {
            if (talents[i].Name == _name)
            {
                return talents[i].Points;
            }
        }
        return 0;
    }

    public Talent GetTalentByName(string _name) // wybiera talent szukajac po nazwie
    {
        for (int i=0; i<13; i++)
        {
            if (talents[i].Name == _name)
            {
                return talents[i];
            }
        }
        return null;
    }

    public int CheckTotalPoints()
    {
        int _pointstotal = 0;

        for (int i = 0; i < 13; i++)
        {
            _pointstotal += talents[i].Points;
        }

        return _pointstotal;
    }
    

    public string GetTreeCode()
    {
        string _temp = "";

        for (int i = 0; i < 13; i++)
        {
            _temp += talents[i].Points.ToString() + "|";
        }

        _temp += System.Environment.NewLine;

        return _temp;
    }

    public static TalentTree SetTreeFromCode(string _code, CHAMPION _classID)
    {
        TalentTree _temp = null;

        string[] _parts = _code.Split('|');

        _temp = new TalentTree(_classID);

        for (int i = 0; i < 13; i++)
        {
            int.TryParse(_parts[i], out _temp.talents[i].Points);
        }

        return _temp;
    }
}

