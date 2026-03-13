using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 movementDistance; // How far to move (e.g., X=5, Y=0, Z=0)
    public float speed = 2f;
    
    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + movementDistance;
    }

    void Update()
    {
        // Moves the platform back and forth smoothly
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPosition, targetPosition, time);
    }

    // --- The "Stickiness" Logic ---
    private void OnTriggerEnter(Collider other)
    {
        // If the player stands on the platform, make them a child of it
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player jumps off, remove the parent relationship
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}