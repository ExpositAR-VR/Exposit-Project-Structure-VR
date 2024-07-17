using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControllerButtons : MonoBehaviour
{
    public InputActionProperty _ButtonX;
    public InputActionProperty _ButtonY;
    public InputActionProperty _ButtonA;
    public InputActionProperty _ButtonB;
    public InputActionProperty _TriggerButton;
    public InputActionProperty _GripButton;
    public InputActionProperty _MenuButton;

    [Header("X Button")]
    public UnityEvent WhenButtonXPressed;
    public UnityEvent WhenButtonXPressedAgain;
    [Header("Y Button")]
    public UnityEvent WhenButtonYPressed;
    public UnityEvent WhenButtonYPressedAgain;
    [Header("A Button")]
    public UnityEvent WhenButtonAPressed;
    public UnityEvent WhenButtonAPressedAgain;
    [Header("B Button")]
    public UnityEvent WhenButtonBPressed;
    public UnityEvent WhenButtonBPressedAgain;
    [Header("Trigger Button")]
    public bool UseOnTriggerRelesedFunction = false;
    public UnityEvent WhenTriggerButtonPressed;
    public UnityEvent WhenTriggerButtonRelesed;
    public UnityEvent WhenTriggerButtonPressedAgain;
    [Header("Grip Button")]
    public bool UseOnGripRelesedFunction = false;
    public UnityEvent WhenGripButtonPressed;
    public UnityEvent WhenGripButtonPressedAgain;
    public UnityEvent WhenGripButtonRelesed;
    [Header("Menu Button")]
    public UnityEvent WhenMenuButtonPressed;
    public UnityEvent WhenMenuButtonPressedAgain;

    bool XPreesed = false;
    bool YPreesed = false;
    bool APreesed = false;
    bool BPreesed = false;
    bool TriggerPressed = false;
    bool GripPressed = false;
    bool MenuPressed = false;
    

    bool CallXPreesed = false;
    bool CallYPreesed = false;
    bool CallAPreesed = false;
    bool CallBPreesed = false;
    bool CallTriggerPressed = false;
    bool CallGripPressed = false;
    bool CallMenuPressed = false;

    void OnEnable()
    {
        _ButtonX.action.Enable();
        _ButtonY.action.Enable();
        _ButtonA.action.Enable();
        _ButtonB.action.Enable();
        _TriggerButton.action.Enable();
        _GripButton.action.Enable();
        _MenuButton.action.Enable();
    }

    private void OnDisable()
    {
        _ButtonX.action.Disable();
        _ButtonY.action.Disable();
        _ButtonA.action.Disable();
        _ButtonB.action.Disable();
        _TriggerButton.action.Disable();
        _GripButton.action.Disable();
        _MenuButton.action.Disable();
    }

    void Update()
    {
        float X = _ButtonX.action.ReadValue<float>();
        float Y = _ButtonY.action.ReadValue<float>();
        float A = _ButtonA.action.ReadValue<float>();
        float B = _ButtonB.action.ReadValue<float>();
        float Trigger = _TriggerButton.action.ReadValue<float>();
        float Grip = _GripButton.action.ReadValue<float>();
        float Menu = _MenuButton.action.ReadValue<float>();

        if (X >= 1 && !CallXPreesed)
        {
            XPreesed = !XPreesed;
            if (XPreesed)
            {
                WhenButtonXPressed.Invoke();
                CallXPreesed = true;
            }
            else
            {
                WhenButtonXPressedAgain.Invoke();
                CallXPreesed = true;
            }
        }
        else if (X < 1 && CallXPreesed)
        {
            CallXPreesed = false;
        }

        if (Y >= 1 && !CallYPreesed)
        {
            YPreesed = !YPreesed;
            if (YPreesed)
            {
                WhenButtonYPressed.Invoke();
                CallYPreesed = true;
            }
            else
            {
                WhenButtonYPressedAgain.Invoke();
                CallYPreesed = true;
            }
        }
        else if (Y < 1 && CallYPreesed)
        {
            CallYPreesed = false;
        }

        if (A >= 1 && !CallAPreesed)
        {
            APreesed = !APreesed;
            if (APreesed)
            {
                WhenButtonAPressed.Invoke();
                CallAPreesed = true;
            }
            else
            {
                WhenButtonAPressedAgain.Invoke();
                CallAPreesed = true;
            }
        }
        else if (A < 1 && CallAPreesed)
        {
            CallAPreesed = false;
        }

        if (B >= 1 && !CallBPreesed)
        {
            BPreesed = !BPreesed;
            if (BPreesed)
            {
                WhenButtonBPressed.Invoke();
                CallBPreesed = true;
            }
            else
            {
                WhenButtonBPressedAgain.Invoke();
                CallBPreesed = true;
            }
        }
        else if (B < 1 && CallBPreesed)
        {
            CallBPreesed = false;
        }

        if (Trigger >= 1 && !CallTriggerPressed)
        {
            TriggerPressed = !TriggerPressed;
            if (TriggerPressed)
            {
                WhenTriggerButtonPressed.Invoke();
                CallTriggerPressed = true;
            }
            else
            {
                if (!CallTriggerPressed && !UseOnTriggerRelesedFunction)
                {
                    WhenTriggerButtonPressedAgain.Invoke();
                    CallTriggerPressed = true;
                }
            }
        }
        else if (Trigger < 1 && CallTriggerPressed)
        {
            CallTriggerPressed = false;

            if(!CallTriggerPressed && UseOnTriggerRelesedFunction)
            {
                WhenTriggerButtonRelesed.Invoke();
            }
        }

        if (Grip >= 1 && !CallGripPressed)
        {
            GripPressed = !GripPressed;
            if (GripPressed)
            {
                WhenGripButtonPressed.Invoke();
                CallGripPressed = true;
            }
            else
            {
                if (!CallGripPressed && !UseOnGripRelesedFunction)
                {
                    WhenGripButtonPressedAgain.Invoke();
                    CallGripPressed = true;
                }

            }
        }
        else if (Grip < 1 && CallGripPressed)
        {
            CallGripPressed = false;
            if (!CallGripPressed && UseOnGripRelesedFunction)
            {
                WhenGripButtonRelesed.Invoke();
            }
        }

        if (Menu >= 1 && !CallMenuPressed)
        {
            MenuPressed = !MenuPressed;
            if (MenuPressed)
            {
                WhenMenuButtonPressed.Invoke();
                CallMenuPressed = true;
            }
            else
            {
                WhenMenuButtonPressedAgain.Invoke();
                CallMenuPressed = true;
            }
        }
        else if (Menu < 1 && CallMenuPressed)
        {
            CallMenuPressed = false;
        }
    }
}
