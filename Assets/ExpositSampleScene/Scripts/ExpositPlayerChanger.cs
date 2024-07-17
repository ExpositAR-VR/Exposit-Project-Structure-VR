using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpositPlayerChanger : MonoBehaviour
{
    [HideInInspector] public bool changeState = false;
    public GameObject VRRightHand;
    public GameObject VRLeftHand;
    public GameObject PCRightHand;
    public GameObject PCLeftHand;

    public void Start()
    {
        gameObject.GetComponent<ExpositPlayerController>().enabled = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            changeState = !changeState;

            if(changeState)
            {
                VRLeftHand.SetActive(false);
                VRRightHand.SetActive(false);
                PCLeftHand.SetActive(true);
                PCRightHand.SetActive(true);
                Debug.Log("You are in PC mode");
                gameObject.GetComponent<ExpositPlayerController>().enabled =true;


            }
            if (!changeState)
            {
                VRLeftHand.SetActive(true);
                VRRightHand.SetActive(true);
                PCLeftHand.SetActive(false);
                PCRightHand.SetActive(false);
                Debug.Log("You are in VR mode");
                gameObject.GetComponent<ExpositPlayerController>().enabled = false;
            }
        }
    }
    public void VRLeftHandsOff()
    {
        VRLeftHand.SetActive(false);
    }
    public void VRLeftHandsOn()
    {
        VRLeftHand.SetActive(true);
    }
    public void VRRightHandsOff()
    {
        VRRightHand.SetActive(false);
    }
    public void VRRightHandsOn()
    {
        VRRightHand.SetActive(true);
    }
    public void PCLeftHandsOff()
    {
        PCLeftHand.SetActive(false);
    }
    public void PCLeftHandsOn()
    {
        PCLeftHand.SetActive(true);
    }
    public void PCRightHandsOff()
    {
        PCRightHand.SetActive(false);
    }
    public void PCRightHandsOn()
    {
        PCRightHand.SetActive(true);
    }
    public void PCHandOff()
    {
        PCRightHand.SetActive(false);
        PCLeftHand.SetActive(false);
    }
    public void PCHandOn()
    {
        PCRightHand.SetActive(true);
        PCLeftHand.SetActive(true);
    }
    public void VRHandOff()
    {
        VRRightHand.SetActive(false);
        VRLeftHand.SetActive(false);
    }
    public void VRHandOn()
    {
        VRRightHand.SetActive(true);
        VRLeftHand.SetActive(true);
    }


}
