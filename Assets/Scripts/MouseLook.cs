using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -90F;
	public float maximumY = 90F;

    public bool isCursorVisible;

	//float rotationY = 0F;
    private float rotationX = 0F;
    private float rotationY = 0F;

    private bool paused;

    void Update()
    {
#if UNITY_ANDROID
        return;
#endif
        if (!paused)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                isCursorVisible = true;

            }
            else
            {
#if !UNITY_ANDROID
                Cursor.lockState = CursorLockMode.Locked;
                isCursorVisible = false;
#endif
            }
            if (axes == RotationAxes.MouseXAndY)
            {
                rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }

            if (Cursor.visible != isCursorVisible)
            {
#if !UNITY_ANDROID
                Cursor.visible = isCursorVisible;
#endif
            }
        }
	}
	
	void Start ()
	{
        paused = false;
        //TODO Cursor.SetCursor() change cursor to 
#if !UNITY_ANDROID
        Cursor.visible = isCursorVisible;
#endif
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

    void OnPauseGame()
    {
        paused = true;
        //Cursor.visible = true;
       // Cursor.lockState = CursorLockMode.None;
    }
    void OnResumeGame()
    {
        paused = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector2 valuesINeedSaved()
    {
        return new Vector2(rotationX, rotationY);
    }

    public void setMouseRotation(Vector2 rotation)
    {
        rotationX = rotation.x;
        rotationY = rotation.y;
        Cursor.lockState = CursorLockMode.Locked;
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}