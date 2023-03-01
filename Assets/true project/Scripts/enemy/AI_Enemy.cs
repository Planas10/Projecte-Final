using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject Player;

    [SerializeField]
    private int currentPoint = 0;

    public float speed = 5f;

    public NavMeshAgent IA;

    public Animation Anim;

    private void Awake()
    {
        transform.position = waypoints[0].position;
        currentPoint++;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, waypoints[currentPoint].position) < 1)
        {
            currentPoint++;
            if (currentPoint >= waypoints.Length)
            {
                currentPoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].position, speed * Time.deltaTime);
        
    }

    private void FollowPlayer()
    {
        IA.SetDestination(Player.transform.position);
    }
}
