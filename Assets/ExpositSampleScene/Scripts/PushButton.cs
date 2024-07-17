using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PushButton : MonoBehaviour
{

    public Collider RightHandColider;
    public Collider LeftHandColider;
    public Transform buttonHead; // Reference to the button head
    private AudioSource ClickSound;
    public Vector3 pressedPositionOffset = new Vector3(0, -0.1f, 0); // Offset for the pressed position
    [Space]
    public UnityEvent OnButtonPressed;
    public UnityEvent OnButtonReleased;

    private Vector3 initialButtonHeadPosition; // Initial position of the button head
    private bool isPressed = false; // State of the button

    void Start()
    {
        if (buttonHead != null)
        {
            initialButtonHeadPosition = buttonHead.localPosition; // Store the initial position of the button head
        }
        ClickSound = GetComponent<AudioSource>();

        if (ClickSound == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
       

    }

    void OnTriggerEnter(Collider other)
    {
        if (other == RightHandColider) 
        {
            if (!isPressed)
            {
                PressButton();
            }
            else
            {
                ReleaseButton();
            }
        }
        if (other == LeftHandColider)
        {
            if (!isPressed)
            {
                PressButton();
            }
            else
            {
                ReleaseButton();
            }
        }
    }
   
    public void SetButtonHead(Transform newButtonHead)
    {
        buttonHead = newButtonHead;
        initialButtonHeadPosition = buttonHead.localPosition; // Update the initial position
    }

    void PressButton()
    {
        if (buttonHead != null)
        {
            if (ClickSound != null)
            {
                ClickSound.Play(); // Play the audio clip
            }
            OnButtonPressed.Invoke();
            buttonHead.localPosition = initialButtonHeadPosition + pressedPositionOffset; // Move button head to pressed position
            isPressed = true; // Update button state
        }
    }

    void ReleaseButton()
    {
        if (buttonHead != null)
        {
            OnButtonReleased.Invoke();
            buttonHead.localPosition = initialButtonHeadPosition; // Move button head back to initial position
            isPressed = false; // Update button state
        }
    }
}

