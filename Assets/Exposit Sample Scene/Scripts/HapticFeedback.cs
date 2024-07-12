using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    public static HapticFeedback Instance { get; private set; }

    [Header("Default Haptic Feedback Settings")]
    public float defaultHapticIntensity = 0.5f;
    public float defaultHapticDuration = 0.2f;

    private XRBaseController rightHandController;
    private XRBaseController leftHandController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Assuming the XR Controllers are tagged as "RightHand" and "LeftHand"
        rightHandController = GameObject.FindWithTag("RightHand").GetComponent<XRBaseController>();
        leftHandController = GameObject.FindWithTag("LeftHand").GetComponent<XRBaseController>();
    }

    public void TriggerHaptic(XRBaseController controller, float intensity, float duration)
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }

    public void TriggerRightHandHaptic(float intensity, float duration)
    {
        TriggerHaptic(rightHandController, intensity, duration);
    }

    public void TriggerLeftHandHaptic(float intensity, float duration)
    {
        TriggerHaptic(leftHandController, intensity, duration);
    }

    public void TriggerDefaultRightHandHaptic()
    {
        TriggerHaptic(rightHandController, defaultHapticIntensity, defaultHapticDuration);
    }

    public void TriggerDefaultLeftHandHaptic()
    {
        TriggerHaptic(leftHandController, defaultHapticIntensity, defaultHapticDuration);
    }
}
