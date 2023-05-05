using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorManager : MonoBehaviour
{
    public GameManager gamemanager;
    public FPSController fpsController;

    public GameObject LTrapDoor1;
    public GameObject LTrapDoorLimit1;
    public GameObject RTrapDoor1;
    public GameObject RTrapDoorLimit1;

    public GameObject LTrapDoor2;
    public GameObject LTrapDoorLimit2;
    public GameObject RTrapDoor2;
    public GameObject RTrapDoorLimit2;

    public GameObject LTrapDoor3;
    public GameObject LTrapDoorLimit3;
    public GameObject RTrapDoor3;
    public GameObject RTrapDoorLimit3;

    public GameObject LTrapDoor4;
    public GameObject LTrapDoorLimit4;
    public GameObject RTrapDoor4;
    public GameObject RTrapDoorLimit4;

    private Vector3 Ldoor1InitPos;
    private Vector3 Rdoor1InitPos;
    private Vector3 Ldoor2InitPos;
    private Vector3 Rdoor2InitPos;
    private Vector3 Ldoor3InitPos;
    private Vector3 Rdoor3InitPos;
    private Vector3 Ldoor4InitPos;
    private Vector3 Rdoor4InitPos;

    private void Awake()
    {
        Ldoor1InitPos = LTrapDoor1.transform.position;
        Ldoor2InitPos = LTrapDoor2.transform.position;
        Ldoor3InitPos = LTrapDoor3.transform.position;
        Ldoor4InitPos = LTrapDoor4.transform.position;
        Rdoor1InitPos = RTrapDoor1.transform.position;
        Rdoor2InitPos = RTrapDoor2.transform.position;
        Rdoor3InitPos = RTrapDoor3.transform.position;
        Rdoor4InitPos = RTrapDoor4.transform.position;

    }

    public void ActivateDoor()
    {
        if (fpsController.TrapPc1Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor1, LTrapDoorLimit1, RTrapDoor1, RTrapDoorLimit1);
        }
        else
        {
            //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA");
            gamemanager.CloseTrapDoors(LTrapDoor1, Ldoor1InitPos, RTrapDoor1, Rdoor1InitPos);
        }
        if (fpsController.TrapPc2Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor2, LTrapDoorLimit2, RTrapDoor2, RTrapDoorLimit2);
        }
        else
        {
            gamemanager.CloseTrapDoors(LTrapDoor2, Ldoor2InitPos, RTrapDoor2, Rdoor2InitPos);
        }
        if (fpsController.TrapPc3Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor3, LTrapDoorLimit3, RTrapDoor3, RTrapDoorLimit3);
        }
        else
        {
            gamemanager.CloseTrapDoors(LTrapDoor3, Ldoor3InitPos, RTrapDoor3, Rdoor3InitPos);
        }
        if (fpsController.TrapPc4Active)
        {
            gamemanager.OpenTrapDoors(LTrapDoor4, LTrapDoorLimit4, RTrapDoor4, RTrapDoorLimit4);
        }
        else
        {
            gamemanager.CloseTrapDoors(LTrapDoor4, Ldoor4InitPos, RTrapDoor4, Rdoor4InitPos);
        }
    }
}
