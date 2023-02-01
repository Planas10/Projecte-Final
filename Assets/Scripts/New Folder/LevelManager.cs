using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{      

    public AudioClip Play;
    public AudioSource fuenteAudio;
    
    
    
    public void CargarNivel(string pNombreNivel){



        SceneManager.LoadScene(pNombreNivel);
        Time.timeScale = 1f;
        fuenteAudio.clip = Play;
        fuenteAudio.Play();

    }
    public void Reset(string paresetear){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             


    }
     
    private void Start() 
    
    {
        fuenteAudio = GetComponent<AudioSource> ();
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
