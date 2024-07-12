using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpositPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float mouseSensitivity = 100f; // Sensitivity for mouse movement

    private float verticalRotation = 0f;

    public Transform cameraTransform; // Assign the camera's transform in the Inspector

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle player movement
        MovePlayer();

        // Handle player rotation
        RotatePlayer();
    }

    void MovePlayer()
    {
        // Get the input values for horizontal and vertical movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveVertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Calculate movement direction relative to the camera's orientation
        Vector3 forwardMovement = cameraTransform.forward * moveVertical;
        Vector3 rightMovement = cameraTransform.right * moveHorizontal;

        // Combine the movement directions and normalize to ensure consistent speed
        Vector3 movement = (forwardMovement + rightMovement).normalized;

        // Check for vertical movement using CTRL and ALT keys
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            movement += Vector3.down; // Move down
        }
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            movement += Vector3.up; // Move up
        }

        // Move the player
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    void RotatePlayer()
    {
        // Get the mouse movement values
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the player vertically and clamp the rotation to avoid over-rotation
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Apply the vertical rotation to the player's camera
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
