using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour {

    public Camera[] cameras;

    public GameObject crosshatch;
    public int currentCamera = 0;

    public static int mainCamera = 0;
    public static int CpuCamera = 1;
    public static int HubCamera = 2;

    public GameObject cpuText;
    public Text cpuTextText;
    public GameObject inputText;

    public GameObject exitButton;
    public GameObject mapButton;
    public GameObject powerButton;

    private bool isCpuEnabled;
    private bool isCpuMapEnabled;

    public Vector2 cpuTextStartingWidth;
    public Vector2 cpuTextStartingPosition;

    public RectTransform cpuTextTransform;

    HubControl hubControl;
    GameManager gameManager;

    private Transform camTransform;
    private Vector3 originalPos;
    public float shakeAmount;
    public float decreaseFactor;
    public float shakeDuration;
    //TODO add more cameras you may need to access quickly here.

    // Use this for initialization
    void Start () {
        //currentCamera = PlayerPrefs.GetInt("SSCurrentCamera");
        //switchCamera(currentCamera);
        cpuTextText = cpuText.GetComponent < Text >();
        cpuTextText.CrossFadeAlpha(0.0f, 0.0f, true);
        cpuTextStartingWidth = cpuTextTransform.sizeDelta;
        cpuTextStartingPosition = cpuTextTransform.anchoredPosition;
        hubControl = FindObjectOfType<HubControl>();
        gameManager = FindObjectOfType<GameManager>();
        camTransform = cameras[0].transform;
        originalPos = camTransform.localPosition;
#if UNITY_EDITOR
        Debug.Log("Unity Editor");
#endif

#if UNITY_IOS
      Debug.Log("Iphone");
#endif

#if UNITY_ANDROID
        Debug.Log("Android");
#endif

#if UNITY_STANDALONE_OSX
    Debug.Log("Stand Alone OSX");
#endif

#if UNITY_STANDALONE_WIN
        Debug.Log("Stand Alone Windows");
#endif
    }

    // Update is called once per frame
    void Update () {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;

            shakeAmount = shakeDuration;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cameras[CameraSwitcher.mainCamera].GetComponent<MouseLook>().enabled = false;
    }
    public void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameras[CameraSwitcher.mainCamera].GetComponent<MouseLook>().enabled = true;
    }

    public void switchCamera(int targetCamera)
    {
        
        isCpuMapEnabled = false;
        cameras[3].enabled = false;
        Debug.Log("Switching to camera " + targetCamera + " from camera " + currentCamera);
        cameras[currentCamera].enabled = false;
        Debug.Log("Disabled current camera");
        cameras[targetCamera].enabled = true;
        Debug.Log("Enabled Next Camera");
        currentCamera = targetCamera;
        PlayerPrefs.SetInt("SSCurrentCamera", currentCamera);
        Debug.Log("Updated Camera PlayerPrefs");
        if (targetCamera != 0)
        {
            cameras[CameraSwitcher.mainCamera].GetComponent<MouseLook>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Disabling Mouse Look");
            crosshatch.SetActive(false);
            Debug.Log("Disabling Crosshatch");
        }
        else
        {
            cameras[CameraSwitcher.mainCamera].GetComponent<MouseLook>().enabled = true;
            Debug.Log("Enabling Mouse Look");
            crosshatch.SetActive(true);
            Debug.Log("Enabling Crosshatch");
        }
        if(targetCamera == HubCamera)
        {
            hubControl.setToNormalMode();
        }
        if (targetCamera == CpuCamera || targetCamera == 3)
        {
            if(cpuTextStartingWidth == Vector2.zero || cpuTextStartingWidth == null)
            {
                cpuTextStartingWidth = cpuTextTransform.sizeDelta;
                cpuTextStartingPosition = cpuTextTransform.anchoredPosition;
            }
            cpuTextTransform.sizeDelta = cpuTextStartingWidth;
            cpuTextTransform.anchoredPosition = new Vector2(cpuTextStartingPosition.x, cpuTextTransform.anchoredPosition.y);
            cpuTextText.CrossFadeAlpha(1.0f, 0.0f, true);
            inputText.SetActive(true);
            isCpuEnabled = true;
            mapButton.SetActive(true);
            exitButton.SetActive(true);
            powerButton.SetActive(true);
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnCPUEnabled", SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            cpuTextText.CrossFadeAlpha(0.0f, 0.0f, true);
            
            inputText.SetActive(false);
            isCpuEnabled = false;
            mapButton.SetActive(false);
            exitButton.SetActive(false);
            powerButton.SetActive(false);
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnCPUDisabled", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    public bool valuesINeedSaved()
    {
        return isCpuEnabled;
    }
    public void setCPUEnabled(bool enabled)
    {
        if (enabled)
            switchCamera(CpuCamera);
        else
            return;
    }
    public void setToMapWithCPU()
    {
        switchCamera(HubCamera);
        hubControl.setToPowerMode();
        cpuTextStartingWidth = cpuTextTransform.sizeDelta;
        cpuTextStartingPosition = cpuTextTransform.anchoredPosition;
        print("Enabling " + cameras[3]);
        cameras[3].enabled = true;
        isCpuMapEnabled = true;
        isCpuEnabled = true;
        float xOffset = cpuTextTransform.sizeDelta.x;
        cpuTextTransform.sizeDelta = new Vector2(Screen.width / 2.1f, cpuTextTransform.sizeDelta.y);
        xOffset -= cpuTextTransform.sizeDelta.x;
        xOffset /= 2;
        print("Difference is " + xOffset);
        print("Setting x value of RectTransform from "+cpuTextTransform.anchoredPosition.x+" to "+(cpuTextTransform.anchoredPosition.x - xOffset));
        cpuTextTransform.anchoredPosition = new Vector2(cpuTextTransform.anchoredPosition.x - xOffset, cpuTextTransform.anchoredPosition.y);
        cpuTextText.CrossFadeAlpha(1.0f, 0.0f, true);
        inputText.SetActive(true);
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnCPUEnabled", SendMessageOptions.DontRequireReceiver);
        }
    }
    public bool getCPUEnabled()
    {
        return isCpuEnabled;
    }
    public bool getCpuMapEnabled()
    {
        return isCpuMapEnabled;
    }
    public IEnumerator AsteroidImpact()
    {
        
        switchCamera(mainCamera);
        yield return new WaitForSeconds(0.2f);
        shakeDuration = 2.0f;
        yield return new WaitForSeconds(2.0f);
        gameManager.Lose();
    }
    
}
