using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public int time;
    int timeOfLastFrame;
    int timeOfThisFrame;
    public bool timeUpdated;
    public int timeToStartDebug = 0;
    public bool timeDebug;
    private bool paused;

    private Text gameObjectText;

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
        print("time is " + time);
        if (timeDebug)
        {
            time = timeToStartDebug;
            print("time is now " + time);
        }
        
#else
#endif
        gameObjectText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        timeUpdated = false;
        if (!paused)
        {
            
            timeOfThisFrame = (int)Time.realtimeSinceStartup;
            if (timeOfThisFrame > timeOfLastFrame)
            {
                time += 1;
                timeUpdated = true;
            }
            UpdateUI();
            timeOfLastFrame = timeOfThisFrame;
        }
	}

    void OnPauseGame()
    {
        paused = true;
    }

    void OnResumeGame()
    {
        paused = false;
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
        gameObjectText.text = timeString+time.ToString();
    }
    public void setTime(int newtime)
    {
        print("Time is set to " + newtime + " from " + time);
        time = newtime;
        timeUpdated = true;
    }
}
