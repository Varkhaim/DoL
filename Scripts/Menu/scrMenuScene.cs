using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMenuScene : MonoBehaviour
{
    public void StartGame()
    {
        ScenesController.SControl.SwitchScene("AdventureScene");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
