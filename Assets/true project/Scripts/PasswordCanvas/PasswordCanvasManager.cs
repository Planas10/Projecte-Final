using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCanvasManager : MonoBehaviour
{
    public string password;
    [SerializeField] private Text passwordText;

    private string currentPasword = "";

    public GameObject panel;

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
        passwordText.text = currentPasword;
    }

    public void EnterPressed()
    {
        if(password == currentPasword)
        {
            panel.gameObject.SetActive(false);
            //ok
        }
        else
        {
            //mec
            currentPasword = "";
        }

        passwordText.text = currentPasword;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyUp(KeyCode.Escape))
        //{
        //    panel.gameObject.SetActive(false);
        //}
    }
}
