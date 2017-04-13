using UnityEngine;
using System.Collections;

public class PlayerDetector : MonoBehaviour {

    public string roomName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player entered " + roomName);
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("PlayerEnter"+roomName, SendMessageOptions.DontRequireReceiver);
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player exited " + roomName);
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("PlayerExit" + roomName, SendMessageOptions.DontRequireReceiver);
            }
        }

    }
}
