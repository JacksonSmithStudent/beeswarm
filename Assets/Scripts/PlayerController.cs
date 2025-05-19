using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get direction based on camera (but ignore camera Y rotation)
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * moveZ + right * moveX).normalized;
        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        // Only rotate player in third-person
        float zoomDistance = Vector3.Distance(cameraTransform.position, transform.position);
        if (moveDir != Vector3.zero && zoomDistance > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }
}
