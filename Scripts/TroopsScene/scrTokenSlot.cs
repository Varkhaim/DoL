using UnityEngine;
using System.Collections;

public class scrTokenSlot : MonoBehaviour {

    public int slotID;
    public SOLDIER soldierType = SOLDIER.NONE;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PickSlot()
    {
        GameObject.Find("Main Camera").GetComponent<scrTroopsScene>().currentSlot = gameObject;
    }

    public void ResetSlot()
    {
        GameObject.Find("Main Camera").GetComponent<scrTroopsScene>().currentSlot = null;
    }
}
