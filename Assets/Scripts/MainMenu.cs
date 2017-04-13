using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        SceneManager.LoadScene("mainscene");
    }
    public void Instructions()
    {
        SceneManager.LoadScene("instructions");
    }
    public void Credits()
    {
        SceneManager.LoadScene("credits");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
