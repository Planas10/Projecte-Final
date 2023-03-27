using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PauseMenu pausemenu;
    [SerializeField]
    FPSController fpsController;

    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject doorLeft2;
    public GameObject doorRight2;

    //public GameObject emptyObjectDoorRight;
    //public GameObject emptyObjectDoorLeft;
    //public GameObject emptyObjectDoorRight2;
    //public GameObject emptyObjectDoorLeft2;

    private static GameManager instance;
    public static GameManager Instance() {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (fpsController.CanOpen1)
        {
            OpenDoor1(doorRight, doorLeft);
        }
        if (fpsController.CanOpen2)
        {
            OpenDoor2(doorRight2,doorLeft2);
        }
    }

    public bool IsPaused() { return pausemenu.GameIsPaused; }
    //private void ActivarTextoTuto(GameObject texto) { texto.SetActive(true); }
    //private void DesactivarTextoTuto(GameObject texto) { texto.SetActive(false); }
    //private void ActivarLuces(GameObject luz, bool LightIsActivated) { luz.SetActive(true); LightIsActivated = true; }
    //private void InicializarHackeo(bool ActiveStatus, GameObject scrollbar, Text texto, Image progressbar, float fillprogress) { }
    //private void ActivarHackeo(bool ActiveStatus, GameObject scrollbar, Text texto, Image progressbar, float fillprogress) { }
    private void OpenDoor1(GameObject Rdoor, GameObject Ldoor) { Rdoor.gameObject.SetActive(false); Ldoor.gameObject.SetActive(false); }
    private void OpenDoor2(GameObject Rdoor, GameObject Ldoor) { Rdoor.gameObject.SetActive(false); Ldoor.gameObject.SetActive(false); }


}
