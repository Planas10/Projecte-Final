using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void ButtonStart() { SceneManager.LoadScene("lvl1"); }
    public void CreditsButton() { SceneManager.LoadScene("Credits"); }
    public void buttonSettings() { SceneManager.LoadScene("Settings"); }
    public void ButtonQuit() { Application.Quit(); }

}
