using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingController : MonoBehaviour
{
    public FPSController fpsController;

    [SerializeField] private Text interactText;
    [SerializeField] private Text hackingText;

    private float fillAmount;

    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject scrollbar;

    private bool isInteractable;

    public bool CanOpen1;
    public bool CanOpen2;

    private bool pulsandoE;

    public bool IsHacking = false;

    public List<LightObject> lights;
    public List<PcLightObject> PClights;

    private void Awake()
    {
        lights = new(FindObjectsOfType<LightObject>());
        lights.Sort((a, b) => { return a.name.CompareTo(b.name); });

        PClights = new(FindObjectsOfType<PcLightObject>());
        PClights.Sort((a, b) => { return a.name.CompareTo(b.name); });

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
        if (lights[0].IsActivated == true && lights[1].IsActivated == true){ CanOpen1 = true; }

        if (lights[2].IsActivated == true && lights[3].IsActivated == true && lights[4].IsActivated == true) { CanOpen2 = true; }

        //check inputs
        if (Input.GetKeyUp(KeyCode.E)) {  }

        if (!IsHacking) { fpsController.CanMove = true; }
        else { fpsController.CanMove = false; }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Object1") && !lights[0].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[0], PClights[0]);
        }
        if (other.gameObject.CompareTag("Object2") && !lights[1].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[1], PClights[1]);
        }
        if (other.gameObject.CompareTag("Object3") && !lights[2].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[2], PClights[2]);
        }
        if (other.gameObject.CompareTag("Object4") && !lights[3].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[3], PClights[3]);
        }
        if (other.gameObject.CompareTag("Object5") && !lights[4].IsActivated)
        {
            ActivateHackingUI();
            Hacking(lights[4], PClights[4]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object1") ||
            other.CompareTag("Object2") ||
            other.CompareTag("Object3") ||
            other.CompareTag("Object4") ||
            other.CompareTag("Object5")) { DeactivateHackingUI(); }
    }

    private void Hacking(LightObject light, PcLightObject PcLight)
    {
        if (isInteractable)
        {
            if (pulsandoE)
            {
                scrollbar.SetActive(true);
                fillAmount += (Time.deltaTime / 6);
                progressBar.fillAmount = fillAmount;
                IsHacking = true;
                if (fillAmount >= 1f)
                {
                    IsHacking = false;
                    light.ActivateLight(true);
                    PcLight.GetComponent<Light>().color = Color.green;
                    DeactivateHackingUI();
                }
            }
            else
            {
                scrollbar.SetActive(false);
                IsHacking = false;
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
