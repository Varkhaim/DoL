using UnityEngine;
using System.Collections;

public class particleClean : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (!GetComponent<ParticleSystem>().IsAlive())
		{
			Destroy (gameObject);
		}
	}
}
