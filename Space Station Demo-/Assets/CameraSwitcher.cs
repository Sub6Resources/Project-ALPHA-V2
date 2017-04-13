using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {

    public Camera[] cameras;

    public GameObject crosshatch;
    public int currentCamera = 0;

    public static int mainCamera = 0;
    public static int CpuCamera = 1;
    //TODO add more cameras you may need to access quickly here.

	// Use this for initialization
	void Start () {
        //currentCamera = PlayerPrefs.GetInt("SSCurrentCamera");
        //switchCamera(currentCamera);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void switchCamera(int targetCamera)
    {
        Debug.Log("Switching to camera " + targetCamera + " from camera " + currentCamera);
        cameras[currentCamera].enabled = false;
        Debug.Log("Disabled current camera");
        cameras[targetCamera].enabled = true;
        Debug.Log("Enabled Next Camera");
        currentCamera = targetCamera;
        PlayerPrefs.SetInt("SSCurrentCamera", currentCamera);
        Debug.Log("Updated Camera PlayerPrefs");
        if(targetCamera != 0)
        {
            cameras[CameraSwitcher.mainCamera].GetComponent<MouseLook>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Disabling Mouse Look");
            crosshatch.SetActive(false);
            Debug.Log("Disabling Crosshatch");
        } else
        {
            cameras[CameraSwitcher.mainCamera].GetComponent<MouseLook>().enabled = true;
            Debug.Log("Enabling Mouse Look");
            crosshatch.SetActive(true);
            Debug.Log("Enabling Crosshatch");
        }
    }
}
