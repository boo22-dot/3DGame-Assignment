using UnityEngine;

public class SimpleFirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.8f;

    [Header("Setup")]
    public Camera playerCamera; // Drag your camera here in the Inspector!

    private float rotationX = 0f;
    private float rotationY = 0f;
    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        // This locks your mouse to the game window
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. MOUSE LOOK (Rotation)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Rotate Body left/right
        transform.rotation = Quaternion.Euler(0, rotationY, 0); 
        
        // Rotate Camera up/down
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }

        // 2. MOVEMENT (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // 3. JUMPING & GRAVITY
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
            else
            {
                velocity.y = -2f; 
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // 4. APPLY EVERYTHING
        characterController.Move((move * moveSpeed + velocity) * Time.deltaTime);
    }
}