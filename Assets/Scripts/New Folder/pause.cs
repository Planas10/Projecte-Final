using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class pause : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausedd;
    bool enpause;
    [SerializeField] Button boton;
    public Collider botoncol;
    
    void Update()
    
    {


    
        /*if (SceneManager.LoadScene("Menu") ){

                Time.timeScale = 1f;
        }
        if (SceneManager.LoadScene("Ajustes") ){

                Time.timeScale = 1f;
        }*/
    }

    void OnCollisionEnter()
    {
        Pause();
    }
    void OnCollisionExit()
    {
        Return();
    }


    public void Return()
    {  
    Time.timeScale = 1f;
    pausedd.SetActive(false);
    enpause=false;
        
    }
    public void Pause()
    {
     Cursor.lockState = CursorLockMode.None;
     Time.timeScale = 0f;
    pausedd.SetActive(true);
    enpause=true;

    }
    public void MainMenuPause()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
    }
    

}
