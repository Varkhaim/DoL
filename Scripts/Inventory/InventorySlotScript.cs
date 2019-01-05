using UnityEngine;
using System.Collections;

public class InventorySlotScript : MonoBehaviour
{
    /*public int slotIndex = 0;
    public bool charSlot = false;
    public GameObject myItem;
    public GameObject myShine;
    public GameObject core;

    private Tooltip myTooltip;

    void Start()
    {
        myItem = Instantiate(Resources.Load("ItemObject"), transform.position + new Vector3(0,0,-2), transform.rotation) as GameObject;
        myShine = Instantiate(Resources.Load("ShineEffect"), transform.position + new Vector3(0, 0, -1), transform.rotation) as GameObject;
    }

    void OnMouseEnter()
    {
        Item _item;
        if (charSlot)
        {
            _item = GameCore.Core.chosenAccount.myInventory.equippedItem[slotIndex];
        }
        else
        {
            _item = GameCore.Core.chosenAccount.myInventory.item[slotIndex];
        }
        if ((myTooltip == null) && (_item != null))
        {
            Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //myTooltip = new Tooltip(myPos.x, myPos.y, _item.GetDescription(), true, 2f);
        }
    }

    void OnMouseExit()
    {
        if (myTooltip != null)
        {
           // myTooltip.Delete();
            myTooltip = null;
        }
        
    }

    void OnMouseOver()
    {
        if ((Input.GetMouseButtonDown(0)) && (!charSlot))
        {
            if (GameCore.Core.chosenAccount.myInventory.EquipItem(slotIndex))
            OnMouseExit();
        }
        if ((Input.GetMouseButtonDown(1)) && (charSlot))
        {
            if (GameCore.Core.chosenAccount.myInventory.UnequipItem(slotIndex))
                OnMouseExit();
        }
        if (myTooltip != null)
        {
            Vector3 _myv = myTooltip.myBack.transform.localScale;
            myTooltip.myBack.transform.localScale = new Vector3(_myv.x, myTooltip.myTip.GetComponent<Renderer>().bounds.extents.y/5.5f);
            while (myTooltip.myTip.transform.position.y - myTooltip.myTip.GetComponent<Renderer>().bounds.extents.y < -3f)
            {
                myTooltip.myTip.transform.position += Vector3.up;
                myTooltip.myBack.transform.position += Vector3.up;
            }
        }
    }*/
}
