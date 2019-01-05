using UnityEngine;
using System.Collections;

public class DebuffScript : MonoBehaviour {
	
	public GameObject myParent;
    public Debuff parentDebuff;
	public int myid;

    // animacja
    int timer = 30;
    float scale = 1f;
	
	void Start () {
		
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

    public void Animate()
    {
        timer = 30;
        scale = 1f;
    }

    public void SetParent(Debuff _parent)
    {
        parentDebuff = _parent;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            parentDebuff.Dispel();
        }
    }
}
