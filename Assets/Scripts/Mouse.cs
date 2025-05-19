using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Transform player; // Player target to follow
    public Vector2 sensitivity = new Vector2(10000f, 10000f);
    public float scrollSpeed = 5f;
    public float minZoom = 0f;     // First-person distance
    public float maxZoom = 5f;     // Third-person distance

    private float currentZoom = 2.5f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private bool isRightClicking = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * scrollSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Right-click to rotate camera in third-person
        if (currentZoom > minZoom)
        {
            if (Input.GetMouseButtonDown(1)) Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetMouseButton(1))
            {
                isRightClicking = true;
                RotateCamera();
            }
            else
            {
                isRightClicking = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else // First-person
        {
            RotateCamera();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void LateUpdate()
    {
        if (currentZoom > minZoom)
        {
            // Third-person camera positioning
            Vector3 direction = Quaternion.Euler(xRotation, yRotation, 0f) * Vector3.back;
            transform.position = player.position + direction * currentZoom;
            transform.LookAt(player.position);
        }
        else
        {
            // First-person camera follows player's head (assumes camera is a child)
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
