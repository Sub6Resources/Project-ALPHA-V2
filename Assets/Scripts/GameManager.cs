using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private TimeManager timeManager;
    private CPUTextController cpu;
    private CameraSwitcher cameraManager;
    private PlayerMove playerPositionStuff;
    private MouseLook mousePosition;
    private SystemManager systemManager;
    private InventoryManager inventoryManager;
    private InventoryChanger inventoryChanger;
    private PowerManager powerManager;
    private MusicController musicController;
    private StationLighting lightManager;
    [SerializeField] GameObject player;
    public bool cpuEnabled = false;
    //first checkpoint stuff
    public bool stationInitialized = false;
    public bool firstCheckpoint = false;
    public bool firstDayNotShown = true;
    public bool firstDayIndicatorHidden = false;
    public bool debugMode = false;

    //second checkpoint stuff
    public bool secondCheckpoint = false;
    public bool helpTextShown = false;

    //third checkpoint stuff
    public bool thirdCheckpoint = false;

    //fourth checkpoint stuff
    public bool fourthCheckpoint = false;
    public bool commandEntered = false;

    //fifth checkpoint stuff
    public bool fifthCheckpoint = false;

    //sixth checkpoint stuff
    public bool sixthCheckpoint = false;
    public bool pumpGameComplete = false;

    //seventh checkpoint stuff
    public bool seventhCheckpoint = false;

    //eighth checkpoint stuff
    public bool eighthCheckpoint = false;

    //ninth checkpoint stuff
    public bool ninthCheckpoint = false;

    //tenth checkpoint stuff
    public bool tenthCheckpoint = false;




    //Emergency Mode
    public bool emergencyMessageShown = false;
    public bool startedLosing = false;
    public bool emergencyMode = false;
    public int timeOfImpact = int.MaxValue;

    public int playerInRoom = 0;

    public Image dayIndicatorBackground;
    public Text dayIndicatorText;

    public GameObject pauseMenu;

    public bool paused = false;

    public bool loadFromSave;
    public bool saveForReals;


	// Use this for initialization
	void Start () {
        timeManager = FindObjectOfType<TimeManager>();
        cpu = FindObjectOfType<CPUTextController>();
        cameraManager = FindObjectOfType<CameraSwitcher>();
        playerPositionStuff = FindObjectOfType<PlayerMove>();
        mousePosition = FindObjectOfType<MouseLook>();
        systemManager = FindObjectOfType<SystemManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryChanger = FindObjectOfType<InventoryChanger>();
        powerManager = FindObjectOfType<PowerManager>();
        musicController = FindObjectOfType<MusicController>();
        lightManager = FindObjectOfType<StationLighting>();
        dayIndicatorText.canvasRenderer.SetAlpha(0.0f);
        dayIndicatorBackground.canvasRenderer.SetAlpha(0.0f);
        pauseMenu.SetActive(paused);
        LoadSavedGame();
    }
	
	// Update is called once per frame
	void Update () {
        cpuEnabled = cameraManager.valuesINeedSaved(); //Returns whether the cpu camera is active or not

        int timeThisFrame = timeManager.time;


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        if(timeManager.timeUpdated)
        {
            if(timeThisFrame > 0 && !stationInitialized)
            {
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_ONE_INVENTORY, InventoryManager.ERK_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_ONE_INVENTORY, InventoryManager.ERK_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_ONE_INVENTORY, InventoryManager.BATTERY_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_ONE_INVENTORY, InventoryManager.BATTERY_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_ONE_INVENTORY, InventoryManager.BATTERY_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_TWO_INVENTORY, InventoryManager.ERK_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_TWO_INVENTORY, InventoryManager.ERK_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_TWO_INVENTORY, InventoryManager.BATTERY_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_TWO_INVENTORY, InventoryManager.BATTERY_ITEM);
                inventoryChanger.Give(InventoryChanger.BETASTORAGE_TWO_INVENTORY, InventoryManager.BATTERY_ITEM);
                inventoryChanger.GivePlayer(InventoryManager.FOOD_ITEM);
                inventoryChanger.GivePlayer(InventoryManager.FOOD_ITEM);
                inventoryChanger.GivePlayer(InventoryManager.WATER_ITEM);
                stationInitialized = true;
            }
            if(timeThisFrame > 2 && firstDayNotShown) //DAY 1
            {
                firstDayNotShown = false;
                CrossFadeInAnnouncement("Day One");
            }
            if (timeThisFrame > 5 && !firstDayIndicatorHidden) //DAY 1 Hide
            {
                firstDayIndicatorHidden = true;
                CrossFadeOutAnnouncement("Day One");
            }
            if(playerIn(1) && !firstCheckpoint && !helpTextShown)
            {
                StartCoroutine(ShowHelpText());
            }
            if (timeThisFrame > 0 && cpuEnabled && !firstCheckpoint) //Begin Printing Initial Instructions!!!!
            {
                print("First Checkpoint Activated!");
                StartCoroutine(DayOneTextOne());
                firstCheckpoint = true;
                //TODO unlock doors
            }
            if(firstCheckpoint && playerIn(3) && cpuEnabled && !secondCheckpoint) //User is in the Beta Storage now, print second set of instructions.
            {
                print("Second Checkpoint Activiated!");
                StartCoroutine(DayOneTextTwo());
                secondCheckpoint = true;
            }
            if(secondCheckpoint && !thirdCheckpoint && cpuEnabled && inventoryManager.Player.ERKAmount > 0 && inventoryManager.Player.BatteriesAmount > 0) //User has grabbed a battery and ERK, commence more instructions.
            {
                print("Third Checkpoint Activated!");
                StartCoroutine(DayOneTextThree());
                thirdCheckpoint = true;
            }
            if(thirdCheckpoint && !fourthCheckpoint && playerIn(1) && cpuEnabled) //User in room one, just finished first mission.
            {
                print("Fourth Checkpoint Activated!");
                StartCoroutine(DayOneTextFour());
                fourthCheckpoint = true;
            }
            if(fourthCheckpoint && !commandEntered && !fifthCheckpoint)
            {
                //Don't do anything, just reserving a spot
            }
            if(fourthCheckpoint && commandEntered && !fifthCheckpoint && cpuEnabled)
            {
                print("Fifth Checkpoint Activated!");
                StartCoroutine(DayOneTextFive());
                fifthCheckpoint = true;
            }
            if(fifthCheckpoint && playerIn(7) && !sixthCheckpoint && cpuEnabled)
            {
                print("Sixth Checkpoint Activated!");
                StartCoroutine(DayOneTextSix());
                sixthCheckpoint = true;
            }
            if(sixthCheckpoint && pumpGameComplete && playerIn(7) && cpuEnabled && !seventhCheckpoint)
            {
                print("Seventh Checkpoint Activiated");
                StartCoroutine(DayOneTextSeven());
                seventhCheckpoint = true;
            }
            if(seventhCheckpoint && playerIn(1) && cpuEnabled && !eighthCheckpoint)
            {
                print("Eight Checkpoint Activiated");
                StartCoroutine(DayOneTextEight());
                eighthCheckpoint = true;
            }
            if(eighthCheckpoint && playerIn(1) && cpuEnabled && !ninthCheckpoint) //add other stuff here
            {
                print("Ninth Checkpoint Activated!");
                StartCoroutine(DayOneTextNine());
                ninthCheckpoint = true;
            }
            if(ninthCheckpoint && playerIn(1) && cpuEnabled && !tenthCheckpoint) //add other stuff here
            {
                print("Tenth Checkpoint Activated!");
                StartCoroutine(DayOneTextTen());
                tenthCheckpoint = true;
            }
            //LOSING SCRIPTS---------------------------------------------------------------------------
            if(timeThisFrame > 150 && !tenthCheckpoint && !emergencyMode) //First challenge
            {
                emergencyMode = true;
                lightManager.redalert = true;
                musicController.stopSound(MusicController.MUSIC);
                musicController.startSound(MusicController.ALARM_SOUND);
            }
            if(emergencyMode && cpuEnabled && !emergencyMessageShown)
            {
                emergencyMessageShown = true;
                StartCoroutine(asteroidComing());
            }
            if(timeOfImpact < timeManager.time && !startedLosing)
            {
                startedLosing = true;
                lightManager.redalert = false;
                lightManager.blackout = true;
                player.GetComponent<PlayerMove>().enabled = false;
                musicController.startSound(MusicController.CRASH_SOUND);
                StartCoroutine(cameraManager.AsteroidImpact());
            }
            //WINNING------------------------------------------------------------------------------------
            if(timeThisFrame > 150 && tenthCheckpoint)
            {
                lightManager.redalert = false;
                lightManager.greenalert = true;
                StartCoroutine(WinRoutine());
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
	}
    IEnumerator ShowHelpText()
    {
        helpTextShown = true;
        CrossFadeInAnnouncement("Click on Objects to Interact");
        yield return new WaitForSeconds(3.0f);
        CrossFadeOutAnnouncement("Click on Objects to Interact");
    }
    IEnumerator DayOneTextOne()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine("~~~Welcome Back Scientist 202!~~~");
            yield return new WaitForSeconds(1.25f);
            cpu.WriteLine(">>>An Unknown object has struck Station AL-1681 and has caused a noticable split between two parts of the station. You were the only human being on your half of the station. (Now to be referred to as 'Half A').");
            yield return new WaitForSeconds(3.0f);
            cpu.WriteLine(">>>Thanks to an emergency airlock, you are alive and well. The status of the scientists on 'Half B' is unknown.");
            yield return new WaitForSeconds(1.5f);
            cpu.WriteLine(">>>The life support system on 'Half A' has been damaged beyond repair, and oxygen levels have significantly decreased since your last login.");
            yield return new WaitForSeconds(4.0f);
            cpu.WriteLine(">>>An order to advance to Room 3 has been created. You are currently in Room One.");
            yield return new WaitForSeconds(3.0f);
            cpu.WriteLine("INSTRUCTIONS: View the map to find a route to Room 3 from here. Follow that route and find a computer panel in the room for further instructions.");
            yield return new WaitForSeconds(3.0f);
            cpu.WriteLine("NOTE: For this Minimum Viable Product of the game, the point of the game is to complete all of the computer's instructions before another asteroid strikes the station. Good luck!");
            //yield return new WaitForSeconds(3.0f);
            //cpu.WriteLine(">>>An alternative to meet the orders standards is by clicking on the map button, then clicking on Room 3 to automatically arrive there.");
            //yield return new WaitForSeconds(4.0f);
            //cpu.WriteLine(">>>BEWARE: The time it takes to arrive via map will be the same regardless on how far the destination is.");
        }
    }
    IEnumerator DayOneTextTwo()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine("");
            cpu.WriteLine("--------------------------------------");
            cpu.WriteLine("Welcome to Room 3 (Beta Storage).");
            yield return new WaitForSeconds(2.0f);
            cpu.WriteLine(">>>You are required to check the supplies for repair kits (E.R.K) and batteries. Move one of each to your inventory");
            yield return new WaitForSeconds(2.5f);
            cpu.WriteLine(">>>Note: You can only carry a limited amount of supplies at a time.");
            yield return new WaitForSeconds(1.5f);
            cpu.WriteLine(">>>When you are done, come back to this computer panel.");
        }
    }
    IEnumerator DayOneTextThree()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine("");
            cpu.WriteLine("");
            cpu.WriteLine("");
            cpu.WriteLine("------------------------------------------");
            cpu.WriteLine("~~~Mission Completed Successfully~~~");
            yield return new WaitForSeconds(0.5f);
            cpu.WriteLine(">>>Return to Room One for further instructions");
        }
    }
    IEnumerator DayOneTextFour()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine("");
            cpu.WriteLine("");
            cpu.WriteLine("");
            cpu.WriteLine("");
            cpu.WriteLine("");
            cpu.WriteLine("-------------------------------");
            cpu.WriteLine(">>>Good Job. You will now be given a command to unlock most of the doors in Half A.");
            yield return new WaitForSeconds(1.0f);
            cpu.WriteLine(">>>The command is: 'opn7169'");
            yield return new WaitForSeconds(0.5f);
            cpu.WriteLine(">Type the command now.");
        }
    }
    IEnumerator DayOneTextFive()
    {
        if (!emergencyMode)
        {
            yield return new WaitForSeconds(0.75f);
            cpu.WriteLine("Good Job.");
            yield return new WaitForSeconds(0.5f);
            cpu.WriteLine(">>>Orders have been given for you to travel to Room Seven.");
            yield return new WaitForSeconds(0.8f);
            cpu.WriteLine(">>>Report to the computer panel there immediately.");
        }
    }
    IEnumerator DayOneTextSix()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine("");
            cpu.WriteLine("---------------------------------");
            cpu.WriteLine("Welcome to Room 7 (Pump System).");
            yield return new WaitForSeconds(0.75f);
            cpu.WriteLine(">>>Your are required to use your repair kits and fix the broken pump control console.");
            yield return new WaitForSeconds(0.5f);
            cpu.WriteLine(">>> Please do so as fast and efficiently as possible, as your chance of survival will increase by 4%.");
            yield return new WaitForSeconds(0.8f);
            cpu.WriteLine(">>>Return here when you are finished.");
        }
    }
    IEnumerator MinigameWarning()
    {

        CrossFadeInAnnouncement("Congrats, due to time \nconstraints, you don't have to \ndo this minigame!\nReturn to the computer panel.");
        yield return new WaitForSeconds(2.0f);
        CrossFadeOutAnnouncement("Congrats, due to time \nconstraints, you don't have to \ndo this minigame!\nReturn to the computer panel.");
    }
    IEnumerator DayOneTextSeven()
    {
        if (!emergencyMode)
        {
            yield return new WaitForSeconds(0.0f);
            cpu.WriteLine("Please Return to Room One for Further Instructions");
        }
    }
    IEnumerator DayOneTextEight()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine(">>> Exit the computer and look to your right to find battery ports.");
            cpu.WriteLine(">>> Insert all of the batteries you've carried into these ports");
            yield return new WaitForSeconds(1.0f);
            cpu.WriteLine(">>> Return here when you are finished.");
        }
    }
    IEnumerator DayOneTextNine()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine(">>> Room 1 has the only computer in the station that can perform advanced tasks such as rerouting power");
            yield return new WaitForSeconds(1.0f);
            cpu.WriteLine(">>> Click on the 'Power' tab for further instructions");
        }
    }
    IEnumerator DayOneTextTen()
    {
        if (!emergencyMode)
        {
            cpu.WriteLine("Clicking on rooms in the power tab will enable/disable the power.");
            yield return new WaitForSeconds(1.0f);
            cpu.WriteLine("Please consider what systems you need online when using this tab.");
            yield return new WaitForSeconds(0.75f);
            cpu.WriteLine("Click on Room 2 to cut the power, then click on Room 7 to connect the power to the pump.");
        }
    }
    IEnumerator asteroidComing()
    {
        cpu.WriteLine("An asteroid is on an impact course with the station.");
        yield return new WaitForSeconds(0.8f);
        cpu.WriteLine("Brace for impact and hope for the best!");
        yield return new WaitForSeconds(1.0f);
        cpu.WriteLine("[20] Seconds until impact.");
        timeOfImpact = timeManager.time + 20;
        yield return new WaitForSeconds(5.0f);
        cpu.WriteLine("[15] Seconds until impact.");
        yield return new WaitForSeconds(5.0f);
        cpu.WriteLine("[10] Seconds until impact.");
        yield return new WaitForSeconds(5.0f);
        cpu.WriteLine("[5] Seconds until impact.");
        yield return new WaitForSeconds(1.0f);
        cpu.WriteLine("[4] Seconds until impact.");
        yield return new WaitForSeconds(1.0f);
        cpu.WriteLine("[3] Seconds until impact.");
        yield return new WaitForSeconds(1.0f);
        cpu.WriteLine("[2] Seconds until impact.");
        yield return new WaitForSeconds(1.0f);
        cpu.WriteLine("[1] Seconds until impact.");
        yield return new WaitForSeconds(1.0f);
        cpu.WriteLine("BRACE FOR IMPACT");
    }
    IEnumerator LoseRoutine()
    {
        musicController.fadeOut(MusicController.ALARM_SOUND, 7.0f);
        yield return new WaitForSeconds(7.0f);
        CrossFadeInAnnouncement("You Lose!!!");
        yield return new WaitForSeconds(2.0f);
        CrossFadeOutAnnouncement("You Lose!!!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGame();
    }
    IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        CrossFadeInAnnouncement("You Win!!!");
        yield return new WaitForSeconds(2.0f);
        CrossFadeOutAnnouncement("You Win!!!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGame();
    }

    public void CrossFadeInAnnouncement(string text)
    {
        dayIndicatorBackground.CrossFadeAlpha(1f, 2f, true);
        dayIndicatorText.text = text;
        dayIndicatorText.CrossFadeAlpha(1f, 3f, true);
    }
    public void CrossFadeOutAnnouncement(string text)
    {
        dayIndicatorBackground.CrossFadeAlpha(0.0f, 2f, true);
        dayIndicatorText.text = text;
        dayIndicatorText.CrossFadeAlpha(0f, 1f, true);
    }
    public void Pause()
    {
        paused = !paused;
        if(paused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            cameraManager.unlockCursor();
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
        } else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            cameraManager.lockCursor();
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    public void Lose()
    {
        StartCoroutine(LoseRoutine());
    }
    public void SaveAndExit()
    {
#if UNITY_EDITOR
        if (saveForReals)
        {
            SAVE_VALUES();
            Time.timeScale = 1f;
            SceneManager.LoadScene("mainmenu");
        }
        else
        {
            print("Game Wasn't actually saved because saveForReals == " + saveForReals);
        }
#else
    SAVE_VALUES();
    Time.timeScale = 1f;
    SceneManager.LoadScene("mainmenu");
#endif
    }
    public void Reset()
    {
        PlayerPrefs2.SetBool("Reset", true);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("mainscene");
    }
    public void Save()
    {
#if UNITY_EDITOR
        if (saveForReals)
        {
            SAVE_VALUES();
        }
        else
        {
            print("Game Wasn't actually saved because saveForReals == " + saveForReals);
        }
#else
    SAVE_VALUES();
#endif
    }
    public void LoadSavedGame()
    {
        if (loadFromSave && !PlayerPrefs2.GetBool("Reset"))
        {
            LOAD_VALUES();
        } else
        {
            print("Game Wasn't actually loaded because loadFromSave == " + loadFromSave);
            PlayerPrefs2.SetBool("Reset", false);
        }
    }
    private void SAVE_VALUES()
    {
        PlayerPrefs.SetInt("SSTime", timeManager.time);
        print("Time saved");
        PlayerPrefs.SetString("SSCPUText", cpu.computerText);
        print("CPU state saved");
        print("Saving Checkpoints");
        PlayerPrefs2.SetBool("SSfirstCheckpoint", firstCheckpoint);
        PlayerPrefs2.SetBool("SSfirstDayNotShown", firstDayNotShown);
        PlayerPrefs2.SetBool("SSfirstDayIndicatorHidden", firstDayIndicatorHidden);

        PlayerPrefs2.SetVector2("SSmousePosition", mousePosition.valuesINeedSaved());
        PlayerPrefs2.SetVector3("SSPlayerPosition", playerPositionStuff.valuesINeedSaved());
        PlayerPrefs2.SetBool("SSCPUcameraEnabled", cameraManager.valuesINeedSaved());
        PlayerPrefs.SetInt("SSPlayerInRoom", playerInRoom);
        PlayerPrefs2.SetBool("SSsecondCheckpoint", secondCheckpoint);
        PlayerPrefs2.SetInventoryObject("SSPlayerInventory", inventoryManager.Player);
        PlayerPrefs2.SetInventoryObject("SSBetaStorage1Inventory", inventoryManager.BetaStorage1);
        PlayerPrefs2.SetInventoryObject("SSBetaStorage2Inventory", inventoryManager.BetaStorage2);
        PlayerPrefs2.SetInventoryObject("SSBetaStorage3Inventory", inventoryManager.BetaStorage3);
        PlayerPrefs2.SetInventoryObject("SSBetaStorage4Inventory", inventoryManager.BetaStorage4);
        PlayerPrefs2.SetInventoryObject("SSAlphaStorage1Inventory", inventoryManager.AlphaStorage1);
        for (int i=0; i<systemManager.valuesINeedSaved().Length; i++)
        {
            PlayerPrefs2.SetFloatArray("SSSystemManagerData["+i+"]", systemManager.valuesINeedSaved()[i]);
        }
        for (int i = 0; i < inventoryChanger.stuffINeedSaved().Length; i++)
        {
            Debug.Log("Saving InventoryChanger stuff[" + i + "]");
            Debug.Log("That stuff is: " + inventoryChanger.stuffINeedSaved()[i].ToString());
            PlayerPrefs2.SetStringArray("SSInventoryChangerValues[" + i + "]", inventoryChanger.stuffINeedSaved()[i]);
        }
        PlayerPrefs2.SetBool("SSStationInitialized", stationInitialized);
        PlayerPrefs2.SetBool("SSthirdCheckpoint", thirdCheckpoint);
        PlayerPrefs2.SetBool("SSfourthCheckpoint", fourthCheckpoint);
        PlayerPrefs2.SetBool("SScommandEntered", commandEntered);
        PlayerPrefs2.SetBool("SSfifthCheckpoint", fifthCheckpoint);
        PlayerPrefs2.SetBool("SSsixthCheckpoint", sixthCheckpoint);
        PlayerPrefs2.SetBool("SSseventhCheckpoint", seventhCheckpoint);
        PlayerPrefs2.SetBool("SSpumpGameComplete", pumpGameComplete);
        PlayerPrefs2.SetBool("SSeighthCheckpoint", eighthCheckpoint);
        PlayerPrefs2.SetBool("SSninthCheckpoint", ninthCheckpoint);
        PlayerPrefs2.SetBool("SStenthCheckpoint", tenthCheckpoint);
        PlayerPrefs2.SetBoolArray("SSRoomsPowered", powerManager.valuesINeedSaved());
        PlayerPrefs2.SetBool("SSEmergencyMode", emergencyMode);
        PlayerPrefs.SetInt("SStimeOfImpact", timeOfImpact);
        PlayerPrefs2.SetBool("SSEmergencyMessageShown", emergencyMessageShown);
        PlayerPrefs2.SetBool("SSStartedLosing", startedLosing);
        PlayerPrefs2.SetBool("SSHelpTextShown", helpTextShown);
    }
    private void LOAD_VALUES()
    {
        if (!debugMode)
        {
            timeManager.setTime(PlayerPrefs.GetInt("SSTime"));
            print("time loaded");
            cpu.computerText = PlayerPrefs.GetString("SSCPUText");
            print("CPU state loaded");
            print("Loading checkpoints");
            firstCheckpoint = PlayerPrefs2.GetBool("SSfirstCheckpoint");
            firstDayNotShown = PlayerPrefs2.GetBool("SSfirstDayNotShown");
            firstDayIndicatorHidden = PlayerPrefs2.GetBool("SSfirstDayIndicatorHidden");
            print("Done loading checkpoints");
            mousePosition.setMouseRotation(PlayerPrefs2.GetVector2("SSmousePosition"));
            playerPositionStuff.setPlayerPosition(PlayerPrefs2.GetVector3("SSPlayerPosition"));
            cameraManager.setCPUEnabled(PlayerPrefs2.GetBool("SSCPUcameraEnabled"));
            playerInRoom = PlayerPrefs.GetInt("SSPlayerInRoom");
            secondCheckpoint = PlayerPrefs2.GetBool("SSsecondCheckpoint");
            float[][] floatThing = new float[4][];
            for (int i = 0; i < 4; i++)
            {
                floatThing[i] = PlayerPrefs2.GetFloatArray("SSSystemManagerData[" + i + "]");
            }
            string[][] inventoryChangerLoadString = new string[3][];
            for(int i = 0; i < 3; i++)
            {
                Debug.Log("Loading InventoryChangerValues[" + i + "]");
                inventoryChangerLoadString[i] = PlayerPrefs2.GetStringArray("SSInventoryChangerValues[" + i + "]");
                Debug.Log("Loaded InventoryChangerValues[" + i + "] it is " + inventoryChangerLoadString[i].Length + " objects long.");
            }
            inventoryChanger.Load(inventoryChangerLoadString);
            systemManager.setValuesOfAll(floatThing);
            inventoryManager.Player = PlayerPrefs2.GetInventoryObject("SSPlayerInventory");
            inventoryManager.BetaStorage1 = PlayerPrefs2.GetInventoryObject("SSBetaStorage1Inventory");
            inventoryManager.BetaStorage2 = PlayerPrefs2.GetInventoryObject("SSBetaStorage2Inventory");
            inventoryManager.BetaStorage3 = PlayerPrefs2.GetInventoryObject("SSBetaStorage3Inventory");
            inventoryManager.BetaStorage4 = PlayerPrefs2.GetInventoryObject("SSBetaStorage4Inventory");
            inventoryManager.AlphaStorage1 = PlayerPrefs2.GetInventoryObject("SSAlphaStorage1Inventory");
            stationInitialized = PlayerPrefs2.GetBool("SSStationInitialized");
            thirdCheckpoint = PlayerPrefs2.GetBool("SSthirdCheckpoint");
            fourthCheckpoint = PlayerPrefs2.GetBool("SSfourthCheckpoint");
            commandEntered = PlayerPrefs2.GetBool("SScommandEntered");
            fifthCheckpoint = PlayerPrefs2.GetBool("SSfifthCheckpoint");
            sixthCheckpoint = PlayerPrefs2.GetBool("SSsixthCheckpoint");
            seventhCheckpoint = PlayerPrefs2.GetBool("SSseventhCheckpoint");
            pumpGameComplete = PlayerPrefs2.GetBool("SSpumpGameComplete");
            eighthCheckpoint = PlayerPrefs2.GetBool("SSeighthCheckpoint");
            ninthCheckpoint = PlayerPrefs2.GetBool("SSninthCheckpoint");
            tenthCheckpoint = PlayerPrefs2.GetBool("SStenthCheckpoint");
            powerManager.loadValues(PlayerPrefs2.GetBoolArray("SSRoomsPowered"));
            emergencyMode = PlayerPrefs2.GetBool("SSEmergencyMode");
            timeOfImpact = PlayerPrefs.GetInt("SStimeOfImpact", int.MaxValue);
            emergencyMessageShown = PlayerPrefs2.GetBool("SSEmergencyMessageShown");
            startedLosing = PlayerPrefs2.GetBool("SSStartedLosing");
            helpTextShown = PlayerPrefs2.GetBool("SSHelpTextShown");
        }
    }

    void PlayerEnterBathroom() { playerInRoom = 0; }
    void PlayerEnterAIROOM() { playerInRoom = 1; }
    void PlayerEnterBETAHUB() { playerInRoom = 2; }
    void PlayerEnterBETASTORAGE() { playerInRoom = 3; }
    void PlayerEnterGREENROOM() { playerInRoom = 4; }
    void PlayerEnterBETAQUARTERS() { playerInRoom = 5; }
    void PlayerEnterRADARANDCOMM() { playerInRoom = 6; }
    void PlayerEnterPUMPSYSTEM() { playerInRoom = 7; }
    void PlayerEnterWATERFILT() { playerInRoom = 8; }
    void PlayerEnterDIGLIBRARY() { playerInRoom = 9; }
    void PlayerEnterSHIELDINGSYSTEM() { playerInRoom = 10; }
    void PlayerEnterALPHASTORAGE() { playerInRoom = 11; }
    void PlayerEnterMYSTERYROOM() { playerInRoom = 17; }
    public bool playerIn(int room)
    {
        if(playerInRoom == room)
        {
            return true;
        }
        return false;
    }

    //MINIGAME LOADERS----------------------------------------------------------------------------------------------------------------------------------------------
    public void loadMinigame(string game)
    {
        PlayerPrefs.SetString("CheckpointToSet", null);
        
        SAVE_VALUES();
        if (game == "pump")
        {
            //Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = true;
            //SceneManager.LoadSceneAsync("pumpminigame");
            StartCoroutine(MinigameWarning());
            pumpGameComplete = true;
        } else
        {
            return;
        }
    }
    public void EndGame()
    {
        SceneManager.LoadScene("credits");
    }
}



/// <summary>
/// An expanded version of PlayerPrefs for Project AL1681.
/// </summary>
public class PlayerPrefs2
{
    
    //BOOLEAN-------------------------------------------------------------------------------
    /// <summary>
    /// Sets the value of the preferences indicated by key.
    /// </summary>
    /// <param name="name">Key</param>
    /// <param name="booleanValue">Boolean Value</param>
    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }

    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    public static bool GetBool(string name, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return GetBool(name);
        }

        return defaultValue;
    }
    //VECTOR3-------------------------------------------------------------------------------
    public static void SetVector3(string name, Vector3 vector3)
    {
        PlayerPrefs.SetFloat(name + ".x", vector3.x);
        PlayerPrefs.SetFloat(name + ".y", vector3.y);
        PlayerPrefs.SetFloat(name + ".z", vector3.z);

        Debug.Log("PlayerPrefs2: Set Vector3 " + name + " to " + vector3.x + "," + vector3.y + "," + vector3.z);
    }

    public static Vector3 GetVector3(string name)
    {
        return new Vector3(PlayerPrefs.GetFloat(name + ".x"), PlayerPrefs.GetFloat(name + ".y"), PlayerPrefs.GetFloat(name + ".z"));
    }

    public static Vector3 GetVector3(string name, Vector3 defaultValue)
    {
        if (PlayerPrefs.HasKey(name + ".x") && PlayerPrefs.HasKey(name + ".y") && PlayerPrefs.HasKey(name + ".z"))
        {
            return GetVector3(name);
        }
        return defaultValue;
    }
    //VECTOR2-------------------------------------------------------------------------------
    public static void SetVector2(string name, Vector2 vector2)
    {
        PlayerPrefs.SetFloat(name + ".x", vector2.x);
        PlayerPrefs.SetFloat(name + ".y", vector2.y);
    }

    public static Vector2 GetVector2(string name)
    {
        return new Vector2(PlayerPrefs.GetFloat(name + ".x"), PlayerPrefs.GetFloat(name + ".y"));
    }

    public static Vector2 GetVector2(string name, Vector2 defaultValue)
    {
        if (PlayerPrefs.HasKey(name + ".x") && PlayerPrefs.HasKey(name + ".y"))
        {
            return GetVector2(name);
        }
        return defaultValue;
    }
    //FLOAT ARRAY-------------------------------------------------------------------------------
    public static void SetFloatArray(string name, float[] floatArray)
    {
        PlayerPrefs.SetInt(name + "Length", floatArray.Length);
        for (int i = 0; i < floatArray.Length; i++)
        {
            PlayerPrefs.SetFloat(name + "[" + i + "]", floatArray[i]);
        }
    }
    public static float[] GetFloatArray(string name)
    {
        int length = PlayerPrefs.GetInt(name + "Length");
        float[] returnFloat = new float[length];
        for (int i = 0; i < length; i++)
        {
            returnFloat[i] = PlayerPrefs.GetFloat(name + "[" + i + "]");
        }
        return returnFloat;
    }
    public static float[] GetFloatArray(string name, float[] defaultValue)
    {
        if (PlayerPrefs.HasKey(name + "Length"))
        {
            return GetFloatArray(name);
        }
        return defaultValue;
    }
    public static int GetFloatArrayLength(string name)
    {
        return PlayerPrefs.GetInt(name + "Length");
    }
    //INTEGER ARRAY-------------------------------------------------------------------------------
    public static void SetIntArray(string name, int[] intArray)
    {
        PlayerPrefs.SetInt(name + "Length", intArray.Length);
        for (int i = 0; i < intArray.Length; i++)
        {
            PlayerPrefs.SetInt(name + "[" + i + "]", intArray[i]);
        }
    }
    public static int[] GetIntArray(string name)
    {
        int length = PlayerPrefs.GetInt(name + "Length");
        int[] returnInt = new int[length];
        for (int i = 0; i < length; i++)
        {
            returnInt[i] = PlayerPrefs.GetInt(name + "[" + i + "]");
        }
        return returnInt;
    }
    public static int[] GetIntArray(string name, int[] defaultValue)
    {
        if (PlayerPrefs.HasKey(name + "Length"))
        {
            return GetIntArray(name);
        }
        return defaultValue;
    }
    public static int GetIntArrayLength(string name)
    {
        return PlayerPrefs.GetInt(name + "Length");
    }
    //BOOLEAN ARRAY-------------------------------------------------------------------------------
    public static void SetBoolArray(string name, bool[] boolArray)
    {
        PlayerPrefs.SetInt(name + "Length", boolArray.Length);
        for (int i = 0; i < boolArray.Length; i++)
        {
            PlayerPrefs2.SetBool(name + "[" + i + "]", boolArray[i]);
        }
    }
    public static bool[] GetBoolArray(string name)
    {
        int length = PlayerPrefs.GetInt(name + "Length");
        bool[] returnBool = new bool[length];
        for (int i = 0; i < length; i++)
        {
            returnBool[i] = PlayerPrefs2.GetBool(name + "[" + i + "]");
        }
        return returnBool;
    }
    public static bool[] GetBoolArray(string name, bool[] defaultValue)
    {
        if (PlayerPrefs.HasKey(name + "Length"))
        {
            return GetBoolArray(name);
        }
        return defaultValue;
    }
    public static int GetBoolArrayLength(string name)
    {
        return PlayerPrefs.GetInt(name + "Length");
    }
    //STRING ARRAY-------------------------------------------------------------------------------
    public static void SetStringArray(string name, string[] stringArray)
    {
        PlayerPrefs.SetInt(name + "Length", stringArray.Length);
        for (int i = 0; i < stringArray.Length; i++)
        {
            PlayerPrefs.SetString(name + "[" + i + "]", stringArray[i]);
        }
    }
    public static string[] GetStringArray(string name)
    {
        int length = PlayerPrefs.GetInt(name + "Length");
        string[] returnString = new string[length];
        for (int i = 0; i < length; i++)
        {
            returnString[i] = PlayerPrefs.GetString(name + "[" + i + "]");
            Debug.Log("PLAYERPREFS2:::" + name + "[" + i + "]" + returnString[i]);
        }
        return returnString;
    }
    public static string[] GetStringArray(string name, string[] defaultValue)
    {
        if (PlayerPrefs.HasKey(name + "Length"))
        {
            return GetStringArray(name);
        }
        return defaultValue;
    }
    public static int GetStringArrayLength(string name)
    {
        return PlayerPrefs.GetInt(name + "Length");
    }
    //INVENTORYOBJECT-----------------------------------------------------------------------------
    public static void SetInventoryObject(string name, InventoryObject inventoryObject)
    {
        PlayerPrefs.SetInt(name + ".nrg", inventoryObject.NRGAmount);
        PlayerPrefs.SetInt(name + ".battery", inventoryObject.BatteriesAmount);
        PlayerPrefs.SetInt(name + ".co2", inventoryObject.C02FilterAmount);
        PlayerPrefs.SetInt(name + ".water", inventoryObject.WaterAmount);
        PlayerPrefs.SetInt(name + ".food", inventoryObject.BoosterFoodAmount);
        PlayerPrefs.SetInt(name + ".plant", inventoryObject.PlantAmount);
    }
    public static InventoryObject GetInventoryObject(string name)
    {
        InventoryObject returnObject = new InventoryObject();
        returnObject.NRGAmount = PlayerPrefs.GetInt(name + ".nrg");
        returnObject.BatteriesAmount = PlayerPrefs.GetInt(name + ".battery");
        returnObject.C02FilterAmount = PlayerPrefs.GetInt(name + ".co2");
        returnObject.WaterAmount = PlayerPrefs.GetInt(name + ".water");
        returnObject.BoosterFoodAmount = PlayerPrefs.GetInt(name + ".food");
        returnObject.PlantAmount = PlayerPrefs.GetInt(name + ".plant");
        return returnObject;
    }
    public static InventoryObject GetInventoryObject(string name, InventoryObject defaultValue)
    {
        if(PlayerPrefs.HasKey(name+".nrg"))
        {
            return GetInventoryObject(name);
        }
        return defaultValue;
    }
}

class Vector3String
{
    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Debug.Log("SARRAY Length: " + sArray.Length);
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}
