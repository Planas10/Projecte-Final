using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private FPSController fpsController;
    [SerializeField] private Animator PlayerAnimator;

    private void Update()
    {
        //Debug.Log(fpsController.IsHacking);
        PlayerAnimator.SetBool("IsHacking",fpsController.IsHacking);
    }

}
