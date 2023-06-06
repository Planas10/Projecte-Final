using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PasswordCanvasManager : MonoBehaviour
{
    public string correctPassword;
    [SerializeField] private Text passwordText;

    private string currentPasword = "";

    public GameObject panel;

    public GameObject HUD;

    public ColliderMinijuegoNumeros colliderMinijuegoNumeros;

    public AudioSource ok;

    public AudioSource mek;

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
            mek.Play();
        }
    }

    public void EnterPressed()
    {
        if(correctPassword == currentPasword)
        {
            panel.gameObject.SetActive(false);
            HUD.SetActive(true);
            colliderMinijuegoNumeros.gameCompleted = true;
            colliderMinijuegoNumeros.doingGame = false;
            currentPasword = "";

            //ok
            ok.Play();

        }
        else
        {
            //mec
            mek.Play();
            currentPasword = "";
        }

    }

    // Update is called once per frame
    void Update()
    {
        correctPassword = colliderMinijuegoNumeros.password;

        passwordText.text = currentPasword;
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    panel.gameObject.SetActive(false);
        //}
        if (colliderMinijuegoNumeros.gameCompleted)
        {
            colliderMinijuegoNumeros.OpenDoors();
        }
    }
}
