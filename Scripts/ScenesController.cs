using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesController
{
    // Singleton
    static private ScenesController sControl;

    private ScenesController()
    {

    }

    static public ScenesController SControl
    {
        get
        {
            if (sControl == null)
                sControl = new ScenesController();
            return sControl;
        }
    }
    // END Singleton

    string myScene;
    public bool switchingScene = false;
    GameObject myBlackScreen;
    float alpha;

    bool switchAnimation = false;
    float alpha2;
    public bool isLocked = false;

    public void SwitchScene(string sceneName)
    {
        if ((!switchingScene) && (!switchingScene))
        {
            myScene = sceneName;
            switchingScene = true;
            myBlackScreen = GameObject.Instantiate(Resources.Load("BlackScreen")) as GameObject;
            myBlackScreen.transform.SetParent(GameObject.Find("Canvas").transform);
            myBlackScreen.GetComponent<Image>().color = Color.clear;
        }
    }

    public void BlackScreenAnimation()
    {
        if ((!switchAnimation) && (!switchingScene))
        {
            switchAnimation = true;
            myBlackScreen = GameObject.Instantiate(Resources.Load("BlackScreen")) as GameObject;
            myBlackScreen.transform.SetParent(GameObject.Find("Canvas").transform);
            myBlackScreen.GetComponent<Image>().color = Color.black;
            isLocked = true;
        }
    }

    public void Update()
    {
        if (switchingScene)
        {
            if (alpha < 1f)
            {
                alpha += 0.02f;
                myBlackScreen.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            }
            else
            {
                SceneManager.LoadScene(myScene);
                switchingScene = false;
            }
        }

        if (switchAnimation)
        {
            if (alpha > 0f)
            {
                alpha -= 0.02f;
                myBlackScreen.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            }
            else
            {
                switchAnimation = false;
                isLocked = false;
                GameObject.Destroy(myBlackScreen.gameObject);
            }
        }
    }
}
