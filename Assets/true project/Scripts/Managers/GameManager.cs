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

    public void OpenTrapDoors(GameObject Ldoor, GameObject Rdoor) {

        Vector3 LdoorFpos = new Vector3(Ldoor.transform.position.x, Ldoor.transform.position.y, Ldoor.transform.position.z + 1.5f);
        Vector3 RdoorFpos = new Vector3(Rdoor.transform.position.x, Rdoor.transform.position.y, Rdoor.transform.position.z - 1.5f);

        if (Vector3.Distance(Ldoor.transform.position, LdoorFpos) > 0.1f)
        {
            Ldoor.transform.Translate(Ldoor.transform.forward * 0.8f * Time.deltaTime);
        }
        if (Vector3.Distance(Rdoor.transform.position, RdoorFpos) > 0.1f)
        {
            Ldoor.transform.Translate(-Rdoor.transform.forward * 0.8f * Time.deltaTime);
        }
        //Vector3 LFpose = new Vector3(Ldoor.transform.position.x, Ldoor.transform.position.y, Ldoor.transform.position.z + 1.5f);
        //Vector3 RFpose = new Vector3(Rdoor.transform.position.x, Rdoor.transform.position.y, Rdoor.transform.position.z - 1.5f);
        //vfvfdvfdgdf

        //Ldoor.transform.position = LFpose;
        //Rdoor.transform.position = RFpose;

        //Ldoor.SetActive(false); Rdoor.SetActive(false);

    }
    public void CloseTrapDoors(GameObject Ldoor, GameObject Rdoor)
    {
        Vector3 LdoorFpos = new Vector3(Ldoor.transform.position.x, Ldoor.transform.position.y, Ldoor.transform.position.z - 1.5f);
        //Vector3 RdoorFpos = new Vector3(Rdoor.transform.position.x, Rdoor.transform.position.y, Rdoor.transform.position.z + 1.5f);

        if (Vector3.Distance(Ldoor.transform.position, LdoorFpos) > 0.1f)
        {
            Ldoor.transform.Translate(-Ldoor.transform.right * 0.8f * Time.deltaTime);
        }
        //if (Vector3.Distance(Rdoor.transform.position, RdoorFpos) > 0.1f)
        //{
        //    Ldoor.transform.Translate(Rdoor.transform.right * 0.8f * Time.deltaTime);
        //}
        //Vector3 LIpose = new Vector3(Ldoor.transform.position.x, Ldoor.transform.position.y, Ldoor.transform.position.z - 1.5f);
        //Vector3 RIpose = new Vector3(Rdoor.transform.position.x, Rdoor.transform.position.y, Rdoor.transform.position.z + 1.5f);
        //Ldoor.transform.position = LIpose;
        //Rdoor.transform.position = RIpose;

        //Ldoor.SetActive(true); Rdoor.SetActive(true);
    }

}
