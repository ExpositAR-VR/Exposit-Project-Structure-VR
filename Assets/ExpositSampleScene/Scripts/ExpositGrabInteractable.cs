using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class ExpositGrabInteractable : MonoBehaviour
{
    private Transform originalParent;
    [Header("Insert Object HandPose Here")]
    public GameObject RightHandPose;
    public GameObject LeftHandPose;
    public GameObject DropPoint;
    public Transform RightAttachTransform;
    public Transform LeftAttachTransform;
    public bool UseDefaultGrab = false;
    private bool isGrabbed = false;
    public bool DropWithSnap = false;
    private Collider DropColider;
    private float TimeDuration = 1f;
    public UnityEvent OnGrab;
    public UnityEvent OnRelease;
    bool isRightHandGrab = false;
    bool isLeftHandGrab = false;
    bool isGhostOn = false;
    [SerializeField] ExpositPlayerChanger playerHandChanger;

    private XRGrabInteractable grabInteractable;

    [Header("Haptic Feedback Settings")]
    [Range(0,1)]
    public float hapticIntensity = 0.5f;
    [Range(0, 1)]
    public float hapticDuration = 0.2f;

    private XRBaseController rightHandController;
    private XRBaseController leftHandController;

    void Start()
    {
        RightHandPose.SetActive(false);
        LeftHandPose.SetActive(false);
        originalParent = DropPoint.transform.parent; // Store the original parent of the object
        DropPoint.SetActive(false);
        DropColider = DropPoint.GetComponent<Collider>();

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabObject);
        grabInteractable.selectExited.AddListener(OnReleaseObject);

        // Assuming the XR Controllers are attached to the hand colliders
        rightHandController = GameObject.FindWithTag("XRRightHand").GetComponent<XRBaseController>();
        leftHandController = GameObject.FindWithTag("XRLeftHand").GetComponent<XRBaseController>();
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabObject);
        grabInteractable.selectExited.RemoveListener(OnReleaseObject);
    }

    private void OnGrabObject(SelectEnterEventArgs args)
    {
        if (isGrabbed) return;

        isGrabbed = true;
        transform.SetParent(args.interactorObject.transform); // Make the object a child of the hand
        GetComponent<Rigidbody>().isKinematic = true; // Make it kinematic so it moves with the hand
        OnGrab.Invoke();
        StartCoroutine(Grab(args.interactorObject.transform));
        StartCoroutine(OnDropObjectLate());

        if (args.interactorObject.transform.name.Contains("Right"))
        {
            isRightHandGrab = true;
            TriggerHaptic(rightHandController, hapticIntensity, hapticDuration); // Haptic feedback on grab
        }
        else if (args.interactorObject.transform.name.Contains("Left"))
        {
            isLeftHandGrab = true;
            TriggerHaptic(leftHandController, hapticIntensity, hapticDuration); // Haptic feedback on grab
        }
    }

    private void OnReleaseObject(SelectExitEventArgs args)
    {
        if (!isGrabbed) return;

        StartCoroutine(Release());
        DropPoint.SetActive(false);
        isRightHandGrab = false;
        isLeftHandGrab = false;
        OnRelease.Invoke();

        if (args.interactorObject.transform.name.Contains("Right"))
        {
            TriggerHaptic(rightHandController, hapticIntensity / 2, hapticDuration / 2); // Haptic feedback on release
        }
        else if (args.interactorObject.transform.name.Contains("Left"))
        {
            TriggerHaptic(leftHandController, hapticIntensity / 2, hapticDuration / 2); // Haptic feedback on release
        }
    }

    IEnumerator Grab(Transform hand)
    {
        float elapsedTime = 0f;
        isGhostOn = false;

        if (UseDefaultGrab)
        {
            transform.localPosition = Vector3.zero; // Optionally, reset the position relative to the hand
            transform.localRotation = Quaternion.identity; // Optionally, reset the rotation relative to the hand
            if (playerHandChanger.changeState == true)
            {
                playerHandChanger.PCHandOn();
            }
            else
            {
                playerHandChanger.VRHandOn();
            }
        }
        else
        {
            while (elapsedTime < TimeDuration)
            {
                if (isRightHandGrab)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, RightAttachTransform.localPosition, elapsedTime / TimeDuration);
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, RightAttachTransform.localRotation, elapsedTime / TimeDuration);
                    RightHandPose.SetActive(true);
                    if (playerHandChanger.changeState == true)
                    {
                        playerHandChanger.PCLeftHandsOn();
                        playerHandChanger.PCRightHandsOff();
                    }
                    else
                    {
                        playerHandChanger.VRLeftHandsOn();
                        playerHandChanger.VRRightHandsOff();
                    }
                }
                if (isLeftHandGrab)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, LeftAttachTransform.localPosition, elapsedTime / TimeDuration);
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, LeftAttachTransform.localRotation, elapsedTime / TimeDuration);
                    LeftHandPose.SetActive(true);
                    if (playerHandChanger.changeState == true)
                    {
                        playerHandChanger.PCLeftHandsOff();
                        playerHandChanger.PCRightHandsOn();
                    }
                    else
                    {
                        playerHandChanger.VRLeftHandsOff();
                        playerHandChanger.VRRightHandsOn();
                    }
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator Release()
    {
        float elapsedTime = 0f;
        transform.SetParent(originalParent); // Restore the original parent
        GetComponent<Rigidbody>().isKinematic = false; // Make it non-kinematic again
        RightHandPose.SetActive(false);
        LeftHandPose.SetActive(false);
       

        if (DropWithSnap)
        {
            transform.localRotation = DropPoint.transform.localRotation;
            transform.localPosition = DropPoint.transform.localPosition;
        }
        else
        {
            while (elapsedTime < TimeDuration)
            {
                transform.position = Vector3.Lerp(transform.position, DropPoint.transform.position, elapsedTime / TimeDuration);
                transform.rotation = Quaternion.Lerp(transform.rotation, DropPoint.transform.rotation, elapsedTime / TimeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
                if (playerHandChanger.changeState == true)
                {
                    playerHandChanger.PCHandOn();
                }
                else
                {
                    playerHandChanger.VRHandOn();
                }
            }
        }

        StartCoroutine(OnGrabBoolAgain());
    }

    void TriggerHaptic(XRBaseController controller, float amplitude, float duration)
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }
    }

    IEnumerator OnGrabBoolAgain()
    {
        yield return new WaitForSeconds(1.5f);
        isGrabbed = false;
    }

    IEnumerator OnDropObjectLate()
    {
        yield return new WaitForSeconds(2f);
        DropPoint.SetActive(true);
    }
}
