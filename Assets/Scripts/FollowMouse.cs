using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    private float originalMoveSpeed;

    void Start()
    {
        // Store the original move speed
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        // Double the speed when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSpeed *= 2;
        }

        // Return to the original speed when the space bar is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            moveSpeed = originalMoveSpeed;
        }

        // Cast a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If the ray hits something (e.g., the floor), move and rotate the player towards that point
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Calculate the rotation towards the target
            Vector3 directionToFace = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
