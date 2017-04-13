using UnityEngine;
using System.Collections;

public class PowerManager : MonoBehaviour {

    public bool roomZeroPowered;
    public bool roomOnePowered;
    public bool roomTwoPowered;
    public bool roomThreePowered;
    public bool roomFourPowered;
    public bool roomFivePowered;
    public bool roomSixPowered;
    public bool roomSevenPowered;
    public bool roomEightPowered;
    public bool roomNinePowered;
    public bool roomTenPowered;
    public bool roomElevenPowered;
    public bool roomTwelvePowered;
    public bool roomThirteenPowered;
    public bool roomFourteenPowered;
    public bool roomFifteenPowered;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isRoomPowered(int room)
    {
        switch(room)
        {
            case 0:
                return roomZeroPowered;
            case 1:
                return roomOnePowered;
            case 2:
                return roomTwoPowered;
            case 3:
                return roomThreePowered;
            case 4:
                return roomFourPowered;
            case 5:
                return roomFivePowered;
            case 6:
                return roomSixPowered;
            case 7:
                return roomSevenPowered;
            case 8:
                return roomEightPowered;
            case 9:
                return roomNinePowered;
            case 10:
                return roomTenPowered;
            case 11:
                return roomElevenPowered;
            case 12:
                return roomTwelvePowered;
            case 13:
                return roomThirteenPowered;
            case 14:
                return roomFourteenPowered;
            case 15:
                return roomFifteenPowered;
            default:
                return false;
        }
    }
    public bool[] valuesINeedSaved()
    {
        bool[] returnBool = new bool[] {roomZeroPowered, roomOnePowered, roomTwoPowered, roomThreePowered, roomFourPowered, roomFivePowered, roomSixPowered, roomSevenPowered, roomEightPowered, roomNinePowered, roomTenPowered, roomElevenPowered, roomTwelvePowered, roomThirteenPowered, roomFourteenPowered, roomFifteenPowered};
        print("Saving power with the values: ");
        foreach (bool i in returnBool)
        {
            print(i);
        }
        return returnBool;
    }

    public void loadValues(bool[] values)
    {
        print("Loading power with the values: ");
        foreach(bool i in values)
        {
            print(i);
        }
        roomZeroPowered = values[0];
        roomOnePowered = values[1];
        roomTwoPowered = values[2];
        roomThreePowered = values[3];
        roomFourPowered = values[4];
        roomFivePowered = values[5];
        roomSixPowered = values[6];
        roomSevenPowered = values[7];
        roomEightPowered = values[8];
        roomNinePowered = values[9];
        roomTenPowered = values[10];
        roomElevenPowered = values[11];
        roomTwelvePowered = values[12];
        roomThirteenPowered = values[13];
        roomFourteenPowered = values[14];
        roomFifteenPowered = values[15];
    }
}
