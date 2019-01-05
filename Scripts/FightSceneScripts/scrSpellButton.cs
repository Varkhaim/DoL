using UnityEngine;
using System.Collections;

public class scrSpellButton : MonoBehaviour
{
    bool grow = false;
    float size = 1f;
    public GameObject ring;
    float maxval = 2f;

	void Start ()
    {
	
	}

    public void Animate(float val)
    {
        grow = true;
        maxval = val;

        transform.SetAsLastSibling();
        ring.transform.SetAsLastSibling();
        
    }

	void Update ()
    {
	    if (grow)
        {
            size += 0.1f;
            if (size > maxval)
                grow = false;
        }
        else
        {
            if (size > 1f)
                size -= 0.1f;
        }
        transform.localScale = new Vector3(size, size);
        ring.transform.localScale = new Vector3(size, size);
    }
}
