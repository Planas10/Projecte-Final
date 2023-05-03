using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorManager : MonoBehaviour
{
    public GameManager gamemanager;
    public FPSController fpsController;

    public GameObject LTrapDoor1;
    public GameObject RTrapDoor1;

    public GameObject LTrapDoor2;
    public GameObject RTrapDoor2;
    
    public GameObject LTrapDoor3;
    public GameObject RTrapDoor3;
    
    public GameObject LTrapDoor4;
    public GameObject RTrapDoor4;


    public void ActivateDoor()
    {
        if (fpsController.TrapPc1Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor1, RTrapDoor1);
        }
        //else
        //{
        //    gamemanager.CloseTrapDoors(LTrapDoor1, RTrapDoor1);
        //}
        if (fpsController.TrapPc2Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor2, RTrapDoor2);
        }
        //else
        //{
        //    gamemanager.CloseTrapDoors(LTrapDoor2, RTrapDoor2);
        //}
        if (fpsController.TrapPc3Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor3, RTrapDoor3);
        }
        //else
        //{
        //    gamemanager.CloseTrapDoors(LTrapDoor3, RTrapDoor3);
        //}
        if (fpsController.TrapPc4Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor4, RTrapDoor4);
        }
        //else
        //{
        //    gamemanager.CloseTrapDoors(LTrapDoor4, RTrapDoor4);
        //}
    }
}
