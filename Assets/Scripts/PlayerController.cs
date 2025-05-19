using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform cameraTransform;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;

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

        // Rotate player toward movement direction if moving
        float zoomDistance = Vector3.Distance(cameraTransform.position, transform.position);
        if (moveDir != Vector3.zero && zoomDistance > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }

        // Ground check using raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Jump with Space key
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
