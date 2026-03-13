using UnityEngine;
using UnityEngine.SceneManagement; // This is needed to restart the level

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing that fell into the zone is the Player
        if (other.CompareTag("Player"))
        {
            // This restarts the current level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}