using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHightAdjust : MonoBehaviour
{
    // Public variables to be set in the Inspector
    public Slider slider;
    public GameObject targetObject;
    public float minY;
    public float maxY;

    void Start()
    {
       
        slider.onValueChanged.AddListener(UpdateYPosition);
        UpdateYPosition(slider.value);
    }

    void UpdateYPosition(float value)
    {
        // Calculate the new Y position based on the slider value
        float newY = Mathf.Lerp(minY, maxY, value);

        // Update the target object's position
        Vector3 newPosition = targetObject.transform.position;
        newPosition.y = newY;
        targetObject.transform.position = newPosition;
    }
}
