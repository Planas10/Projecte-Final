using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMinijuegoNumeros : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel;

    public string debugstring;

    private PasswordCanvasManager passwordCanvasManager;

    private bool isInside = false;
    public bool doingGame;

    public GameObject HUD;

    private void Awake()
    {
        if (FindObjectOfType<PasswordCanvasManager>() != null)
        {
            passwordCanvasManager = FindObjectOfType<PasswordCanvasManager>();
        }

        switch (gameObject.tag)
        {
            case "pass1":
                passwordCanvasManager.correctPassword = passwordCanvasManager.password1;
                break;
            case "pass2":
                passwordCanvasManager.correctPassword = passwordCanvasManager.password2;
                break;
            case "pass3":
                passwordCanvasManager.correctPassword = passwordCanvasManager.password3;
                break;
        }
    }

    private void Update()
    {
      
        if (isInside && Input.GetKeyDown(KeyCode.E) && passwordCanvasManager.colliderMinijuegoNumeros == this)
        {
            if (!doingGame)
            {
        Debug.LogError(doingGame);
                Cursor.lockState = CursorLockMode.None;
                panel.gameObject.SetActive(true);
                doingGame = true;
                HUD.SetActive(false);
            }
            else
            {
                Debug.LogError(debugstring);
                isInside = false;
                Cursor.lockState = CursorLockMode.Locked;
                doingGame = false;
                HUD.SetActive(true);
                panel.gameObject.SetActive(false);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        passwordCanvasManager.colliderMinijuegoNumeros = this;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
            Cursor.lockState = CursorLockMode.Locked;
            doingGame = false;
            HUD.SetActive(true);
        }

    }

}
