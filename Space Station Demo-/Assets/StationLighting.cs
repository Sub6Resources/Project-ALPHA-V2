using UnityEngine;
using System.Collections;

public class StationLighting : MonoBehaviour {
    public GameObject[] lights;
    public bool redalert;
    public bool yellowalert;
    public bool greenalert;
    public bool bluealert;
    public bool blackalert;
    public bool blackout;
    public bool magentaalert;
    public bool grayalert;
    public bool clearalert;
    TimeManager timeManager;

    public static Color Red = Color.red;
    public static Color White = Color.white;
    public static Color Yellow = Color.yellow;
    public static Color Green = Color.green;
    public static Color Blue = Color.blue;
    public static Color Black = Color.black;
    public static Color Magenta = Color.magenta;
    public static Color Gray = Color.gray;
    public static Color Grey = Color.grey;
    public static Color Clear = Color.clear;
    public static Color Orange = Color.red + Color.yellow;

    public Color lightcolor = White;
    public Color lastFramelightcolor;
	// Use this for initialization
	void Start () {
        timeManager = FindObjectOfType<TimeManager>();
	}
	
	// Update is called once per frame
	void Update () {

       
        if(blackout)
        {
            lightcolor = StationLighting.Black;
        }
        else if (redalert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Red)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Red;
                }
            }

        }
        else if (yellowalert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Yellow)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Yellow;
                }
            }
        }
        else if (greenalert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Green)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Green;
                }
            }
        }
        else if (bluealert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Blue)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Blue;
                }
            }
        }
        else if (blackalert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Black)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Black;
                }
            }
        }
        else if (magentaalert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Magenta)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Magenta;
                }
            }
        }
        else if (grayalert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Gray)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Grey;
                }
            }
        }
        else if (clearalert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Clear)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Clear;
                }
            }
        }
        if(!redalert && !blackout && !greenalert && !yellowalert && !bluealert && !blackalert && !magentaalert && !grayalert && !clearalert)
        {
            lightcolor = StationLighting.White;
        }

        if (lastFramelightcolor != lightcolor)
        {
            for(int i = 0; i < lights.Length; i++)
            {
                //lights[i].GetComponent<Light>().color = UnityEngine.Color.Lerp(lastFramelightcolor, lightcolor, Time.deltaTime);
                lights[i].GetComponent<Light>().color = lightcolor;
            }
            lastFramelightcolor = lightcolor;
        }

        
        
	}
}
