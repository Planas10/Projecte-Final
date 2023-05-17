using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserScript : MonoBehaviour
{
    AI_Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<AI_Enemy>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!enemy.IsDistracted)
        {
            //Debug.Log("Veo al player");
            if (other.gameObject.GetComponent<FPSController>())
            {
                enemy.ChasePlayer();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<BulletDistraction>())
        {
            Debug.LogError("Veo la bala");
            enemy.Bulleted(other.transform.position);
        }
        if (other.gameObject.GetComponent<FPSController>())
        {
            enemy.IsChasingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BulletDistraction>())
        {
            Debug.LogError("Dejo de ver la bala");
            enemy.IsDistracted = false;
        }
        if (other.gameObject.GetComponent<FPSController>())
        {
            enemy.IsChasingPlayer = false;
        }
    }
}
