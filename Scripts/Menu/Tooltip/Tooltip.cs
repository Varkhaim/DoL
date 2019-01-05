using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    /*
    public static GameObject Create(string _text)
    {
        GameObject mytip = Object.Instantiate(Resources.Load("Tooltip"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        Text myText = mytip.transform.GetChild(0).GetComponent<Text>();
        myText.text = _text;
        mytip.transform.SetParent(GameObject.Find("Canvas").transform);
        mytip.transform.localScale = new Vector3(1, 1, 1);
        return mytip;
    }
    */

    public bool followPointer;
    public int duration;
    public int timer = 0;
    public string content;
    public Text contentDisplay;

    public static Tooltip Show(string _content, int _duration, Vector3 position, bool _followPointer)
    {
        GameObject tipObject = Instantiate(Resources.Load("Tooltip")) as GameObject;
        tipObject.transform.SetParent(GameObject.Find("Canvas").transform);
        tipObject.transform.localScale = new Vector3(1, 1, 1);
        Tooltip myTip = tipObject.GetComponent<Tooltip>();
        myTip.content = _content;
        myTip.duration = _duration;
        tipObject.transform.position = position;
        myTip.followPointer = _followPointer;
        myTip.contentDisplay = tipObject.transform.GetChild(0).GetComponent<Text>();
        myTip.Refresh();

        return myTip;
    }

    private void Update()
    {
        if (followPointer)
            FollowPointer();
        else if (duration > 0)
            HandleLifeTime();
    }

    private void HandleLifeTime()
    {
        if (timer++ >= duration)
        {
            Destroy(this);
        }
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = 10f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    private void FollowPointer()
    {
        transform.position = Tooltip.GetMousePosition();
    }

    public void UpdateContent(string _content)
    {
        content = _content;
        Refresh();
    }

    public void Refresh()
    {
        contentDisplay.text = content;
    }
}
