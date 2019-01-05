using UnityEngine;
using System.Collections;

public class BossTalk
{
	private string[] text;
	private Color[] textColor;
	private int textAmount;
	public GameObject myTextMesh;
	private int freq; // x tekstow na minute
	private bool live=true;

	private int gap;
	private int myTime=0;
    private int closeCD=0;
    private GameCore core = GameCore.Core;

    private string deathText;
    private Color deathColor;

	public BossTalk (int _freq)
	{
		text = new string[10];
		textColor = new Color[10];
		textAmount = 0;
		freq = _freq;
		gap = 3600/freq;
	}

	public void AddText(string _text, Color _color)
	{
		text[textAmount] = _text;
		textColor[textAmount++] = _color;
	}

    public void SetDeathTalk(string _text, Color _color)
    {
        deathText = _text;
        deathColor = _color;
    }

    public void DeathTalk()
    {
        NewTalk(deathText, deathColor);
    }

	public void RandomTalk()
	{
		int r = Random.Range(0, textAmount);
        NewTalk(text[r], textColor[r]);
    }

	public void Talk(string _text, Color _color, int minimumTime)
	{
        NewTalk(_text, _color);
        myTime += minimumTime;
	}

    private void NewTalk(string _text, Color _color)
    {
        if ((closeCD <= 0) && (!core.GameFinished))
        {
            GameObject myText = Object.Instantiate(Resources.Load("EmoteText"), new Vector3(4f, 5f, -5f), Quaternion.Euler(0, 0, 0)) as GameObject;
            myText.GetComponent<TextMesh>().text = _text;
            myText.GetComponent<EmoteText>().SetColor(_color);
            closeCD = 120;
        }
    }

	public void Stop()
	{
		live = false;
	}

	public void Update()
	{
        if (closeCD > 0)
            closeCD--;
		if (live)
		{
			if (myTime < gap)
			{
				myTime++;
			}
			else
			{
				RandomTalk();
				myTime = 0;
			}
		}

	}
}


