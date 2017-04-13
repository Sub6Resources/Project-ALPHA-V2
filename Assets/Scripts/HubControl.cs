using UnityEngine;
using System.Collections;

public class HubControl : MonoBehaviour {

    public GameObject[] rooms;
    public GameObject[] hallways;

    public Sprite[] greySprites;
    public Sprite[] redSprites;
    public Sprite[] greenSprites;

    public bool powerMode = false;

    public PowerManager powerManager;


    public SystemManager system;

    public string RoomOne = "room1";
    public string RoomTwo = "room2";
    public string RoomThree = "room3";
    public string RoomFour = "room4";
    public string RoomFive = "room5";
    public string RoomSix = "room6";
    public string RoomSeven = "room7";
    public string RoomEight = "room8";
    public string RoomMystery = "room?";
    public string RoomTen = "room10";
    public string RoomEleven = "room11";
    public string Bathroom = "bathroom";
    public string OfflineRoom = "offlineroom";
    public string HalfB = "halfb";

    public static int BATHROOM = 0;
    public static int AIROOM = 1;
    public static int DIGLIBRARY = 2;
    public static int GREENROOM = 3;
    public static int ALPHASTORAGE = 4;
    public static int BETASTORAGE = 5;
    public static int PUMPSYSTEM = 6;
    public static int WATERFILTRATION = 7;
    public static int COMMROOM = 8;
    public static int BETAQUARTERS = 9;
    public static int CO2FILTERING = 10;
    public static int SHIELDGENERATOR = 11;
    public static int RADAR = 11;
    public static int AIRLOCK = 13;
    public static int SOLARPANEL = 14;
    public static int BETAHUB = 15;
    public static int MYSTERY = 16;
    

    // Use this for initialization
    void Start () {
        powerManager = FindObjectOfType<PowerManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(powerMode)
        {
            //update colors
            setToPowerMode();
        }
	}

    public static string GetRoomName(int ROOMID)
    {
        if(ROOMID == 0)
        {
            return "Bathroom";
        }
        if (ROOMID == 1)
        {
            return "AI Room";
        }
        if (ROOMID == 2)
        {
            return "Beta Hub";
        }
        if (ROOMID == 3)
        {
            return "Green Room";
        }
        if (ROOMID == 4)
        {
            return "Alpha Storage";
        }
        if (ROOMID == 5)
        {
            return "Beta Storage";
        }
        if (ROOMID == 6)
        {
            return "Pump System";
        }
        if (ROOMID == 7)
        {
            return "Water Filtration";
        }
        if (ROOMID == 8)
        {
            return "Communications Room";
        }
        if (ROOMID == 9)
        {
            return "Beta Quarters";
        }
        if (ROOMID == 10)
        {
            return "C02 Filtering";
        }
        if (ROOMID == 11)
        {
            return "Shield Generator Room";
        }
        if (ROOMID == 12 || ROOMID == 11)
        {
            return "Radar";
        }
        if (ROOMID == 13)
        {
            return "Air Lock";
        }
        if (ROOMID == 14)
        {
            return "Solar Panel";
        }
        if (ROOMID == 15)
        {
            return "Beta Hub";
        }
        if (ROOMID == 16)
        {
            return "Mystery Room";
        } else
        {
            return "ai.exe error...room does not exist......";
        }
    }
    public void setToPowerMode()
    {
        powerMode = true;
        for (int i = 0; i < rooms.Length - 2; i++)
        {
            if (powerManager.isRoomPowered(i))
            {
                rooms[i].GetComponent<SpriteRenderer>().sprite = greenSprites[i];
            }
            else
            {
                rooms[i].GetComponent<SpriteRenderer>().sprite = greySprites[i];
            }
        }
    }

    public void setToNormalMode()
    {
        powerMode = false;
        for(int i = 0; i < rooms.Length - 2; i++)
        {
            rooms[i].GetComponent<SpriteRenderer>().sprite = greenSprites[i];
        }
    }
}
