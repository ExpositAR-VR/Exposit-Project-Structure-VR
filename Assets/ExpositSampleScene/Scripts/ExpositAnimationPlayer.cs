using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpositAnimationPlayer : MonoBehaviour
{
    public Animator AttachedAnimator;

    public void playAnimation(string animationName)
    {
        AttachedAnimator.Play(animationName);
    }
}
