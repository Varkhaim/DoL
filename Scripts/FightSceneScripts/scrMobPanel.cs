using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class scrMobPanel : MonoBehaviour
{
    public Mob myMob;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            LeftClicked();
        if (Input.GetMouseButtonDown(1))
            RightClicked();
    }

    public void LeftClicked()
    {
        GameCore.Core.SetTarget(myMob);
    }

    public void RightClicked()
    {
        myMob.Interupt();
    }

    void Update()
    {
        if (dying)
        DyingAnimation();
    }

    public bool dying = false;
    float animVal = 1f;
    public void DyingAnimation()
    {
        animVal -= 0.1f;

        transform.localScale = new Vector3(animVal, animVal);

        if (animVal <= 0)
        {
            myMob.Remove();
        }
    }
}
