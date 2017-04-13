using UnityEngine;
using System.Collections;

public class CPU : MonoBehaviour {

    private CPUTextController cpuTextControl;

	// Use this for initialization
	void Start () {
        cpuTextControl = FindObjectOfType<CPUTextController>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public static string getReply(string input)
    {
        return "ai.exe is not working at the moment...";
    }
}
