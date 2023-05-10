using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramTrap : MonoBehaviour
{
    public TrapDoor trapDoor;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<AI_Enemy>() != null)
        {
            other.gameObject.GetComponent<AI_Enemy>().Trapped = !trapDoor.isOpened;
        }
    }

}
