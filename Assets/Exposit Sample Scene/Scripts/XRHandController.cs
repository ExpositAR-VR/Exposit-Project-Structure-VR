using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandController : MonoBehaviour
{
    public InputActionReference leftHandGrabAction;
    public InputActionReference rightHandGrabAction;
    public XRDirectInteractor leftHandInteractor;
    public XRDirectInteractor rightHandInteractor;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private void OnEnable()
    {
        leftHandGrabAction.action.performed += OnLeftHandGrab;
        rightHandGrabAction.action.performed += OnRightHandGrab;

        leftHandGrabAction.action.Enable();
        rightHandGrabAction.action.Enable();
    }

    private void OnDisable()
    {
        leftHandGrabAction.action.performed -= OnLeftHandGrab;
        rightHandGrabAction.action.performed -= OnRightHandGrab;

        leftHandGrabAction.action.Disable();
        rightHandGrabAction.action.Disable();
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetLeftAnimation(true);
         
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetRightAnimation(true);   
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            SetLeftAnimation(false);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            SetRightAnimation(false);
        }

    }

    private void OnLeftHandGrab(InputAction.CallbackContext context)
    {
        if (leftHandInteractor.hasSelection)
        {
            leftHandInteractor.EndManualInteraction();
          
        }
        else
        {
            var interactable = GetNearestInteractable(leftHandInteractor);
            if (interactable != null)
            {
                leftHandInteractor.StartManualInteraction(interactable);
               
            }
        }
    }

    private void OnRightHandGrab(InputAction.CallbackContext context)
    {
        if (rightHandInteractor.hasSelection)
        {
            rightHandInteractor.EndManualInteraction();
           
          

        }
        else
        {
            var interactable = GetNearestInteractable(rightHandInteractor);
            if (interactable != null)
            {
                rightHandInteractor.StartManualInteraction(interactable);
            
                
            }
        }
    }

    private IXRSelectInteractable GetNearestInteractable(XRDirectInteractor interactor)
    {
        IXRSelectInteractable nearestInteractable = null;
        float minDistance = float.MaxValue;

        foreach (var interactable in interactor.interactablesSelected)
        {
            float distance = Vector3.Distance(interactor.transform.position, interactable.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestInteractable = interactable;
            }
        }

        return nearestInteractable;
    }
    public void SetRightAnimation(bool value)
    {
        rightHandAnimator.SetBool("NowGrab", value);
       
    }

    public void SetLeftAnimation(bool value)
    {
        leftHandAnimator.SetBool("NowGrab", value);
      
    }
}
