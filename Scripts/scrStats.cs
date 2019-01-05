using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrStats : MonoBehaviour {

    public Text statText;

	void Start ()
    {
        statText = transform.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        statText.text = GameCore.Core.myCaster.GetStatsString();
	}
}
