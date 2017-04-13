using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    public bool open = false;

    BoxCollider collider;
    MeshRenderer meshrenderer;

	// Use this for initialization
	void Start () {
        collider = gameObject.GetComponent<BoxCollider>();
        meshrenderer = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(open)
        {
            collider.enabled = false;
            meshrenderer.enabled = false;
        } else
        {
            collider.enabled = true;
            meshrenderer.enabled = true;
        }
	}
}
