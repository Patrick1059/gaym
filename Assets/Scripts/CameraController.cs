using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The game object that the camera should follow
    public GameObject target;

    // The offset from the target position that the camera should maintain
    public Vector3 offset;

    // The smoothness of the camera's movement (a value between 0 and 1)
    public float smoothness = 0.5f;

    // The maximum speed that the camera can move at
    public float maxSpeed = 5f;

    void LateUpdate()
    {
        // Calculate the target position of the camera
        Vector3 targetPosition = target.transform.position + offset;

        // Calculate the direction and distance to the target position
        Vector3 direction = targetPosition - transform.position;
        float distance = direction.magnitude;

        // If the distance to the target position is greater than the maximum speed,
        // normalize the direction and multiply it by the maximum speed
        if (distance > maxSpeed)
        {
            direction.Normalize();
            direction *= maxSpeed;
        }

        // Smoothly interpolate the camera's position between its current position and the target position
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, smoothness);
    }
}
