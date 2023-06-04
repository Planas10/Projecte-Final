using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chetoslvl1 : MonoBehaviour
{
    private GameManager gameManager;
    private HackingController hackingController;

    private void Update()
    {
        //Completar hacks
        if (Input.GetKey(KeyCode.H))
        {
            gameManager.GODmode();
            for (int i = 0; i < hackingController.lights.ToArray().Length; i++)
            {
                gameManager.GODmodeActivateLights(hackingController.lights[i], hackingController.PClights[i]);
            }
        }
    }
}
