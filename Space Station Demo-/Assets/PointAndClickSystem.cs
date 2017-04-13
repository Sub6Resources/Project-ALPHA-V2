using UnityEngine;
using System.Collections;

public class PointAndClickSystem : MonoBehaviour {

    public string functionToCall;
    public float delay;

    private StationLighting stationLighting;
    private CameraSwitcher cameraController;
    
	
    void Start () {
        stationLighting = FindObjectOfType<StationLighting>();
        cameraController = FindObjectOfType<CameraSwitcher>();
    }

    void OnMouseOver()
    {
        //Debug.Log("Active Object Hovered Over");
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click On Active Object");
            Invoke(functionToCall, delay);
        }
    }

    void DebugClick()
    {
        Debug.Log("Test Message");
        Destroy(gameObject);
    }

    void FullAlert()
    {
        Debug.Log("Full Alert Initializing");
        stationLighting.redalert = true;
    }

    void SwitchToCameraTwo()
    {
        Debug.Log("Switching to CPU Camera");
        cameraController.switchCamera(CameraSwitcher.CpuCamera);
    }

    void SwitchToMainCamera()
    {
        Debug.Log("Switching To Main Camera onClick");
        cameraController.switchCamera(CameraSwitcher.mainCamera);
    }
}
