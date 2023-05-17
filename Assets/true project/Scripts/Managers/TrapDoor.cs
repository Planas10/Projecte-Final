using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public GameObject LTrapDoor;
    public Vector3 LTrapDoorLimit;
    public GameObject RTrapDoor;
    public Vector3 RTrapDoorLimit;

    public float distance = 1.0f;

    private Vector3 LTrapDoorInitPos;
    private Vector3 RTrapDoorInitPos;

    public bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        LTrapDoorInitPos = LTrapDoor.transform.position;
        RTrapDoorInitPos = RTrapDoor.transform.position;
        LTrapDoorLimit = LTrapDoor.transform.position - LTrapDoor.transform.forward * distance;
        RTrapDoorLimit = RTrapDoor.transform.position + RTrapDoor.transform.forward * distance;

        if (isOpened)
        {
            LTrapDoor.transform.position = LTrapDoorLimit;
            RTrapDoor.transform.position = RTrapDoorLimit;
        }
    }
    
    private void OpenTrapDoors()
    {
        if (Vector3.Distance(LTrapDoor.transform.position, LTrapDoorLimit) > 0.01f)
        {
            LTrapDoor.transform.position -= LTrapDoor.transform.forward * 0.8f * Time.deltaTime;
        }
        if (Vector3.Distance(RTrapDoor.transform.position, RTrapDoorLimit) > 0.01f)
        {
            RTrapDoor.transform.position += RTrapDoor.transform.forward * 0.8f * Time.deltaTime;
        }
    }
    private void CloseTrapDoors()
    {
        if (Vector3.Distance(LTrapDoor.transform.position, LTrapDoorInitPos) > 0.01f)
        {
            LTrapDoor.transform.position += LTrapDoor.transform.forward * 0.8f * Time.deltaTime;
        }
        if (Vector3.Distance(RTrapDoor.transform.position, RTrapDoorInitPos) > 0.01f)
        {
            RTrapDoor.transform.position -= RTrapDoor.transform.forward * 0.8f * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (isOpened)
        {
            OpenTrapDoors();
        }else
        {
            CloseTrapDoors();
        }
    }
}
