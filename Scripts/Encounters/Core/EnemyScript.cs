using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	public static float bonusDmg=1f;
	public bool isAlive=true;
	public GameCore core = GameCore.Core;
	public GameObject myHP;
	public float SoldierAmount, baseSoldierAmount;
	
	public GameObject bossEmote;

	private GameObject myHealthBar;
    private GameObject myPortrait;
    private GameObject myName;

    public Mob myMob;

    public GameObject progressBar;

    private AdventureInfo myInfo;

    bool killed = false;

	void Start () 
	{
        myInfo = WorldMapCore.WMCore.adventureInfoHandler.adventureInfo[core.chosenAdventure.ID];

        //myMob.myBossTalk.myTextMesh = bossEmote;

        myHealthBar = transform.GetChild(5).GetChild(0).gameObject;
        myPortrait = transform.GetChild(0).GetChild(0).gameObject;
        myName = transform.GetChild(3).gameObject;

        myPortrait.GetComponent<Image>().sprite = myMob.LoadPortrait();
    }

    private void OnMouseEnter()
    {
        myName.GetComponent<Text>().text = myMob.name;
    }

    private void OnMouseExit()
    {
        myName.GetComponent<Text>().text = "";
    }

    void Update () 
	{
        if (!ScenesController.SControl.isLocked)
        {
            if (!core.GameFinished)
            {
                //myMob.myBossTalk.Update();
                //myMob.Update();

                myHealthBar.GetComponent<Image>().fillAmount = myMob.HealthPercentage;
            }
        }
	}
}
