using UnityEngine;
using System.Collections;

public class SoldierPortrait : MonoBehaviour
{
    void Start()
    {
        GameCore.Core.myPortrait = gameObject;
        RefreshPortrait();
    }

    public void RefreshPortrait()
    {
        Sprite _mysprite;
        switch (GameCore.chosenChampion)
        {
            case CHAMPION.PALADIN: _mysprite = Resources.Load<Sprite>("Portraits/paladin_portrait"); break;
            case CHAMPION.SHADOWMANCER: _mysprite = Resources.Load<Sprite>("Portraits/shadowmancer_portrait"); break;
            default: _mysprite = Resources.Load<Sprite>("Portraits/paladin_portrait"); break;
        }

        GetComponent<SpriteRenderer>().sprite = _mysprite;
    }
}
