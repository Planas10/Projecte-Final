using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    static public List<PatrolPoint> waypoints;
    public GameObject Player;

    [SerializeField]
    private int currentPoint = 0;

    public float speed = 5f;

    private bool IsChasingPlayer = false;

    public NavMeshAgent IA;

    public AudioClip soundEffect;
    private AudioSource audioSource;

    public Animation Anim;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        waypoints = new(FindObjectsOfType<PatrolPoint>());
        waypoints.Sort((a, b) => { return a.name.CompareTo(b.name); });
        transform.position = waypoints[currentPoint].transform.position;
        currentPoint++;
    }

    void Update()
    {
        if (!IsChasingPlayer)
        {
            //Debug.Log("Patrullando");
            if (Vector3.Distance(transform.position, waypoints[currentPoint].transform.position) < 1)
            {
                currentPoint++;
                if (currentPoint >= waypoints.Count)
                {
                    currentPoint = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].transform.position, speed * Time.deltaTime);
        }
        else
        {
            //Debug.Log("Siguiendo al player");
            FollowPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!IsChasingPlayer) {
                //Debug.Log("Player detectado");
                IsChasingPlayer = true;
            }
            else if (IsChasingPlayer)
            {
                IsChasingPlayer = false;
            }
        }
    }

    private void FollowPlayer()
    {
        IA.SetDestination(Player.transform.position);
    }
}
