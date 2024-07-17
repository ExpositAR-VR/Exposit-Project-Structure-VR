using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonOperation : MonoBehaviour
{
    public Button[] buttons;
    public UnityEvent[] ButtonsEvents;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;  // Capture the current index to use in the lambda expression
            buttons[i].onClick.AddListener(() => ButtonClicked(index));
        }
       
    }

    public void ButtonClicked(int buttonIndex)
    {
        InvokeThisEvent(buttonIndex);
    }
    public void InvokeThisEvent(int eventNumber)
    {
        ButtonsEvents[eventNumber].Invoke();
    }
 
}
