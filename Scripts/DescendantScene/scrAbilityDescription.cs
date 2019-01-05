using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrAbilityDescription : MonoBehaviour
{
    public int whichSpell;

    void Start()
    {
        UpdateContent();
    }

    public void UpdateContent()
    {
       GetComponent<Text>().text = DescendantPanelCore.DPCore.GetSpellInfo((SPELL)whichSpell);
    }
}
