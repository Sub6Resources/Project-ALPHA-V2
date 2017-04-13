using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour {

    public GameObject bathroomDoor;


    //AIRLOCKS
    public GameObject airlockDoorOne;
    public GameObject airlockDoorTwo;
    public GameObject emergencyAirlockDoorOne;
    public GameObject emergencyAirlockDoorTwo;



    //CONTROLLERS------------------------------------------------------------------------------------------------
    private DoorController bathroomDoorC;


    //AIRLOCKS
    private DoorController airlockDoorOneC;
    private DoorController airlockDoorTwoC;
    private DoorController emergencyAirlockDoorOneC;
    private DoorController emergencyAirlockDoorTwoC;


    //STATICS----------------------------------------------------------------------------------------------------
    public const int BATHROOM_DOOR = 0;
    public const int AIRLOCK_DOOR_ONE = 1;
    public const int AIRLOCK_DOOR_TWO = 2;
    public const int EMERGENCY_AIRLOCK_DOOR_ONE = 3;
    public const int EMERGENCY_AIRLOCK_DOOR_TWO = 4;


    // Use this for initialization
    void Start () {
        //INITIALIZE CONTROLLERS
        bathroomDoorC = bathroomDoor.GetComponent<DoorController>();
        //AIRLOCKS
        airlockDoorOneC = airlockDoorOne.GetComponent<DoorController>();
        airlockDoorTwoC = airlockDoorTwo.GetComponent<DoorController>();
        emergencyAirlockDoorOneC = emergencyAirlockDoorOne.GetComponent<DoorController>();
        emergencyAirlockDoorTwoC = emergencyAirlockDoorTwo.GetComponent<DoorController>();
	}
	
	public bool Open(int door)
    {
        switch(door)
        {
            case BATHROOM_DOOR:
                if(bathroomDoorC.open)
                {
                    return false;
                }
                bathroomDoorC.open = true;
                return true;
            case AIRLOCK_DOOR_ONE:
                if(airlockDoorOneC.open)
                {
                    return false;
                }
                airlockDoorOneC.open = true;
                return true;
            case AIRLOCK_DOOR_TWO:
                if(airlockDoorTwoC.open)
                {
                    return false;
                }
                airlockDoorTwoC.open = true;
                return true;
            case EMERGENCY_AIRLOCK_DOOR_ONE:
                if(emergencyAirlockDoorOneC.open)
                {
                    return false;
                }
                emergencyAirlockDoorOneC.open = true;
                return true;
            case EMERGENCY_AIRLOCK_DOOR_TWO:
                if(emergencyAirlockDoorTwoC.open)
                {
                    return false;
                }
                emergencyAirlockDoorTwoC.open = true;
                return true;
        }
        return false;
    }

    public bool Close(int door)
    {
        switch (door)
        {
            case BATHROOM_DOOR:
                if(!bathroomDoorC.open)
                {
                    return false;
                }
                bathroomDoorC.open = false;
                return true;
            case AIRLOCK_DOOR_ONE:
                if(!airlockDoorOneC.open)
                {
                    return false;
                }
                airlockDoorOneC.open = false;
                return true;
            case AIRLOCK_DOOR_TWO:
                if(!airlockDoorTwoC.open)
                {
                    return false;
                }
                airlockDoorTwoC.open = false;
                return true;
            case EMERGENCY_AIRLOCK_DOOR_ONE:
                if(!emergencyAirlockDoorOneC.open)
                {
                    return false;
                }
                emergencyAirlockDoorOneC.open = false;
                return true;
            case EMERGENCY_AIRLOCK_DOOR_TWO:
                if(!emergencyAirlockDoorTwoC.open)
                {
                    return false;
                }
                emergencyAirlockDoorTwoC.open = false;
                return true;
        }
        return false;
    }

    public void Alter(int door)
    {
        switch (door)
        {
            case BATHROOM_DOOR:
                bathroomDoorC.open = !bathroomDoorC.open;
                break;
            case AIRLOCK_DOOR_ONE:
                airlockDoorOneC.open = !airlockDoorOneC.open;
                break;
            case AIRLOCK_DOOR_TWO:
                airlockDoorTwoC.open = !airlockDoorTwoC.open;
                break;
            case EMERGENCY_AIRLOCK_DOOR_ONE:
                emergencyAirlockDoorOneC.open = !emergencyAirlockDoorOneC.open;
                break;
            case EMERGENCY_AIRLOCK_DOOR_TWO:
                emergencyAirlockDoorTwoC.open = !emergencyAirlockDoorTwoC.open;
                break;
        }
    }
}
