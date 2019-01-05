using UnityEngine;
using System.Collections;

public class scrCanvas : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        ScenesController.SControl.BlackScreenAnimation();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ScenesController.SControl.Update();
	}
}
