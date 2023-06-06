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

    private void Awake()
    {
        isInteractable = false;
    }

    private void Start()
    {
        hackingText.enabled = false;
        interactText.enabled = false;

        scrollbar.SetActive(false);

        progressBar.fillAmount = 0f;
        fillAmount = 0f;
    }

    private void Update()
    {
        Debug.LogError(isInteractable);
        if(Input.GetKey(KeyCode.E))
            pulsandoE = true;
        else
            pulsandoE = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("reactor"))
        {
            ActivateHackingUI();
            if (isInteractable)
            {
                FinalHack();
            }
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
        if (pulsandoE)
        {
            scrollbar.SetActive(true);
            fillAmount += (Time.deltaTime / 10);
            progressBar.fillAmount = fillAmount;
            fpsController.IsHacking = true;
            if (fillAmount >= 1f)
            {
                isInteractable = false;
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
        interactText.enabled = false;
        hackingText.enabled = false;
        progressBar.enabled = false;
        progressBar.fillAmount = 0f;
        fillAmount = 0f;
        scrollbar.SetActive(false);
    }
}
