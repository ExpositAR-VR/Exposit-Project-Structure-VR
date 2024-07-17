using UnityEngine;

public class RotateWithPlayer : MonoBehaviour
{
    public Transform xrOrigin;  // Reference to the XR origin (player or camera rig)
    public float yOffset = 2.0f;  // Optional: offset in the Y direction if needed
    public float distanceFromPlayer = 2.0f;  // Distance between the player and the canvas
    public float lerpSpeed = 5.0f;  // Speed of the lerp motion

    void Update()
    {
        // Ensure we have an XR origin to reference
        if (xrOrigin == null)
        {
            Debug.LogError("XR Origin not assigned.");
            return;
        }

        // Get the position of the XR origin
        Vector3 xrOriginPosition = xrOrigin.position;

        // Calculate the direction the canvas should face
        Vector3 directionToFace = xrOrigin.forward;
        directionToFace.y = 0;  // Lock rotation to the Y axis

        // Ensure the direction is normalized
        directionToFace.Normalize();

        // Calculate the target position of the canvas at a fixed distance
        Vector3 targetPosition = xrOriginPosition + directionToFace * distanceFromPlayer;
        targetPosition.y = xrOriginPosition.y + yOffset;

        // Smoothly move the canvas to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

        // Calculate the target rotation to face the player
        Quaternion targetRotation = Quaternion.LookRotation(xrOrigin.position - transform.position);

        // Smoothly rotate the canvas to face the player
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
    }
}
