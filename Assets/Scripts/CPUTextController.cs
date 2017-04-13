using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class CPUTextController : MonoBehaviour {
    public string computerText = "";
    public bool testMode = false;
    private TimeManager timemanager;
    private RectTransform theTransform;
    public float numberOfLines = 1;
    public int numberOfLinesBeforeWrap = 10;
    float lastFrameNOL;
    bool moveThisFrame = false;
    public string input;
    public Text inputText;
    private CameraSwitcher cameraManager;
    private InventoryChanger inventoryChanger;
    private GameManager gameManager;
    private DoorManager doorManager;
    private PlayerMove player;
    public bool cpuEnabled;
	// Use this for initialization
	void Start () {
        timemanager = FindObjectOfType<TimeManager>();
        theTransform = GetComponent<RectTransform>();
        cameraManager = FindObjectOfType<CameraSwitcher>();
        inventoryChanger = FindObjectOfType<InventoryChanger>();
        gameManager = FindObjectOfType<GameManager>();
        doorManager = FindObjectOfType<DoorManager>();
        player = FindObjectOfType<PlayerMove>();
        cpuEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        updateTextPosition();
        if (cpuEnabled)
        {
            string currentText = GetComponent<Text>().text;
            if (computerText != currentText)
            {
                GetComponent<Text>().text = computerText;
            }
            if (testMode && timemanager.timeUpdated)
            {
                WriteLine("ai.exe is conducting a self check...");
            }
            
            foreach (char c in Input.inputString)
            {
                print(c);
                if (c == "\b"[0])
                {
                    if (input.Length != 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                    }
                }
                else if (c == "\n"[0] || c == "\r"[0])
                {
                    print("CPU input: " + input);
                    GetInput(input);
                    input = "";

                }
                else
                {
                    input += c;
                }
                inputText.text = input;
            }
        }

    }
    public void WriteLine(string concatenate)
    {
        updateTextPosition();
        computerText += "\n" + concatenate;
        updateTextPosition();
        
    }
    public void Write(string concatenate)
    {
        computerText += concatenate;
    }
    public void updateTextPosition()
    {
        float amountToMove = 0;
        numberOfLines = LayoutUtility.GetPreferredHeight(theTransform) / 22 - 1;
        if (lastFrameNOL != numberOfLines)
        {
            amountToMove = numberOfLines - lastFrameNOL;
            moveThisFrame = true;
            numberOfLinesBeforeWrap = (Screen.height / 22) - 4;
        }
        if (numberOfLines >= numberOfLinesBeforeWrap && moveThisFrame)
        {
            moveThisFrame = false;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y + (22 * (amountToMove)));
            //GetComponent<Transform>().position = new Vector2(GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y + (1 / 8));
        }
        lastFrameNOL = numberOfLines;
    }
    public void GetInput(string theInput)
    {
        Random randy = new Random();
        WriteLine("--" + theInput);
        updateTextPosition();
        theInput = theInput.ToLower();
        char[] myCharCollection = new char[theInput.Length];
        myCharCollection = theInput.ToCharArray();
        if (!theInput.Contains(','))
        {
            theInput = new string(myCharCollection.Where(c => !char.IsPunctuation(c)).ToArray());
        }
        switch (theInput)
        {
            //TESTMODE COMMANDS----------------------------------------------------------------------------------------
            case "testmode":
                WriteLine(">>>testMode starting...");
                testMode = true;
                break;
            case "testmodeoff":
                testMode = false;
                WriteLine(">>>testMode stopped...");
                break;
            //CONVERSATIONAL COMMANDS----------------------------------------------------------------------------------
            case "thecakeisalie":
                WriteLine(">>>Well, for my sister GLADoS that might be true, but I'll give you all the cake you want!");
                break;
            case "read a book":
                WriteLine(">>>I'm a computer, I can't read. You can check out the Digital Library though.");
                break;
            //INVENTORY DEBUG COMMANDS---------------------------------------------------------------------------------
            case "give player something":
                if (inventoryChanger.GivePlayer(InventoryManager.ERK_ITEM))
                {
                    WriteLine(">>>Giving Player an ERK... [OK]");
                } else
                {
                    WriteLine("Giving Player an ERK... [FAIL]");
                }
                break;
            //DOOR COMMANDS-------------------------------------------------------------------------------------------
            //UNLOCK DOORS
            case "opn7169":
                WriteLine("UNLOCKING DOORS...");
                if(gameManager.commandEntered)
                {
                    Write(" [FAIL--ALREADY UNLOCKED]");
                } else
                {
                    gameManager.commandEntered = true;
                    Write(" [OK]");
                }
                break;
            //OPEN DOORS----------------------------------------
            case "open bathroom door":
                if (doorManager.Open(DoorManager.BATHROOM_DOOR))
                {
                    Write(" [OK]");
                } else
                {
                    Write(" [FAIL--DOOR ALREADY OPEN]");
                }
                break;
            case "open airlock door one":
                if (doorManager.Open(DoorManager.AIRLOCK_DOOR_ONE))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY OPEN]");
                }
                break;
            case "open airlock door two":
                if (doorManager.Open(DoorManager.AIRLOCK_DOOR_TWO))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY OPEN]");
                }
                break;
            case "open emergency airlock door one":
                if (doorManager.Open(DoorManager.EMERGENCY_AIRLOCK_DOOR_ONE))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY OPEN]");
                }
                break;
            case "open emergency airlock door two":
                if (doorManager.Open(DoorManager.EMERGENCY_AIRLOCK_DOOR_TWO))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY OPEN]");
                }
                break;
            //CLOSE DOORS----------------------------------------
            case "close bathroom door":
                if (doorManager.Close(DoorManager.BATHROOM_DOOR))
                {
                    Write(" [OK]");
                } else
                {
                    Write(" [FAIL--DOOR ALREADY CLOSED]");
                }
                break;
            case "close airlock door one":
                if (doorManager.Close(DoorManager.AIRLOCK_DOOR_ONE))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY CLOSED]");
                }
                break;
            case "close airlock door two":
                if (doorManager.Close(DoorManager.AIRLOCK_DOOR_TWO))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY CLOSED]");
                }
                break;
            case "close emergency airlock door one":
                if (doorManager.Close(DoorManager.EMERGENCY_AIRLOCK_DOOR_ONE))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY CLOSED]");
                }
                break;
            case "close emergency airlock door two":
                if (doorManager.Close(DoorManager.EMERGENCY_AIRLOCK_DOOR_TWO))
                {
                    Write(" [OK]");
                }
                else
                {
                    Write(" [FAIL--DOOR ALREADY CLOSED]");
                }
                break;
            
            //VARIABLE COMMANDS----------------------------------------------------------------------------------------------
            default:
                //TODO check unconstant stuff
                //TELEPORTATION--------------------------------------------------------------------------------------------------
                if(theInput.Contains("tp"))
                {
                    //A really long line of code that does a lot of stuff.
                    player.setPlayerPosition(Vector3String.StringToVector3(theInput.Split(' ')[1]));
                    break;
                }
                //TIME
                if(theInput.Contains("timeset"))
                {
                    int timeToSet;
                    if(int.TryParse(theInput.Split(' ')[1], out timeToSet))
                    {
                        timemanager.setTime(timeToSet);
                        WriteLine(" [OK]");
                    } else
                    {
                        WriteLine(" [Failed -- Not An Integer]");
                    }
                    break;
                }
                WriteLine(">>>ai.exe error. Unable to interpret [" + theInput + "]");
                break;
        }
        updateTextPosition();
       
    }
    void OnCPUEnabled()
    {
        updateTextPosition();
        cpuEnabled = true;
    }
    void OnCPUDisabled()
    {
        cpuEnabled = false;
    }
}
