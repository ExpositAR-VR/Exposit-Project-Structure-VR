using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[HelpURL("https://docs.google.com/document/d/1rlWtFSpSTjG-AZoaL1kzDTmgNX4Bxgf-glSFpHtpcqg/edit?usp=sharing")]
public class ExpositTouchGrabInteractable : MonoBehaviour
{
    private Transform originalParent;
    [Header("Insert XR Collider")]
  //  public Collider RighthandColider;
   // public Collider LefthandColider;
    public Transform RightAttachTransform;
    public Transform LeftAttachTransform;
    public bool UseDefaultGrab = false;
    [Header("Insert Object HandPose Here")]
    public GameObject RightHandPose;
    public GameObject LeftHandPose;
    public GameObject DropPoint;
    private bool isGrabbed = false;
    public bool DropWithSnap = false;
    private Collider DropColider;
    private float TimeDuration = 1f;
    public UnityEvent OnGrab;
    public UnityEvent OnRelease;
    bool isRightHandGrab = false;
    bool isLeftHandGrab = false;
    [SerializeField] ExpositPlayerChanger playerHandChanger;

    private XRBaseController rightHandController;
    private XRBaseController leftHandController;

    [Header("Haptic Feedback Settings")]
    [Range(0,1)]
    public float hapticIntensity = 0.5f;
    [Range(0, 1)]
    public float hapticDuration = 0.2f;

    void Start()
    {
        RightHandPose.SetActive(false);
        LeftHandPose.SetActive(false);
        originalParent = DropPoint.transform.parent; // Store the original parent of the object
        DropPoint.SetActive(false);
        DropColider = DropPoint.GetComponent<Collider>();

        // Assuming the XR Controllers are tagged as "RightHand" and "LeftHand"
        rightHandController = GameObject.FindWithTag("XRRightHand").GetComponent<XRBaseController>();
        leftHandController = GameObject.FindWithTag("XRLeftHand").GetComponent<XRBaseController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the other object is tagged as "Hand"
        if (other.gameObject.CompareTag("XRRightHand"))
        {
            if (!isGrabbed)
            {
                StartCoroutine(Grab(other.transform, rightHandController));
                StartCoroutine(OnDropObjectLate());
                isRightHandGrab = true;
            }
        }
        if (other.gameObject.CompareTag("XRLeftHand"))
        {
            if (!isGrabbed)
            {
                StartCoroutine(Grab(other.transform, leftHandController));
                StartCoroutine(OnDropObjectLate());
                isLeftHandGrab = true;
            }
        }
        if (other == DropColider)
        {
            if (isGrabbed && isRightHandGrab)
            {
                StartCoroutine(Release(rightHandController));
                DropPoint.SetActive(false);
            }
            if (isGrabbed && isLeftHandGrab)
            {
                StartCoroutine(Release(leftHandController));
                DropPoint.SetActive(false);
            }
        }
    }

    IEnumerator Grab(Transform hand, XRBaseController handController)
    {
        isGrabbed = true;
        transform.SetParent(hand); // Make the object a child of the hand
        GetComponent<Rigidbody>().isKinematic = true; // Make it kinematic so it moves with the hand
        OnGrab.Invoke();
        TriggerHaptic(handController, hapticIntensity, hapticDuration); // Haptic feedback on grab
        float elapsedTime = 0f;

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
                    transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, RightAttachTransform.transform.localPosition, elapsedTime / TimeDuration);
                    transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, RightAttachTransform.transform.localRotation, elapsedTime / TimeDuration);
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
                    transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, LeftAttachTransform.transform.localPosition, elapsedTime / TimeDuration);
                    transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, LeftAttachTransform.transform.localRotation, elapsedTime / TimeDuration);
                    LeftHandPose.SetActive(true);
                    playerHandChanger.PCLeftHandsOff();
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

    IEnumerator Release(XRBaseController handController)
    {
        float elapsedTime = 0f;
        StartCoroutine(OnGrabBoolAgain());
        transform.SetParent(originalParent); // Restore the original parent
        GetComponent<Rigidbody>().isKinematic = false; // Make it non-kinematic again
        RightHandPose.SetActive(false);
        LeftHandPose.SetActive(false);
        TriggerHaptic(handController, hapticIntensity / 2, hapticDuration / 2); // Haptic feedback on release
        if (DropWithSnap)
        {
            gameObject.transform.localRotation = DropPoint.transform.localRotation;
            gameObject.transform.localPosition = DropPoint.transform.localPosition;
        }
        else
        {
            while (elapsedTime < TimeDuration)
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, DropPoint.transform.position, elapsedTime / TimeDuration);
                transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, DropPoint.transform.rotation, elapsedTime / TimeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
                isRightHandGrab = false;
                isLeftHandGrab = false;
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
        OnRelease.Invoke();
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
        yield return new WaitForSeconds(3);
        isGrabbed = false;
    }

    IEnumerator OnDropObjectLate()
    {
        yield return new WaitForSeconds(3);
        DropPoint.SetActive(true);
    }
}
