using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNormalDoors : MonoBehaviour
{
    public HackingController hackingController;

    public GameObject LDoor;
    public GameObject LDoorLimit;

    public GameObject RDoor;
    public GameObject RDoorLimit;

    public GameObject LDoor2;
    public GameObject LDoorLimit2;

    public GameObject RDoor2;
    public GameObject RDoorLimit2;

    private void Update()
    {
        if(hackingController.CanOpen1)
        {
            if (Vector3.Distance(LDoor.transform.position, LDoorLimit.transform.position) > 0.5f)
            {
                Debug.Log("HOLA");
                LDoor.transform.position -= LDoor.transform.right * 1f * Time.deltaTime;
            }
            if (Vector3.Distance(RDoor.transform.position, RDoorLimit.transform.position) > 0.5f)
            {
                RDoor.transform.position -= RDoor.transform.right * 1f * Time.deltaTime;
            }
        }
        if(hackingController.CanOpen2)
        {
            if (Vector3.Distance(LDoor2.transform.position, LDoorLimit2.transform.position) > 0.5f)
            {
                Debug.Log("HOLA");
                LDoor2.transform.position -= LDoor2.transform.right * 1f * Time.deltaTime;
            }
            if (Vector3.Distance(RDoor2.transform.position, RDoorLimit2.transform.position) > 0.5f)
            {
                RDoor2.transform.position -= RDoor2.transform.right * 1f * Time.deltaTime;
            }
        }
    }
}
