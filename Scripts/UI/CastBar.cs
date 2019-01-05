using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CastBar : MonoBehaviour
{

    private Image myBack;
    private Image myBar;
    private Text myText;
    private Image myGCD;

	void Start () {
        GameCore.Core.spellCastHandler.myCastBar = gameObject;

        myBack = transform.GetChild(0).GetComponent<Image>();
        myBar = transform.GetChild(1).GetComponent<Image>();
        myText = transform.GetChild(2).GetComponent<Text>();
        myGCD = transform.GetChild(3).GetComponent<Image>();
    }

	void Update ()
    {
	
	}

    public void SetFill(float amount)
    {
        myBar.fillAmount = amount;
    }

    public void SetGCD(float amount)
    {
        if (amount >= 1f)
        myGCD.fillAmount = 0;
            else
        myGCD.fillAmount = amount;
    }

    public void Clear()
    {
        myBack.color = Color.clear;
        myBar.color = Color.clear;
        myText.text = "";
    }

    public void SetCast(Sprite _icon, string _name)
    {
        myBack.color = Color.black;
        myBar.color = Color.white;
        myText.text = _name;
    }
}
