using UnityEngine;
using System.Collections;

public class scrMapController : MonoBehaviour
{

	public void OnStartClick()
    {
        WorldMapCore.WMCore.GoToDescendantScene();
    }
}
