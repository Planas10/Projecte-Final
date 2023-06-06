using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private FPSController fpsController;
    [SerializeField] private Reload reload;
    [SerializeField] private Animator PlayerAnimator;
    private LightsController lightsController;

    private void Awake()
    {
        if (GetComponent<LightsController>() != null)
            lightsController = FindObjectOfType<LightsController>();
    }

    private void Update()
    {
        //Debug.Log(fpsController.IsHacking);
        PlayerAnimator.SetBool("IsHacking", fpsController.IsHacking);
        PlayerAnimator.SetBool("IsWalking", fpsController.IsWalking);
        PlayerAnimator.SetBool("IsReloading", reload.IsReloading);
        PlayerAnimator.SetBool("Shooting", fpsController.Shooting);
        if (GetComponent<LightsController>() != null)
            PlayerAnimator.SetBool("DoingLucesGame", lightsController.DoingLucesGame);
    }

}
