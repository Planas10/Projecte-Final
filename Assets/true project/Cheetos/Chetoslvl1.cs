using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chetoslvl1 : MonoBehaviour
{
    public GameManager gameManager;
    public HackingController hackingController;

    private void Update()
    {
        //Completar hacks
        if (Input.GetKey(KeyCode.H))
        {
            for (int i = 0; i < hackingController.lights.ToArray().Length; i++)
            {
                gameManager.GODmodeActivateLights(hackingController.lights[i], hackingController.PClights[i]);
            }
        }
    }
    public void GODmode()
    {
        for (int i = 0; i < hackingController.lights.ToArray().Length; i++)
        {
            hackingController.lights[i].IsActivated = true;
        }
    }
}
