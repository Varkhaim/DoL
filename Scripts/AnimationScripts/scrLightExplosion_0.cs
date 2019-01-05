using UnityEngine;
using System.Collections;

public class scrLightExplosion_0 : MonoBehaviour {

    float size = 0f;
    bool growth = true;

	void Start ()
    {
        transform.localScale = new Vector3(0, 0);
    }

	void Update ()
    {
	    size = (growth) ? size + 0.05f : size - 0.05f;
        if (size > 1.25f)
            growth = false;
        transform.localScale = new Vector3(size,size);
        if (size < 0f)
            Destroy(gameObject);
	}
}
