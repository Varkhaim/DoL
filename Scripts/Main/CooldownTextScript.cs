using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CooldownTextScript : MonoBehaviour
{
    public float cooldown;

    private float animationTimer = 0;
    private float baseSize;

    void Start()
    {
        baseSize = GetComponent<Text>().fontSize;
    }

    public void Animate()
    {
        animationTimer = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTimer > 0f)
        {
            animationTimer--;
            GetComponent<Text>().fontSize = (int)(baseSize + (animationTimer/60f));
        }
        else
            GetComponent<Text>().fontSize = (int)baseSize;
    }
}
