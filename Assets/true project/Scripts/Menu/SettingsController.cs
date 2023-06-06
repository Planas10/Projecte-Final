using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public void ButtonBack() { SceneManager.LoadScene("Main_Menu"); }
    public void ButtonControls() { SceneManager.LoadScene("Controls"); }
}
