using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditPlayer : MonoBehaviour {

    public Vector3 position;
    public int creditTime;
    public float moveSpeed;
    public GameObject[] creditTexts;
    public GameObject[] appearAndRotateObjects;
    public int repetitionCount;
    public GameObject camera;
    private Transform cameraT;
    public float waitTime;
    public float offset;
    public float rotationsPerMinute;
    public Color colorStart;
    public Color colorEnd;
    public float fadeDuration = 1.0f;

    public Vector3 startPosition;

    // Use this for initialization
    void Start () {
        cameraT = camera.GetComponent<Transform>();
        startPosition = cameraT.position;
        repetitionCount = 0;
        StartCoroutine(ShowRandomObjects());
        foreach (GameObject theObject in appearAndRotateObjects)
        {
            Material theMaterial = theObject.GetComponent<Renderer>().material;
            colorStart = theMaterial.color;
            colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
            theMaterial.color = colorEnd;
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        creditTime = (int)Time.timeSinceLevelLoad;
        if (creditTime > waitTime)
        {
            cameraT.position -= new Vector3(0, Time.deltaTime * moveSpeed, 0);
            position = cameraT.position;
        }
        
        if((creditTexts[creditTexts.Length - 1].GetComponent<Transform>().position.y) - offset > cameraT.position.y)
        {
            repetitionCount++;
            cameraT.position = startPosition + new Vector3(0, offset, 0);
            StartCoroutine(ShowRandomObjects());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("mainmenu");
        }
        foreach(GameObject gameObject in appearAndRotateObjects)
        {
            if(gameObject.transform)
            gameObject.transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
        }
    }
    IEnumerator ShowRandomObjects()
    {
        yield return new WaitForSeconds(4.0f);
        StartCoroutine(showObject(appearAndRotateObjects[0]));
        yield return new WaitForSeconds(4.0f);
        StartCoroutine(hideObject(appearAndRotateObjects[0]));
        yield return new WaitForSeconds(4.0f);
        StartCoroutine(showObject(appearAndRotateObjects[1]));
        yield return new WaitForSeconds(4.0f);
        StartCoroutine(hideObject(appearAndRotateObjects[1]));

    }
    public IEnumerator showObject(GameObject theObject)
    {
        print("Showing object...");
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            Material theMaterial = theObject.GetComponent<Renderer>().material;
            theMaterial.color = Color.Lerp(colorEnd, colorStart, t / fadeDuration);
            yield return new WaitForSeconds(0.0f);
        }
    }
    public IEnumerator hideObject(GameObject theObject)
    {
        print("Hiding object...");
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            Material theMaterial = theObject.GetComponent<Renderer>().material;
            theMaterial.color = Color.Lerp(colorStart, colorEnd, t / fadeDuration);
            yield return new WaitForSeconds(0.0f);
        }
    }
}
