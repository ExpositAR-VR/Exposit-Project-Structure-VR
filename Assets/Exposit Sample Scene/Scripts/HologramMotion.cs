using UnityEngine;

public class HologramMotion : MonoBehaviour
{
    public float spinSpeed = 10f; // Speed at which the object spins
    public float hoverHeight = 0.1f; // Maximum height the object hovers
    public float hoverSpeed = 1f; // Speed at which the object moves up and down

    private float startY; // Starting Y position of the object

    void Start()
    {
        startY = transform.position.y; // Store the starting Y position
    }

    void Update()
    {
        // Rotate the object around its up axis
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

        // Calculate the new Y position based on sine wave for hover motion
        float newY = startY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}