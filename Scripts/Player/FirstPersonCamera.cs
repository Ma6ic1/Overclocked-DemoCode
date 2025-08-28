using UnityEditor;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform cameraHolder;   // Assign to CameraHolder (parent of Camera)
    public Transform playerBody;     // Assign to player object
    
    public Transform BodyMesh;
    public Transform HeadMesh;
    public LayerMask seatcolliderMask;

 

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        BodyMesh.rotation = Quaternion.Euler(0, cameraHolder.eulerAngles.y, 0);
        HeadMesh.rotation = Quaternion.Euler(cameraHolder.eulerAngles.x, 0, 0);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, cameraHolder.localEulerAngles.y + mouseX, 0f);
       
    }
}