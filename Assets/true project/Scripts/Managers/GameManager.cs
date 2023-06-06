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

    public void ChangeScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber); 
    }
    public void GODmodeActivateLights(LightObject light, PcLightObject PcLight) { light.ActivateLight(true); PcLight.GetComponent<Light>().color = Color.green; }


}
