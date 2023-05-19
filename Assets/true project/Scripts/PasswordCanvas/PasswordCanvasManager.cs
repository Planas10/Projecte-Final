using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCanvasManager : MonoBehaviour
{
    public string correctPassword;
    public string password1;
    public string password2;
    public string password3;
    [SerializeField] private Text passwordText;

    private string currentPasword = "";

    public GameObject panel;

    public GameObject HUD;

    public ColliderMinijuegoNumeros colliderMinijuegoNumeros;

    private void Awake()
    {
        if (FindObjectOfType<ColliderMinijuegoNumeros>() != null)
        {
            colliderMinijuegoNumeros = FindObjectOfType<ColliderMinijuegoNumeros>();
        }
    }

    public void ButtonPressed(string s)
    {
        if(currentPasword.Length < 3)
        {
            currentPasword += s;
        }
        else
        {
            //mek
        }
    }

    public void EnterPressed()
    {
        if(correctPassword == currentPasword)
        {
            panel.gameObject.SetActive(false);
            HUD.SetActive(true);
            colliderMinijuegoNumeros.doingGame = false;
            currentPasword = "";
            //ok
        }
        else
        {
            //mec
            currentPasword = "";
        }

    }

    // Update is called once per frame
    void Update()
    {
        passwordText.text = currentPasword;
        //if(Input.GetKeyUp(KeyCode.Escape))
        //{
        //    panel.gameObject.SetActive(false);
        //}
    }
}
