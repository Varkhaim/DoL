using UnityEngine;
using System.Collections;

public class scrTroopsScene : MonoBehaviour
{
    public static SOLDIER[] pickedTypes = new SOLDIER[16];
    public GameObject grabbedToken;
    public GameObject currentSlot;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public bool PutTokenInSlot()
    {
        if ((grabbedToken != null) && (currentSlot != null))
        {
            grabbedToken.transform.position = currentSlot.transform.position;
            currentSlot.GetComponent<scrTokenSlot>().soldierType = grabbedToken.GetComponent<scrSoldierToken>().type;
            grabbedToken.GetComponent<scrSoldierToken>().mySlot = currentSlot;
            scrTroopsScene.pickedTypes[currentSlot.GetComponent<scrTokenSlot>().slotID] = currentSlot.GetComponent<scrTokenSlot>().soldierType;
            return true;
        }
        else
            return false;
    }

    public void AcceptButtonClick()
    {
        ScenesController.SControl.SwitchScene("DescendantScene");
    }
}
