using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalFinalPcHacking : MonoBehaviour
{
    public FPSController fpsController;

    [SerializeField] private Text interactText;
    [SerializeField] private Text hackingText;

    private float fillAmount;

    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject scrollbar;

    public AudioSource CoreSound;

    private bool canHack;
    private bool pulsandoE;
    private bool isInteractable;

    public GameObject particulasReactor;
    public GameObject particulasFinal;

    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
            pulsandoE = true;
        else
            pulsandoE = false;
        isInteractable = false;
        FinalHack();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("reactor"))
        {
            ActivateHackingUI();
            FinalHack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("reactor")) {
            DeactivateHackingUIinteraction();
        }
    }

    private void FinalHack()
    {
        if (isInteractable)
        {
            if (pulsandoE)
            {
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 10);
                progressBar.fillAmount = fillAmount;
                fpsController.IsHacking = true;
                if (fillAmount >= 1f)
                {
                    fpsController.IsHacking = false;
                    particulasReactor.SetActive(false);
                    particulasFinal.SetActive(true);
                    CoreSound.enabled = false;
                    DeactivateHackingUI();
                }
            }
            else
            {
                scrollbar.SetActive(false);
                fpsController.IsHacking = false;
                progressBar.fillAmount = 0f;
                fillAmount = 0f;
            }
        }
    }

    private void ActivateHackingUI()
    {
        isInteractable = true;
        progressBar.enabled = true;
        interactText.enabled = true;
    }
    private void DeactivateHackingUIinteraction()
    {
        isInteractable = false;
        progressBar.enabled = false;
        interactText.enabled = false;
    }

    public void DeactivateHackingUI()
    {
        isInteractable = false;
        interactText.enabled = false;
        hackingText.enabled = false;
        progressBar.enabled = false;
        progressBar.fillAmount = 0f;
        fillAmount = 0f;
        scrollbar.SetActive(false);
    }
}
