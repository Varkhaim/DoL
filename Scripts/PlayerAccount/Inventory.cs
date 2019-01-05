using UnityEngine;
using System.Collections;

public class Inventory
{
    public Item[] item = new Item[50];
    public int itemAmount = 0;

    public Item[] equippedItem = new Item[14];

    public const int HEAD = 0;
    public const int NECKLACE = 1;
    public const int CLOAK = 2;
    public const int CHEST = 3;
    public const int GLOVES = 4;
    public const int MAINHAND = 5;
    public const int OFFHAND = 6;
    public const int LEGS = 7;
    public const int BOOTS = 8;
    public const int TRINKET1 = 9;
    public const int TRINKET2 = 10;
    public const int TRINKET3 = 11;
    public const int RING1 = 12;
    public const int RING2 = 13;

    public Inventory()
    {

    }

    public Item AddItem(int _ID, int _diff)
    {
        item[itemAmount] = new Item(_ID, _diff);
        return item[itemAmount++];
    }

    public void AddItem(Item _item)
    {
        item[itemAmount++] = _item;
    }

    public bool UnequipItem(int index)
    {
        if (equippedItem[index] != null)
        {
            AddItem(equippedItem[index]);
            equippedItem[index] = null;
            return true;
        }
        else
            return false;
    }

    public bool EquipItem(int index)
    {
        bool _success = false;
        if (item[index] != null)
        {
            int slotIndex;

            slotIndex = item[index].slot;

            switch (slotIndex)
            {
                case Item.HEAD:
                    {
                        if (equippedItem[HEAD] == null)
                        {
                            equippedItem[HEAD] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    } break;
                case Item.NECKLACE:
                    {
                        if (equippedItem[NECKLACE] == null)
                        {
                            equippedItem[NECKLACE] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    } break;
                case Item.CLOAK:
                    {
                        if (equippedItem[CLOAK] == null)
                        {
                            equippedItem[CLOAK] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    } break;
                case Item.CHEST:
                    {
                        if (equippedItem[CHEST] == null)
                        {
                            equippedItem[CHEST] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    } break;
                case Item.GLOVES:
                    {
                        if (equippedItem[GLOVES] == null)
                        {
                            equippedItem[GLOVES] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    }
                    break;
                case Item.MAINHAND:
                    {
                        if (equippedItem[MAINHAND] == null)
                        {
                            equippedItem[MAINHAND] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    }
                    break;
                case Item.OFFHAND:
                    {
                        if (equippedItem[OFFHAND] == null)
                        {
                            equippedItem[OFFHAND] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    }
                    break;
                case Item.LEGS:
                    {
                        if (equippedItem[LEGS] == null)
                        {
                            equippedItem[LEGS] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    }
                    break;
                case Item.BOOTS:
                    {
                        if (equippedItem[BOOTS] == null)
                        {
                            equippedItem[BOOTS] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    }
                    break;
                case Item.TRINKET:
                    {
                        if (equippedItem[TRINKET1] == null)
                        {
                            equippedItem[TRINKET1] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                        else
                            if (equippedItem[TRINKET2] == null)
                        {
                            equippedItem[TRINKET2] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                        else
                            if (equippedItem[TRINKET3] == null)
                        {
                            equippedItem[TRINKET3] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    }
                    break;
                case Item.RING:
                    {
                        if (equippedItem[RING1] == null)
                        {
                            equippedItem[RING1] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                        else
                            if (equippedItem[RING2] == null)
                        {
                            equippedItem[RING2] = item[index];
                            item[index] = null;
                            itemAmount--;
                            _success = true;
                        }
                    }
                    break;
            }
            RefreshInventory();
        }
        return _success;
    }

    public void RefreshInventory()
    {
        for (int i=0; i<itemAmount; i++)
        {
            if (item[i] == null)
            {
                item[i] = item[i + 1];
                item[i + 1] = null;
            }
        }
    }

    public int GetINTIncrease()
    {
        int inc = 0;
        for (int i = 0; i < 14; i++)
        {
            if (equippedItem[i] != null)
                inc += equippedItem[i].statINT;
        }
        return inc;
    }

    public int GetKNGIncrease()
    {
        int inc = 0;
        for (int i = 0; i < 14; i++)
        {
            if (equippedItem[i] != null)
                inc += equippedItem[i].statKNG;
        }
        return inc;
    }

    public int GetWSDIncrease()
    {
        int inc = 0;
        for (int i = 0; i < 14; i++)
        {
            if (equippedItem[i] != null)
                inc += equippedItem[i].statWSD;
        }
        return inc;
    }

    public int GetFCSIncrease()
    {
        int inc = 0;
        for (int i = 0; i < 14; i++)
        {
            if (equippedItem[i] != null)
                inc += equippedItem[i].statFCS;
        }
        return inc;
    }

    public int GetPWRIncrease()
    {
        int inc = 0;
        for (int i=0; i<14; i++)
        {
            if (equippedItem[i] != null)
            inc += equippedItem[i].statPWR;
        }
        return inc;
    }
}
