using UnityEngine;
using System.Collections;

public class temptriggerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 pos = Input.mousePosition;
			pos = Camera.main.ScreenToWorldPoint(pos);
			pos.z = -5;
			transform.position = Vector3.MoveTowards(transform.position, pos, 1f); 
		}
		if (Input.GetMouseButtonUp(0))
		{
			Destroy(gameObject);
		}
	}
}
