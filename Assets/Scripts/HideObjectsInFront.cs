using UnityEngine;

public class HideObjectsInFront : MonoBehaviour
{
    public Transform player; // Reference to the player's transform.
    public float hideDistance = 5f; // Adjust this distance as needed.
    public LayerMask objectLayer; // Layer mask for tagged objects.

    void Update()
    {
        // Calculate the direction from the player to the object.
        Vector3 direction = transform.position - player.position;

        // Check if the object is within the hide distance and in front of the player.
        if (direction.magnitude < hideDistance)
        {
            // Cast a ray to check for obstacles between player and object.
            RaycastHit hit;
            if (Physics.Raycast(player.position, direction, out hit, hideDistance, objectLayer))
            {
                // The object is in front of the player and not blocked by obstacles.
                // Hide the object (e.g., set its renderer to inactive).
                Renderer objectRenderer = hit.collider.GetComponent<Renderer>();
                if (objectRenderer != null)
                {
                    objectRenderer.enabled = false;
                }
            }
        }
        else
        {
            // The object is not within the hide distance or is not in front of the player.
            // Make the object visible (e.g., set its renderer to active).
            Renderer objectRenderer = GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.enabled = true;
            }
        }
    }
}