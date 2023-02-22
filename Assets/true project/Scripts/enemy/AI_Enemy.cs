using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public GameObject Player;

    public NavMeshAgent IA;

    public float Velocidad;

    public Animation Anim;

    void Update()
    {
        IA.speed = Velocidad;
    }

    private void Patrol(GameObject destination) {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            destination = patrolPoints[i].gameObject;
            IA.SetDestination(destination.transform.position);
            if (i > patrolPoints.Length){ i = 0; }        
        }
    }

    private void FollowPlayer() {
        IA.SetDestination(Player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PatrolPoint")
        {
            Patrol
        }
    }

}
