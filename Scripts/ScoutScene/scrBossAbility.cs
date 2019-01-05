using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrBossAbility : MonoBehaviour
{
    public Text myText;
    public EnemySpellInfo espell;

    public void OnMouseEnter()
    {
        myText.text = espell.GetDescription();
    }

    public void OnMouseExit()
    {
        myText.text = "";
    }
}
