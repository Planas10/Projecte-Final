using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMinijuegoNumeros : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel;

    public string debugstring;

    private PasswordCanvasManager passwordCanvasManager;

    public string password;

    private bool isInside = false;
    public bool doingGame;

    public GameObject HUD;

    public GameObject LDoor;
    public GameObject LDoorLimit;

    public GameObject RDoor;
    public GameObject RDoorLimit;

    public bool gameCompleted;

    private void Awake()
    {
        gameCompleted = false;
        if (FindObjectOfType<PasswordCanvasManager>() != null)
        {
            passwordCanvasManager = FindObjectOfType<PasswordCanvasManager>();
        }

        passwordCanvasManager.correctPassword = password;

    }

    private void Update()
    {      
        if (isInside && passwordCanvasManager.colliderMinijuegoNumeros == this)
        {
            if (doingGame)
            {
                Cursor.lockState = CursorLockMode.None;
                panel.gameObject.SetActive(true);
                doingGame = true;
                HUD.SetActive(false);
            }
            else
            {
                isInside = false;
                Cursor.lockState = CursorLockMode.Locked;
                doingGame = false;
                HUD.SetActive(true);
                panel.gameObject.SetActive(false);
            }

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<FPSController>())
        {
            passwordCanvasManager.colliderMinijuegoNumeros = this;
            isInside = true;
            if (Input.GetKeyDown(KeyCode.E))
                doingGame = true;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<FPSController>())
        {
            isInside = false;
            Cursor.lockState = CursorLockMode.Locked;
            doingGame = false;
            HUD.SetActive(true);
        }

    }

    public void OpenDoors()
    {
        if (Vector3.Distance(LDoor.transform.position, LDoorLimit.transform.position) < 0.5f)
        {
            LDoor.transform.position -= LDoor.transform.right * 1f * Time.deltaTime;
        }
        if (Vector3.Distance(RDoor.transform.position, RDoorLimit.transform.position) < 0.5f)
        {
            RDoor.transform.position -= RDoor.transform.right * 1f * Time.deltaTime;
        }
    }

}
