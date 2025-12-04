using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float sensitivity = 2f;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0.5f, 0.5f);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
