using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour {

	public float ManaMax, ManaCurrent;
	private GameObject myBlack;
	
	void Start () 
	{
        GameCore.Core.myManaBar = gameObject;
		ManaMax = 100f;
		ManaCurrent = 100f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float ManaPercentage;
		ManaPercentage = ManaCurrent/ManaMax;
        GetComponent<Image>().fillAmount = ManaPercentage;
	}
}