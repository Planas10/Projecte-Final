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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            enemy.Bulleted(other.transform.position);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.IsChasingPlayer = true;
        }
    }
}
