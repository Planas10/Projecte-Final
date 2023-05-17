using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private FPSController fpsController;
    [SerializeField] private LightsController lightsController;
    [SerializeField] private Animator PlayerAnimator;

    private void Update()
    {
        //Debug.Log(fpsController.IsHacking);
        PlayerAnimator.SetBool("IsHacking", fpsController.IsHacking);
        PlayerAnimator.SetBool("IsWalking", fpsController.IsWalking);
        PlayerAnimator.SetBool("IsReloading", fpsController.IsReloading);
        PlayerAnimator.SetBool("Shooting", fpsController.Shooting);
        PlayerAnimator.SetBool("DoingLucesGame", lightsController.DoingLucesGame);
    }

}
