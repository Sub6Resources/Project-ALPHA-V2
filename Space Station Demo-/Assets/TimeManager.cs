using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public static int time;
    int timeOfLastFrame;
    int timeOfThisFrame;
    public bool timeUpdated;

	// Use this for initialization
	void Start () {
        time = PlayerPrefs.GetInt("SST");
	}
	
	// Update is called once per frame
	void Update () {
        timeUpdated = false;
        timeOfThisFrame = (int)Time.realtimeSinceStartup;
        if(timeOfThisFrame > timeOfLastFrame)
        {
            time += 1;
            timeUpdated = true;
        }
        UpdateUI();
        PlayerPrefs.SetInt("SST", time);
        timeOfLastFrame = timeOfThisFrame;
	}

    void UpdateUI()
    {
        string timeString = "Time Since Boot: 000000000";
        if(time > 0)
        {
            timeString = "Time Since Boot: 00000000";
        }
        if (time > 9)
        {
            timeString = "Time Since Boot: 0000000";
        }
        if (time > 99)
        {
            timeString = "Time Since Boot: 000000";
        }
        if (time > 999)
        {
            timeString = "Time Since Boot: 00000";
        }
        if (time > 9999)
        {
            timeString = "Time Since Boot: 0000";
        }
        if (time > 99999)
        {
            timeString = "Time Since Boot: 000";
        }
        if (time > 999999)
        {
            timeString = "Time Since Boot: 00";
        }
        if (time > 9999999)
        {
            timeString = "Time Since Boot: 0";
        }
        if (time > 99999999)
        {
            timeString = "Time Since Boot: ";
        }
        if (time > 999999999)
        {
            timeString = "SYSTEM OVERLOAD: YOU TOOK WAAAAAAY TOO MUCH TIME.... I DON'T EVEN KNOW HOW THIS IS EVEN POSSIBLE";
            //TODO game over
        }
        gameObject.GetComponent<Text>().text = timeString+time.ToString();
    }
}
