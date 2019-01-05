using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrTalentIcon : MonoBehaviour
{
    public Talent myTalent;

    Tooltip myTip;

    Image myIcon;

    void Start()
    {
        
        myIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        RefreshIcon();
    }

    void RefreshIcon()
    {
        if (myTalent.Unlocked)
        {
            if (myTalent.Points <= 0)
                myIcon.color = Color.gray;
            else
                myIcon.color = Color.white;
        }
        else
            myIcon.color = Color.black;

        if (myTalent.Points > 1)
        {
            //transform.GetChild(3).GetComponent<Text>().text = "+" + (myTalent.Points - 1).ToString();
            transform.GetChild(2).GetComponent<Image>().fillAmount = (float)(myTalent.Points - 1) / 3f;
        }
        else
        {
            transform.GetChild(3).GetComponent<Text>().text = "";
            transform.GetChild(2).GetComponent<Image>().fillAmount = 0;
        }
    }

    public void OnMouseDown()
    {
        myTalent.AddPoint();
    }

    public void OnMouseEnter()
    {
        myTip = Tooltip.Show(myTalent.GetTalentString(), 0, Tooltip.GetMousePosition(), false);
    }

    public void OnMouseExit()
    {
        if (myTip != null)
        Destroy(myTip.gameObject);
    }

    void Update()
    {
        RefreshIcon();
        /*if (myTip != null)
        {
            Vector3 mousepos = Input.mousePosition;

            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            mousepos.x = Mathf.Max(-7f, Mathf.Min(mousepos.x+2f, 7f));
            mousepos.y = Mathf.Max(-3f, Mathf.Min(mousepos.y+2f, 3f));
            mousepos.z = 0;

            myTip.transform.position = mousepos;
        }*/
    }

}
