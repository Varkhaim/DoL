using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    public GameObject mytext;
    public GameCore core = GameCore.Core;
    public Image myShadow;
    public int myID;

    void Start()
    {
        mytext = transform.GetChild(0).gameObject;
        myShadow = transform.parent.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
       /* if (core.actualSpell == myID)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }*/
    }
}