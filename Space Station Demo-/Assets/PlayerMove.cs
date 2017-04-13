using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public Rigidbody player;
    public float moveSpeed;

    public bool tooGoodForMovement;
    public float readable;

	// Use this for initialization
	void Start () {
        player = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
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
            if (Input.GetKey(KeyCode.W))
            {
                //Forward
                player.velocity = (transform.FindChild("Main Camera").transform.forward * moveSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //Back
                player.velocity = (-transform.FindChild("Main Camera").transform.forward * moveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Left
            player.velocity = -transform.FindChild("Main Camera").transform.right * moveSpeed;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            //Right
            player.velocity = transform.FindChild("Main Camera").transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //Jump
            //TODO do we want jump?
        }
        if (transform.position.y > 3)
        {
            //player.transform.position = new Vector3(player.transform.position.x, Mathf.Clamp(player.transform.position.y, 0, 3), player.transform.position.z);
        }
    }
}
