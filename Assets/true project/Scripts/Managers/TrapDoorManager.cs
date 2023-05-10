using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorManager : MonoBehaviour
{

    public GameManager gamemanager;
    public FPSController fpsController;
    public AI_Enemy Enemy1;
    public AI_Enemy2 Enemy2;
    public AI_Enemy3 Enemy3;
    public AI_Enemy4 Enemy4;

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

    [SerializeField]private GameObject LFinalDoor;
    [SerializeField]private GameObject RFinalDoor;

    private Vector3 Ldoor1InitPos;
    private Vector3 Rdoor1InitPos;
    private Vector3 Ldoor2InitPos;
    private Vector3 Rdoor2InitPos;
    private Vector3 Ldoor3InitPos;
    private Vector3 Rdoor3InitPos;
    private Vector3 Ldoor4InitPos;
    private Vector3 Rdoor4InitPos;

    private Vector3 LFinalDoorLimit;
    private Vector3 RFinalDoorLimit;



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

        LFinalDoorLimit = new Vector3(LFinalDoor.transform.position.x + 1.8f, LFinalDoor.transform.position.y, LFinalDoor.transform.position.z);
        RFinalDoorLimit = new Vector3(RFinalDoor.transform.position.x - 1.8f, RFinalDoor.transform.position.y, RFinalDoor.transform.position.z);

    }

    private void Update()
    {
        Debug.Log("Enemy1" + Enemy1.Trapped);
        Debug.Log("Enemy2" + Enemy2.Trapped2);
        Debug.Log("Enemy3" + Enemy3.Trapped3);
        Debug.Log("Enemy4" + Enemy4.Trapped4);
        OpenFinalDoor();
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

    private void OpenFinalDoor() {
        
        if (Enemy1.Trapped && Enemy2.Trapped2 && Enemy3.Trapped3 && Enemy3.Trapped3)
        {
            if (Vector3.Distance(LFinalDoor.transform.position, LFinalDoorLimit) >= 0.1f)
            {
                LFinalDoor.transform.position += LFinalDoor.transform.forward * 0.8f * Time.deltaTime;
                Debug.Log("puerta L se mueve");
            }
            if (Vector3.Distance(RFinalDoor.transform.position, RFinalDoorLimit) >= 0.1f)
            {
                RFinalDoor.transform.position -= RFinalDoor.transform.forward * 0.8f * Time.deltaTime;
                Debug.Log("puerta R se mueve");
            }
        }
    }
}
