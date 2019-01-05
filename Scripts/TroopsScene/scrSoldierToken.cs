using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrSoldierToken : MonoBehaviour
{
    public SOLDIER type;

    private bool grabbed = false;
    private bool locked = false;

    private Vector3 basePosition;

    public GameObject mySlot = null;

    void Start()
    {
        basePosition = transform.position;
        switch (type)
        {
            case SOLDIER.LONGSWORD: transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("SoldierIcons/Longsword"); break;
            case SOLDIER.SHIELDMAN: transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("SoldierIcons/Shieldman"); break;
            case SOLDIER.MAGE: transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("SoldierIcons/Mage"); break;
            case SOLDIER.RANGER: transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("SoldierIcons/Ranger"); break;
            default: transform.GetChild(0).GetComponent<Image>().sprite = null; break;
        }
    }

    void Update()
    {
        if (grabbed)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            if (Input.GetMouseButtonUp(0))
            {
                if (GameObject.Find("Main Camera").GetComponent<scrTroopsScene>().PutTokenInSlot())
                {
                    locked = true;
                    Release();
                }
                else
                {
                    //transform.position = basePosition;
                    Destroy(gameObject);
                }                
            }
        }
    }

    public void Grab()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (locked)
            {
                mySlot.GetComponent<scrTokenSlot>().soldierType = SOLDIER.NONE;
                Destroy(gameObject);
            }
        }
        else
        if (!locked)
        {
            grabbed = true;
            GameObject.Find("Main Camera").GetComponent<scrTroopsScene>().grabbedToken = gameObject;
            GetComponent<Image>().raycastTarget = false;
            GameObject myObj = Instantiate(gameObject) as GameObject;
            myObj.transform.SetParent(GameObject.Find("Canvas").transform);
            myObj.transform.position = transform.position;
            myObj.transform.localScale = transform.localScale;
            myObj.transform.SetSiblingIndex(2);
            myObj.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void Release()
    {
        grabbed = false;
        GameObject.Find("Main Camera").GetComponent<scrTroopsScene>().grabbedToken = null;
        GetComponent<Image>().raycastTarget = true;
    }
}
