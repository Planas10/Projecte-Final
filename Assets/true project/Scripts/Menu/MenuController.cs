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
    public void ButtonStart() { SceneManager.LoadScene(1); }
    public void CreditsButton() { SceneManager.LoadScene(4); }
    public void buttonSettings() { SceneManager.LoadScene(2); }
    public void ButtonQuit() { Application.Quit(); }

}
