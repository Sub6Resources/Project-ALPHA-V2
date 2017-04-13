using UnityEngine;
using System.Collections;

public class PumpGame : MonoBehaviour {

    public bool mouseClicked = false;
    public bool trackOne;
    public bool trackTwo;
    public bool trackThree;
    public bool trackFour;
    public int currentTrack;
    public GameObject otherCheckpoint;
    public Camera camera;

    public BoxCollider trackOneOne;
    public BoxCollider trackOneTwo;
    public BoxCollider trackTwoOne;
    public BoxCollider trackTwoTwo;
    public BoxCollider trackThreeOne;
    public BoxCollider trackThreeTwo;
    public BoxCollider trackFourOne;
    public BoxCollider trackFiveTwo;


    void Start()
    {

    }
	// Update is called once per frame
	void Update () {
	    if(mouseClicked)
        {
            //Debug.Log("Mouse Click");
            RaycastHit hit;
            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.LogError("Hit is null: " + hit);
                return;
            }

            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            {
                Debug.LogError("Error in line 32: " + rend + "<--rend. rend.sharedMaterial:" + rend.sharedMaterial + " rend.sharedMaterial.mainTexture:" + rend.sharedMaterial.mainTexture + " mesh collider:" + meshCollider);
                return;
            }

            Texture2D tex = rend.material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;
            Color color = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
            if(color == Color.black)
            {
                Debug.Log("OKAY");
            } else
            {
                Debug.Log("NOT OKAY");
            }
            //Debug.Log("Finished");
        }
        else
        {
            Debug.Log("MOUSE UP");
        }
	}

    void OnMouseDown()
    {
        mouseClicked = true;
    }

    void OnMouseUp()
    {
        mouseClicked = false;
    }

    private void set(int trackNumber, bool isFinished)
    {
        switch(trackNumber)
        {
            case 1:
                trackOne = isFinished;
                break;
            case 2:
                trackTwo = isFinished;
                break;
            case 3:
                trackThree = isFinished;
                break;
            case 4:
                trackFour = isFinished;
                break;
        }
    }
}
