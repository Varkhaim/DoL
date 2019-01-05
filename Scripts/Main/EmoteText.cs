using UnityEngine;
using System.Collections;

public class EmoteText : MonoBehaviour
{
    private float alpha = 0;
    private int phase = 0;
    private int timer = 20;

    private float r, g, b;

    void Start()
    {
        
    }

    public void SetColor(Color _color)
    {
        r = _color.r;
        g = _color.g;
        b = _color.b;
    }

    void Update()
    {
        if (phase == 0)
        {
            transform.Translate(new Vector3(0, -0.02f, 0));
            alpha += 0.05f;
            if (timer-- <= 0)
            {
                timer = 120;
                phase = 1;
                alpha = 1f;
            }
        }
        else
        if (phase == 1)
        {
            transform.Translate(new Vector3(0, -0.005f, 0));
            if (timer-- <= 0)
            {
                phase = 2;
            }
        }
        else
        if (phase == 2)
        {
            alpha -= 0.1f;
            if (alpha <= 0)
                Destroy(gameObject);
        }
        GetComponent<TextMesh>().color = new Color(r, g, b, alpha);
    }
}
