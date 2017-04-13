using UnityEngine;
using System.Collections;

public class WarningSystem : MonoBehaviour {

    private StationLighting lightSystem;
    private TimeManager timeManager;
    public bool debugMode;

	// Use this for initialization
	void Start () {
        lightSystem = FindObjectOfType<StationLighting>();
        timeManager = FindObjectOfType<TimeManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void warn(string message)
    {
        if(timeManager.timeUpdated) {
        //Debug.Log("Warning: "+message);
        lightSystem.cpuwarning = true;
        }
    }

    public void error(string message)
    {
        if (timeManager.timeUpdated)
        {
            //Debug.Log("Error: " + message);
            lightSystem.cpuwarning = true;
        }
    }
    public void redalert(string message)
    {
        if (timeManager.timeUpdated)
        {
            lightSystem.cpuerror = true;
            //Debug.Log("RED ALERT: " + message);
        }
    }
    public void bluealert(string message)
    {
        if (timeManager.timeUpdated)
        {
            lightSystem.bluealert = true;
            //Debug.Log("RED ALERT: " + message);
        }
    }
    public void greenalert(string message)
    {
        if (timeManager.timeUpdated)
        {
            lightSystem.greenalert = true;
            //Debug.Log("RED ALERT: " + message);
        }
    }
}
