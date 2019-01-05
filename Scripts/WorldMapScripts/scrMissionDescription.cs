using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrMissionDescription : MonoBehaviour
{

    bool isAnimated = false;
    bool isFading = false;
    string content = "";

    float Alpha = 0f;

    private float alpha
    {
        get { return Alpha; }
        set
        {
            Alpha = value;
            UpdateVisibility();
        }
    }

    public void animate()
    {
        isAnimated = true;
        isFading = true;
    }

    public void SetContent(string _content)
    {
        content = _content;
        animate();
    }

    private void UpdateContent()
    {
        GetComponent<Text>().text = content;
    }

    void Update()
    {
        if (isAnimated)
        {
            if (isFading)
            {
                alpha -= 0.02f;
                if (alpha <= 0)
                {
                    isFading = false;
                    UpdateContent();
                }
            }
            else
            {
                alpha += 0.02f;
                if (alpha >= 1f)
                    isAnimated = false;
            }
        }
    }

    void UpdateVisibility()
    {
        GetComponent<Text>().canvasRenderer.SetAlpha(alpha);
    }
}
