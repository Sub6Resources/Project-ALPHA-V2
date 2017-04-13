using UnityEngine;
using System.Collections;

public class PointAndClickSystem : MonoBehaviour {

    public string functionToCall;
    public float delay;

    private StationLighting stationLighting;
    private CameraSwitcher cameraController;
    private InventoryChanger inventoryChanger;
    private GameManager gameManager;
    private DoorManager doorManager;

    private bool paused;
    
	
    void Start () {
        stationLighting = FindObjectOfType<StationLighting>();
        cameraController = FindObjectOfType<CameraSwitcher>();
        inventoryChanger = FindObjectOfType<InventoryChanger>();
        doorManager = FindObjectOfType<DoorManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseOver()
    {
        //Debug.Log("Active Object Hovered Over");
        if (!paused)
        {
            if (!inventoryChanger.isOpen())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Click On Active Object");
                    Invoke(functionToCall, delay);
                }
            }
        }
    }

    public void DebugClick()
    {
        Debug.Log("Test Message");
        Destroy(gameObject);
    }

    public void FullAlert()
    {
        Debug.Log("Full Alert Initializing");
        stationLighting.redalert = true;
    }

    public void SwitchToCameraTwo()
    {
        Debug.Log("Switching to CPU Camera");
        cameraController.switchCamera(CameraSwitcher.CpuCamera);
    }

    public void SwitchToMainCamera()
    {
        Debug.Log("Switching To Main Camera onClick");
        cameraController.switchCamera(CameraSwitcher.mainCamera);
    }

    public void SwitchToHub()
    {
        Debug.Log("Switching to HUB Camera");
        cameraController.switchCamera(CameraSwitcher.HubCamera);
    }

    public void OnPauseGame()
    {
        paused = true;
    }
    public void OnResumeGame()
    {
        paused = false;
    }

    public void OpenBetaHubInventoryOne()
    {
        
        inventoryChanger.OpenInventory("BetaStorage1");
    }
    public void OpenBetaHubInventoryTwo()
    {
        
        inventoryChanger.OpenInventory("BetaStorage2");
    }
    public void loadPumpMiniGame()
    {
        Debug.Log("Loading Pump Mini Game");
        gameManager.loadMinigame("pump");
    }
    public void toggleBathroomDoor()
    {
        doorManager.Alter(DoorManager.BATHROOM_DOOR);
    }
}
