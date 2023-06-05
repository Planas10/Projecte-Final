using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PauseMenu pausemenu;
    public FPSController fpsController;
    public Reload reload;

    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject doorLeft2;
    public GameObject doorRight2;

    //public GameObject emptyObjectDoorRight;
    //public GameObject emptyObjectDoorLeft;
    //public GameObject emptyObjectDoorRight2;
    //public GameObject emptyObjectDoorLeft2;

    private static GameManager instance;
    [SerializeField] private Text BulletText;
    public static GameManager Instance() {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pausemenu.Resume();
    }

    private void Update()
    {
        BulletText.text = reload.RemainingAmmo + "/" + reload.MaxAmmo;
    }

    public bool IsPaused() { return pausemenu.GameIsPaused; }
    //private void ActivarTextoTuto(GameObject texto) { texto.SetActive(true); }
    //private void DesactivarTextoTuto(GameObject texto) { texto.SetActive(false); }
    //private void ActivarLuces(GameObject luz, bool LightIsActivated) { luz.SetActive(true); LightIsActivated = true; }
    //private void InicializarHackeo(bool ActiveStatus, GameObject scrollbar, Text texto, Image progressbar, float fillprogress) { }
    //private void ActivarHackeo(bool ActiveStatus, GameObject scrollbar, Text texto, Image progressbar, float fillprogress) { }

    public void ChangeScene(int sceneNumber) {
        Debug.LogError("Cambio de escena");
        SceneManager.LoadScene(sceneNumber); 
    }
    public void GODmodeActivateLights(LightObject light, PcLightObject PcLight) { light.ActivateLight(true); PcLight.GetComponent<Light>().color = Color.green; }


}
