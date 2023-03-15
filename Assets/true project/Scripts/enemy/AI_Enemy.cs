using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    static public List<PatrolPoint> waypoints;
    public GameObject Player;

    public FPSController playerS;

    [SerializeField]
    private int currentPoint = 0;

    public float speed = 5f;

    private NavMeshAgent IA;

    private bool IsChasingPlayer;

    public Animation Anim;

    private Vector3 InitialPos;

    private void Awake()
    {
        

        IA = GetComponent<NavMeshAgent>();

        playerS = FindObjectOfType<FPSController>();

        waypoints = new(FindObjectsOfType<PatrolPoint>());
        waypoints.Sort((a, b) => { return a.name.CompareTo(b.name); });
        transform.position = waypoints[currentPoint].transform.position;

        InitialPos = transform.position;

        currentPoint++;
    }

    void Update()
    {
        Vector3 TargetDir = Player.transform.position - transform.position;

        Physics.Raycast(transform.position, Player.transform.position);
        //float Angle = Vector3.SignedAngle(, Vector3.forward);

        if (!IsChasingPlayer)
        {
            Patrol();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Veo al player");

        if (other.gameObject.tag == "Player")
        {
            switch (playerS.WalkingSound)
            {
                case 5:
                    //Debug.Log("Veo al player bien");
                    ChasePlayer();
                    break;
                case 3:
                    //Debug.Log("Veo al player mal");
                    LookAround();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsChasingPlayer = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Reinicio");
            transform.position = InitialPos;
        }
    }
    private void Patrol() {

        if (Vector3.Distance(transform.position, waypoints[currentPoint].transform.position) < 1)
        {
            if (currentPoint != 0)
            {
                transform.Rotate(0, transform.rotation.y - 90f, 0);
            }
            //Debug.Log(transform.rotation.y);
            currentPoint++;

        }
        if (currentPoint >= waypoints.Count)
        {
            transform.Rotate(0, 0, 0);
            //Debug.Log("Final de trayecto");
            currentPoint = 0;
            IA.SetDestination(waypoints[currentPoint].transform.position);
        }
        transform.LookAt(waypoints[currentPoint].transform.position);
        IA.SetDestination(waypoints[currentPoint].transform.position);
    }

    private void LookAround()
    {
        transform.Rotate(0, (transform.rotation.y + 90f) * Time.deltaTime, 0);
    }

    private void ChasePlayer()
    {
        IsChasingPlayer = true;
        transform.LookAt(Player.transform.position);
        IA.SetDestination(Player.transform.position);
    }
}
