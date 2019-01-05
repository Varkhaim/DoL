using UnityEngine;
using System.Collections;

public class BuffScript : MonoBehaviour {

	public GameObject myParent;
	public GameObject mytext;
	public int myid;

    int timer = 30;
    float scale = 1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer--;
            if (timer > 15)
                scale += 0.1f;
            else
                scale -= 0.1f;
            transform.localScale = new Vector3(scale, scale);
            if (timer <= 0)
                transform.localScale = new Vector3(1, 1);
        }
    }
}
