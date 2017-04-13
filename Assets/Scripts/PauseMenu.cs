using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public bool isPaused;

	// Use this for initialization
	void Start () {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if(isPaused)
        {
            gameObject.SetActive(false);
        }
	}
}
