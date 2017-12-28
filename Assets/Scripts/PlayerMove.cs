using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour {

    public Rigidbody player;
    public float moveSpeed;

    public bool tooGoodForMovement;
    public float readable;

    public Transform cameraTransform;

    public InventoryChanger inventoryChanger;

    public CameraSwitcher cameraSwitcher;

    TouchPad moveTouchPad;
    TouchPad rotateTouchPad;

	// Use this for initialization
	void Start () {
        player = GetComponent<Rigidbody>();
        cameraTransform = transform.Find("Main Camera");
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        inventoryChanger = FindObjectOfType<InventoryChanger>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = Vector3.zero;
        readable = transform.rotation.z;
        if (transform.rotation.x < -10)
        {
            //do no forward or backward movement
            Debug.Log("Rotation too great to forward move...");
            tooGoodForMovement = true;
            
        }
        else
        {
            tooGoodForMovement = false;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //Forward
                velocity = (cameraTransform.transform.forward * moveSpeed);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //Back
                velocity = (-cameraTransform.transform.forward * moveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //Left
            velocity = -cameraTransform.transform.right * moveSpeed;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //Right
            velocity = cameraTransform.transform.right * moveSpeed;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //Inventory
            if (!cameraSwitcher.getCPUEnabled())
            {
                if (inventoryChanger.isOpen())
                {
                    inventoryChanger.CloseInventory();
                }
                else
                {
                    inventoryChanger.OpenInventory("player");
                }
            }
        }
        velocity.y = 0;
        //print(velocity);
        //print("PlayerObject: "+player);
        player.AddForce(velocity, ForceMode.VelocityChange);
        player.velocity = Vector3.ClampMagnitude(player.velocity, moveSpeed - 2);
        //print("Player: "+player.velocity);
    }
    void touchPadInput(TouchPad touchPad)
    {
        
    }

    public Vector3 valuesINeedSaved()
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public void setPlayerPosition(Vector3 position)
    {
        if(position == new Vector3(0,0,0))
        {
            Debug.Log("Someone tried to teleport out of the station!!!");
            return;
        }
        transform.position = position;
    }
}
