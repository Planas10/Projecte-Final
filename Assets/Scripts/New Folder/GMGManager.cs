using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GMGManager : MonoBehaviour
{
    public bool gameRunning; 

    //public static GMGManager s_current;

    //protected static MovimientoLobo s_ParcaData = null;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){

            //ChangeGameRunningState();

                SceneManager.LoadScene("MenuPausa");
        }
    }

    /*void Awake() {
        {
            s_current = this;
        }
    }

    public void SaveProtagonistData()
    {
        var MovimientoLobo = FindObjectOfType<>
    }

        public void OnProtagonistDeath(GameObject Parca)
    {
        Destroy(Parca);

        Invoke("Respawn", 1f);


    }

    protected void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
    
    /*public void ChangeGameRunningState(){

        gameRunning = !gameRunning;

        if (gameRunning){

            Time.timeScale= 1f;

            AudioSource[] audios= FindObjectOfType<AudioSource>();

            foreach (AudioSource a in audios){

                a.Play();
            }


        }
        else
        {

            Time.timeScale= 0f;
            
            AudioSource[] audios= FindObjectOfType<AudioSource>();

            foreach (AudioSource a in audios){

                a.Pause();
            }

        }
    }
    public bool IsGameRunning(){

        return gameRunning;
    }


     public void CambiarPantalla(Button button)
    {
        if (button.name.Equals("Jugar"))
        {
            SceneManager.LoadScene("PantallaJuego");
        }
        else if (button.name.Equals("HighScores"))
        {
   

    }*/
}
