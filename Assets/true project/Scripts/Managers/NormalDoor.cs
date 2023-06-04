using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : MonoBehaviour
{
    public GameObject LTrapDoor;
    public GameObject LTrapDoorLimit;

    public GameObject RTrapDoor;
    public GameObject RTrapDoorLimit;

    private Vector3 LTrapDoorInitPos;
    private Vector3 RTrapDoorInitPos;

    public bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        LTrapDoorInitPos = LTrapDoor.transform.position;
        RTrapDoorInitPos = RTrapDoor.transform.position;

        if (isOpened)
        {
            LTrapDoor.transform.position = LTrapDoorLimit.transform.position;
            RTrapDoor.transform.position = RTrapDoorLimit.transform.position;
        }
    }

    private void OpenTrapDoors()
    {
        //Debug.Log(Vector3.Distance(LTrapDoor.transform.position, LTrapDoorLimit.transform.position) < distance);
        if (Vector3.Distance(LTrapDoor.transform.position, LTrapDoorLimit.transform.position) > 0.5f)
        {
            Debug.Log("HOLA");
            LTrapDoor.transform.position -= LTrapDoor.transform.right * 1f * Time.deltaTime;
        }
        if (Vector3.Distance(RTrapDoor.transform.position, RTrapDoorLimit.transform.position) > 0.5f)
        {
            RTrapDoor.transform.position -= RTrapDoor.transform.right * 1f * Time.deltaTime;
        }
    }
    private void CloseTrapDoors()
    {
        //Debug.Log(Vector3.Distance(RTrapDoor.transform.position, RTrapDoorLimit.transform.position) < 3f/* < distance*/);
        Debug.Log(Vector3.Distance(RTrapDoor.transform.position, RTrapDoorInitPos)/* < distance*/);
        if (Vector3.Distance(LTrapDoor.transform.position, LTrapDoorInitPos) > 0.02f)
        {
            LTrapDoor.transform.position += LTrapDoor.transform.right * 0.8f * Time.deltaTime;
        }
        if (Vector3.Distance(RTrapDoor.transform.position, RTrapDoorInitPos) > 0.02f)
        {
            RTrapDoor.transform.position += RTrapDoor.transform.right * 0.8f * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (isOpened)
        {
            OpenTrapDoors();
        }
        else
        {
            CloseTrapDoors();
        }
    }
}
