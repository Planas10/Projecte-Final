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
        if (other.gameObject.GetComponent<FPSController>())
        {
            enemy.IsDistracted = false;
            enemy.ChasePlayer();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FPSController>())
        {
            enemy.IsDistracted = false;
            enemy.IsChasingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FPSController>())
        {
            enemy.IsChasingPlayer = false;
        }
    }
}
