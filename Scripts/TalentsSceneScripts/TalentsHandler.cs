using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TalentsHandler
{
    public TalentsHandler() { }

    private float gridSize = 1.5f;
    public TalentTree tree;

    public void ShowTalentTree(TalentTree _tree)
    {
        tree = _tree;
        int actualTier = -1;
        int myNum = 0;
        float xOff = 0;

        GameObject[] myLinks = new GameObject[40];
        GameObject[] myIcons = new GameObject[13];
        int linksAmount = 0;

        int[] myTars = new int[40];

        for (int i = 0; i < 13; i++)
        {
            if (actualTier != _tree.talents[i].tier)
            {
                actualTier = _tree.talents[i].tier;
                myNum = 0;
                xOff = _tree.GetSpellsAmountFromTier(actualTier)*(-gridSize/2f);
            }
            else
                myNum++;

            myIcons[i] = Object.Instantiate(Resources.Load("TalentIcon"), new Vector3(xOff+(myNum*gridSize), 5f-gridSize-_tree.talents[i].tier*gridSize, -1), Quaternion.Euler(0,0,0)) as GameObject;
            myIcons[i].transform.SetParent(GameObject.Find("Canvas").transform);
            myIcons[i].transform.localScale = new Vector3(1, 1, 1);
            myIcons[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Talent.GetSpriteByTalent(_tree.talents[i]);
            myIcons[i].GetComponent<scrTalentIcon>().myTalent = _tree.talents[i];

            for (int j = 0; j < _tree.talents[i].linkAmount; j++)
            {
                if (_tree.talents[i].linkedWith[j] > -1)
                {

                    myLinks[linksAmount] = Object.Instantiate(Resources.Load("Link"), new Vector3(xOff + (myNum * gridSize), 5f - gridSize - _tree.talents[i].tier * gridSize, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                    myLinks[linksAmount].transform.SetParent(GameObject.Find("Canvas").transform);
                    myLinks[linksAmount].transform.localScale = new Vector3(1, 1, 1);
                    myLinks[linksAmount].transform.SetSiblingIndex(1);
                    myTars[linksAmount] = _tree.talents[i].linkedWith[j];
                    linksAmount++;
                }
            }
        }

        for (int j=0; j<linksAmount; j++)
        {
            if (myLinks[j] != null)
            {
                Vector3 vectorToTarget = myIcons[myTars[j]].transform.position - myLinks[j].transform.position;
                float distance = vectorToTarget.magnitude;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                myLinks[j].transform.rotation = Quaternion.Slerp(myLinks[j].transform.rotation, q, 100f);
                myLinks[j].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(distance*100, 20);
            }
        }
    }

    public void ResetTalents()
    {
        tree.Reset();
    }
}