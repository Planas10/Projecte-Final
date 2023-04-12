using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_EnemyRotator : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;

    private FPSController PlayerS;
    private GameManager GameManager;
    private bullet BulletS;

    public GameObject ChasingLight;
    public GameObject AlertLight;

    private NavMeshAgent IA;

    private float rotationSpeed;
    private float movmentSpeed;

    private bool IsChasingPlayer;
    private bool IsDistracted;

    private void Awake()
    {
        IA = GetComponent<NavMeshAgent>();

        PlayerS = FindObjectOfType<FPSController>();
        GameManager = FindObjectOfType<GameManager>();
        BulletS = FindObjectOfType<bullet>();
    }

    private void Update()
    {
        if (!IsDistracted)
        {
            LookAround();
        }
    }

    private void LookAround() {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
