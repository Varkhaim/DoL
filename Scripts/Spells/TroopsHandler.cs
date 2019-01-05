using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TroopsHandler
{
    public GameObject[] framez;
    public int SoldierAmount;
    public int count;
    public Soldier[] soldier;
    private Soldier[] soldierPlace; // tabela przechowujaca Soldier wg pozycji
    public Vector3[] basePosition = new Vector3[32];

    // tanks
    private Soldier[] tank = new Soldier[16];
    public int tanksAmount = 0;

    public TroopsHandler()
    {

    }

    public void InitSoldiers()
    {
        soldier = new Soldier[16];
        soldierPlace = new Soldier[32];
        count = 16;
        SoldierAmount = 16;
        for (int i = 0; i < 16; i++)
        {
            SOLDIER _type = (SOLDIER)Random.Range(1, 5);
            if (scrTroopsScene.pickedTypes[i] != SOLDIER.NONE)
                _type = scrTroopsScene.pickedTypes[i];
            soldier[i] = new Soldier(i, RandomName(), null, 600f, _type);
            soldier[i].InitHealingHandler();
            soldierPlace[i] = soldier[i];
            if (soldier[i].type == SOLDIER.SHIELDMAN)            
                tank[tanksAmount++] = soldier[i];
        }

        for (int i=0; i<32; i++)
        {
            int j = i / 8;
            float basex = -4.75f;
            float x_spacing = 1.35f;
            float basey = 2.05f;
            float y_spacing = 1.35f;
            basePosition[i] = new Vector3(basex + ((i * x_spacing) - j * (8 * x_spacing)), basey - j * y_spacing);
            
        }
    }

    public void SetTarget(Mob _target)
    {
        for (int i = 0; i < SoldierAmount; i++)
        {
            if (soldier[i] != null)
                soldier[i].SetTarget(_target);
        }
    }

    public Soldier[] getTanks()
    {
        SortTanks();
        return tank;
    }

    public void MoveRow(int _row)
    {
        for (int i=0; i<4; i++)
        {
            Soldier temp = soldierPlace[i * 4 + _row];
            soldierPlace[i * 4 + _row] = soldierPlace[i * 4 + _row + 1];
            soldierPlace[i * 4 + _row + 1] = temp;
        }
        UpdateSoldiersPositions(0.25f);
    }

    // poruszanie jednostek
        
    public void SpreadTroop()
    {
        for (int i=0; i<32; i++)
        {
            soldierPlace[i] = null;              
        }
        for (int i=0; i<16; i++)
        {
            int myVal = i * 2;
            if (((i > 3) && (i < 8)) || (i > 11))
                myVal += 1;
            soldierPlace[myVal] = soldier[i];
        }
        UpdateSoldiersPositions(1f);
    }

    public void StackTroop()
    {
        for (int i = 0; i < 32; i++)
        {
            soldierPlace[i] = null;
        }
        for (int i = 0; i < 16; i++)
        {
            int myVal = 2;
            if (i > 3)
                myVal += 4;
            if (i > 7)
                myVal += 4;
            if (i > 11)
                myVal += 4;
            soldierPlace[myVal + i] = soldier[i];
        }
        UpdateSoldiersPositions(1f);
    }

    // ---

    public void MoveCol(int _col)
    {
        for (int i = 0; i < 4; i++)
        {
            Soldier temp = soldierPlace[i + _col * 4];
            soldierPlace[i + _col * 4] = soldierPlace[i + _col * 4 + 4];
            soldierPlace[i + _col * 4 + 4] = temp;
        }
        UpdateSoldiersPositions(0.5f);        
    }

    private void UpdateSoldiersPositions(float _speed)
    {
        for (int i = 0; i < 32; i++)
        {
            if (null != soldierPlace[i])
            soldierPlace[i].moveTo(basePosition[i], _speed);
        }
    }

    private string RandomName()
    {
        int ran = Random.Range(0, 10);
        switch (ran)
        {
            case 0: return "Chuck";
            case 1: return "Annie";
            case 2: return "Matthew";
            case 3: return "Bob";
            case 4: return "Christopher";
            case 5: return "Xena";
            case 6: return "Ray";
            case 7: return "Jared";
            case 8: return "Caesar";
            case 9: return "Lancelot";
            default: return "Noob";
        }
    }

    private bool isExcluded(Soldier chosen, Soldier[] excTab)
    {
        int i = 0;
        while (excTab[i] != null)
        {
            if (excTab[i++] == chosen)
                return true;
        }
        return false;
    }

    public Troop GetTargets(TARGETTYPE type, int amount, Soldier[] _excluded = null, Soldier sol = null)
    {
        Troop troop;
        Soldier[] tars;

        amount = Mathf.Min(amount, SoldierAmount);

        if (type == TARGETTYPE.EVERYONE)
            amount = 16;

        tars = new Soldier[amount];

        switch (type)
        {
            case TARGETTYPE.EVERYONE:
                {
                    for (int i = 0; i < 16; i++)
                    {
                        tars[i] = soldier[i];
                    }
                }
                break;
            case TARGETTYPE.NEARBY:
                {

                }
                break;
            case TARGETTYPE.BY_HEALTH:
                {
                    tars = GetSoldiersByHealth(amount);                    
                }
                break;
            case TARGETTYPE.FIRSTCOLUMN:
                {
                    int bas = 16;
                    int _dead = 0;
                    int _which = 0;

                    do
                    {
                        bas -= 4;
                        _which = 0;
                        _dead = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (null != soldierPlace[bas + i])
                            {
                                if (soldierPlace[bas + i].isDead)
                                    _dead++;
                                else
                                {
                                    tars[_which++] = soldierPlace[bas + i];
                                }
                            }
                        }
                    }
                    while (_dead >= 4);
                }
                break;
            case TARGETTYPE.RANDOM:
                {
                    int _Soldieramount = 16; // ilosc graczy
                    Soldier[] _Soldiers = new Soldier[_Soldieramount]; // tablica graczy
                    int counter = 0;

                    if (amount > SoldierAmount) // jesli zywych graczy jest mniej niz ilosc celow, zmniejszamy ilosc celow
                        amount = SoldierAmount;

                    // losowanie celow bez zwracania
                    for (int i = 0; i < 16; i++)
                    {
                        // wypelniamy tablice graczy zywymi graczami
                        if (!soldier[i].isDead)
                        {
                            _Soldiers[counter++] = soldier[i];
                        }
                        else
                        {
                            _Soldieramount--;
                        }
                    }

                    for (int j = 0; j < amount; j++)
                    {
                        if (amount == 1)
                        {
                            int r = Random.Range(0, _Soldieramount);
                            tars[j] = _Soldiers[r];
                        }
                        else
                        {
                            int r = Random.Range(0, _Soldieramount);
                            tars[j] = _Soldiers[r];
                            _Soldiers[r] = _Soldiers[_Soldieramount - 1];
                            _Soldieramount--;
                        }
                    }
                } break;
            default: Debug.Log(type); break;
        }

        if (_excluded != null)
        {
            for (int i = 0; i < amount; i++)
            {
                if (isExcluded(tars[i], _excluded))
                    tars[i] = null;
            }
        }

        troop = new Troop(amount, tars);
        return troop;
    }

    public void Update()
    {

    }

    public void ClearSoldiers()
    {
        for (int i = 0; i < SoldierAmount; i++)
        {
            if (soldier[i] != null)
                soldier[i].ClearSoldier();
        }
    }

    public Soldier[] GetSoldiersByHealth(int amount)
    {
        Soldier[] tars = soldier;

        int n = SoldierAmount;

        do
        {
            for (int i = 0; i < n - 1; i++)
            {
                if (tars[i].health > tars[i + 1].health)
                {
                    Soldier temp = tars[i];
                    tars[i] = tars[i + 1];
                    tars[i + 1] = temp;
                }
            }
            n--;
        }
        while (n > 1);

        return tars;
    }

    // --- Metoda ktora sortuje/czysci tablice graczy (jesli ktos zginal usuwa go z tablicy)
    public void SortSoldiers()
    {
        for (int i = 0; i < SoldierAmount; i++)
        {
            if (soldier[i].health <= 0f) // jesli gracz nie ma przypisanego paska HP (czyli zginal)...
            {
                if (i < SoldierAmount - 1) // ...to jesli to nie-ostatni gracz...
                {
                    Soldier temp = soldier[i];

                    soldier[i] = soldier[i + 1];

                    soldier[i + 1] = temp;

                    // ...to zamieniamy go miejscami z kolejnym zywym, az do momentu az zmarly bedzie jako ostatni
                }
                if (i >= SoldierAmount - 1) // ...jesli to ostatni gracz
                {
                    soldier[i].Kill();
                    SoldierAmount--; // zmniejszamy ilosc graczy o 1
                }

            }
        }
    }

    public void SortTanks()
    {
        for (int i = 0; i < tanksAmount; i++)
        {
            if (tank[i].health <= 0f) // jesli gracz nie ma przypisanego paska HP (czyli zginal)...
            {
                if (i < tanksAmount - 1) // ...to jesli to nie-ostatni gracz...
                {
                    Soldier temp = tank[i];

                    tank[i] = tank[i + 1];

                    tank[i + 1] = temp;

                    // ...to zamieniamy go miejscami z kolejnym zywym, az do momentu az zmarly bedzie jako ostatni
                }
                if (i >= tanksAmount - 1) // ...jesli to ostatni gracz
                {
                    tanksAmount--; // zmniejszamy ilosc graczy o 1
                }

            }
        }
    }
}
