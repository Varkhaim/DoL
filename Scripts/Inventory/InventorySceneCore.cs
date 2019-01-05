using UnityEngine;
using System.Collections;

public class InventorySceneCore : MonoBehaviour
{
    /*
    // tworzenie sceny
    public int amount = 21;

    
    public GameObject HEAD;
    public GameObject NECKLACE;
    public GameObject CLOAK;
    public GameObject CHEST;
    public GameObject GLOVES;
    public GameObject MAINHAND;
    public GameObject OFFHAND;
    public GameObject LEGS;
    public GameObject BOOTS;
    public GameObject TRINKET1;
    public GameObject TRINKET2;
    public GameObject TRINKET3;
    public GameObject RING1;
    public GameObject RING2;
    

    public GameObject[] eqSlot = new GameObject[14];
    public GameObject[] invSlot = new GameObject[30];

    void Start()
    {
        for (int i = 0; i < 14; i++)
        {
            eqSlot[i] = Instantiate(Resources.Load("ItemSlot"), new Vector3(0.65f + (i/7) * 5.75f, 4 - i * 1.25f + (i/7)*8.75f), transform.rotation) as GameObject;
            eqSlot[i].GetComponent<InventorySlotScript>().slotIndex = i;
            eqSlot[i].GetComponent<InventorySlotScript>().core = gameObject;
            eqSlot[i].GetComponent<InventorySlotScript>().charSlot = true;
        }

        for (int i = 0; i < amount; i++)
        {
            invSlot[i] = Instantiate(Resources.Load("ItemSlot"), transform.position + new Vector3((i / 7) * 1.25f, i * 1.25f - (i / 7) * 8.75f), transform.rotation) as GameObject;
            invSlot[i].GetComponent<InventorySlotScript>().slotIndex = i;
            invSlot[i].GetComponent<InventorySlotScript>().core = gameObject;
        }
    }

    void Update()
    {
        RefreshSprites();
    }

    public void RefreshSprites()
    {
        for (int i = 0; i < 21; i++)
        {
            Item tempitem = GameCore.Core.chosenAccount.myInventory.item[i];
            if (tempitem != null)
            {
                Sprite temp = GameCore.Core.chosenAccount.myInventory.item[i].mySprite;
                if (temp != null)
                {
                    invSlot[i].GetComponent<InventorySlotScript>().myItem.GetComponent<SpriteRenderer>().sprite = temp;
                    invSlot[i].GetComponent<InventorySlotScript>().myShine.GetComponent<SpriteRenderer>().color = GameCore.Core.chosenAccount.myInventory.item[i].GetColor();
                }
            }
            else
            {
                invSlot[i].GetComponent<InventorySlotScript>().myItem.GetComponent<SpriteRenderer>().sprite = null;
                invSlot[i].GetComponent<InventorySlotScript>().myShine.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
            }
        }
        for (int i = 0; i < 14; i++)
        {
            Item tempitem = GameCore.Core.chosenAccount.myInventory.equippedItem[i];
            if (tempitem != null)
            {
                Sprite temp = GameCore.Core.chosenAccount.myInventory.equippedItem[i].mySprite;
                if (temp != null)
                {
                    eqSlot[i].GetComponent<InventorySlotScript>().myItem.GetComponent<SpriteRenderer>().sprite = temp;
                    eqSlot[i].GetComponent<InventorySlotScript>().myShine.GetComponent<SpriteRenderer>().color = GameCore.Core.chosenAccount.myInventory.equippedItem[i].GetColor();
                }
            }
            else
            {
                if (eqSlot[i] != null)
                {
                    eqSlot[i].GetComponent<InventorySlotScript>().myItem.GetComponent<SpriteRenderer>().sprite = null;
                    eqSlot[i].GetComponent<InventorySlotScript>().myShine.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);

                }
            }
        }
    }
    */
}
