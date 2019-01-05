using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatLogScript : MonoBehaviour
{

    private float lifetime = 120f;
    public bool isCrit;
    float xstr, ystr;
    // Use this for initialization
    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = new Vector3(transform.position.x, transform.position.y);
        if (isCrit)
        {
            GetComponent<Text>().fontSize += 4;
            lifetime += 30f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0)
            lifetime--;
        else
            Destroy(gameObject);

        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, lifetime / 60f);

        if (!isCrit)
        {
            transform.Translate(new Vector3(0, 0.008f, 0));
        }

        /*
        if (!isCrit)
        {
            transform.Translate(new Vector3(0, 0.01f, 0));
        }
        else
        {
            transform.Translate(new Vector3((Random.Range(0f, 2f) - 1f) / 100, (Random.Range(0f, 2f) - 1f) / 100, 0));
        }*/
    }
}
