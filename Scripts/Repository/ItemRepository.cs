using UnityEngine;
using System.Collections;

/// <summary>
/// Repozytorium itemow
/// </summary>

public class ItemRepository : IRepo<CombatItem>
{
    private CombatItem[] combatItem = new CombatItem[100];
    private int count = 0;

    public ItemRepository()
    {
        FillRepository();
    }

    public CombatItem GetObject(int _ID)
    {
        return combatItem[_ID];
    }

    public void UpdateObject(int _ID, CombatItem _object)
    {

    }

    public void AddObject(CombatItem _object)
    {
        combatItem[count++] = _object;
    }

    public void RemoveObject(int _ID)
    {

    }

    public void FillRepository()
    {
        AddObject(new CombatItem((int)COMBATITEM.MANA_POTION, "Mana Potion", "Restores 30% of your missing mana.", 30, "ManaPotion"));
        AddObject(new CombatItem((int)COMBATITEM.SCROLL_OF_ALAPHI, "Scroll of Renew", "Heals everyone for 50% of their maximum health.", 50, "DivineScroll"));
    }
}

public enum COMBATITEM
{
    MANA_POTION,
    SCROLL_OF_ALAPHI
}