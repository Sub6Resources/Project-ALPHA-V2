using UnityEngine;
using System.Collections;

public class StationLighting : MonoBehaviour {
    public GameObject[] lights;
    public GameObject[] airoomlights;
    public GameObject[] hallwayonetotwolights;

    public GameObject[] cpulights;
    public bool redalert;
    public bool yellowalert;
    public bool greenalert;
    public bool bluealert;
    public bool blackalert;
    public bool blackout;
    public bool magentaalert;
    public bool grayalert;
    public bool clearalert;
    public bool orangealert;
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
    public static Color Orange = new Color(255f/255f, 81f/255f, 0f/255f, 1f);

    private Color lightcolor = White;
    private Color lastFramelightcolor;

    private Color cpulightcolor = Green;
    private Color cpulastframelightcolor;

    public bool cpuwarning;
    public bool cpuerror;

    //these variables go in this order: airoom (0), onetotwohallway(1)
    private Color[] roomcolors = new Color[2];
    private Color[] lastroomcolors = new Color[2];
    public bool[] roomalerts;
    //end of those variables.

    public Color alertcolor;

    
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
        else if (orangealert)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (lightcolor == StationLighting.Orange)
                {
                    lightcolor = StationLighting.White;
                }
                else
                {
                    lightcolor = StationLighting.Orange;
                }
            }
        }
        if (!redalert && !blackout && !greenalert && !yellowalert && !bluealert && !blackalert && !magentaalert && !grayalert && !clearalert && !orangealert)
        {
            lightcolor = StationLighting.White;
        }

        cpuAlertLights();
        if (lastFramelightcolor != lightcolor)
        {
            for(int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<Light>().color = lightcolor;
            }
            lastFramelightcolor = lightcolor;
        }

        if(roomalerts[0])
            AIROOMALERT(alertcolor);
        if (roomalerts[1])
            HALLWAYONETOTWOALERT(alertcolor);

        updateRoomLights();
        
	}

    public void AIROOMALERT(Color color)
    {

        lastroomcolors[0] = roomcolors[0];
        if (timeManager.timeUpdated)
        {
            if (roomcolors[0] == color)
            {
                roomcolors[0] = StationLighting.White;
            }
            else
            {
                roomcolors[0] = color;
            }
        }
        
    }

    public void HALLWAYONETOTWOALERT(Color color)
    {
        lastroomcolors[1] = roomcolors[1];
        if (timeManager.timeUpdated)
        {
            if (roomcolors[1] == color)
            {
                roomcolors[1] = StationLighting.White;
            }
            else
            {
                roomcolors[1] = color;
            }
        }
        
    }

    void updateRoomLights()
    {
        
        for (int i = 0; i < roomcolors.Length; i++)
        {
            if (roomcolors[i] != lastroomcolors[i])
            {
                if (i == 0)
                {
                    changeLightColor(airoomlights, roomcolors[i]);
                } else if(i == 1)
                {
                    changeLightColor(hallwayonetotwolights, roomcolors[i]);
                }
                
            }
        }
       
    }

    void changeLightColor(GameObject[] room, Color color)
    {
        for(int i = 0; i < room.Length; i++)
        {
            room[i].GetComponent<Light>().color = color;
        }
    }

    void cpuAlertLights()
    {
        if(cpuerror)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (cpulightcolor == StationLighting.Red)
                {
                    cpulightcolor = StationLighting.Green;
                }
                else
                {
                    cpulightcolor = StationLighting.Red;
                }
            }
        } else if(cpuwarning)
        {
            if (timeManager.timeUpdated) //This is called every second
            {
                if (cpulightcolor == StationLighting.Yellow)
                {
                    cpulightcolor = StationLighting.Green;
                }
                else
                {
                    cpulightcolor = StationLighting.Yellow;
                }
            }
        } else
        {
            cpulightcolor = Green;
        }
        if (cpulastframelightcolor != cpulightcolor)
        {
            for (int i = 0; i < cpulights.Length; i++)
            {
                cpulights[i].GetComponent<Light>().color = cpulightcolor;
            }
            cpulastframelightcolor = cpulightcolor;
        }
    }
}

