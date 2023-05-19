using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorEnd : MonoBehaviour
{
    [SerializeField] private AI_Enemy[] enemiesTrap;

    public GameObject LTrapDoor;
    private Vector3 LTrapDoorLimit;
    public GameObject RTrapDoor;
    private Vector3 RTrapDoorLimit;

    private bool allTrapped;

    public float distance = 1.0f;

    private void Awake()
    {
        LTrapDoorLimit = new Vector3(LTrapDoor.transform.position.x + 2f, LTrapDoor.transform.position.y, LTrapDoor.transform.position.z);
        RTrapDoorLimit = new Vector3(RTrapDoor.transform.position.x + 2f, RTrapDoor.transform.position.y, RTrapDoor.transform.position.z);
    }

    private void OpenEndTrapDoor()
    {
        if (allTrapped)
        {
            if (Vector3.Distance(LTrapDoor.transform.position, LTrapDoorLimit) > 0.01f)
            {
                LTrapDoor.transform.position -= LTrapDoor.transform.right * 0.8f * Time.deltaTime;
            }
            if (Vector3.Distance(RTrapDoor.transform.position, RTrapDoorLimit) > 0.01f)
            {
                RTrapDoor.transform.position -= RTrapDoor.transform.right * 0.8f * Time.deltaTime;
            }
        }
    }

    private void CheckEnemiesTrapped() {
        for (int i = 0; i < enemiesTrap.Length; i++)
        {
            if (!enemiesTrap[i].GetComponent<AI_Enemy>().Trapped)
            {
                return;
            }
            else
            {
                allTrapped = true;
            }
        }
    }

    private void Update()
    {
        CheckEnemiesTrapped();
        OpenEndTrapDoor();
    }

}
