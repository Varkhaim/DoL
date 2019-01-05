using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InvExitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        SceneManager.LoadScene("Scenes/DescendantScene");
    }
}
