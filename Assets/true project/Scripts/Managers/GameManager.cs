using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        BulletText.text = fpsController.RemainingAmmo + "/" + fpsController.MaxAmmo;
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

    public void ChangeScene(int sceneNumber) {
        Debug.LogError("Cambio de escena");
        SceneManager.LoadScene(sceneNumber); 
    }

    public void GODmode() {

        for (int i = 0; i < fpsController.lights.ToArray().Length; i++)
        {
            fpsController.lights[i].IsActivated = true;
        }
    }
    public void GODmodeActivateLights(LightObject light, PcLightObject PcLight) { light.ActivateLight(true); PcLight.GetComponent<Light>().color = Color.green; }

    public void OpenTrapDoors(GameObject Ldoor, GameObject LdoorLimit, GameObject Rdoor, GameObject RdoorLimit) {

        //Debug.Log("asignar destino puertas");


        if (Vector3.Distance(Ldoor.transform.position, LdoorLimit.transform.position) > 5.5f)
        {
            Ldoor.transform.position -= Ldoor.transform.forward * 0.8f * Time.deltaTime;
            //Vector3.MoveTowards(Ldoor.transform.position, LdoorLimit.transform.position, 0.2f);
            Debug.Log("puerta L se mueve");
        }
        if (Vector3.Distance(Rdoor.transform.position, RdoorLimit.transform.position) > 5.9f)
        {
            Debug.Log(Vector3.Distance(Rdoor.transform.position, RdoorLimit.transform.position));
            Rdoor.transform.position += Rdoor.transform.forward * 0.8f * Time.deltaTime;
            //Vector3.MoveTowards(Rdoor.transform.position, RdoorLimit.transform.position, 0.2f);
            Debug.Log("puerta R se mueve");
        }
    }
    public void CloseTrapDoors(GameObject Ldoor, Vector3 LdoorLimit, GameObject Rdoor, Vector3 RdoorLimit)
    {
        Debug.Log(Vector3.Distance(Ldoor.transform.position, LdoorLimit));
        if (Vector3.Distance(Ldoor.transform.position, LdoorLimit) > 0.01f)
        {
            Ldoor.transform.position += Ldoor.transform.forward * 0.8f * Time.deltaTime;
            Debug.Log("puerta L se mueve");
        }
        if (Vector3.Distance(Rdoor.transform.position, RdoorLimit) > 0.01f)
        {
            Debug.Log(Vector3.Distance(Rdoor.transform.position, RdoorLimit));
            Rdoor.transform.position -= Rdoor.transform.forward * 0.8f * Time.deltaTime;
            Debug.Log("puerta R se mueve");
        }
    }

}
