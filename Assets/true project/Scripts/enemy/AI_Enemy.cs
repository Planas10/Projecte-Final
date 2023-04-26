using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    static public List<PatrolPoint1> waypoints;
    public GameObject Player;

    public FPSController playerS;
    private bullet Bullet;

    public GameObject ChasingLight;
    public GameObject AlertLight;

    [SerializeField] private int currentPoint = 0;

    [SerializeField] private float SearchTime = 4f;

    private float normalSpeed = 5f;
    private float alertSpeed = 6.5f;
    private float chaseSpeed = 8f;

    private NavMeshAgent IA;

    public Animation Anim;

    private Vector3 InitialPos;

    private bool IsChasingPlayer;
    private bool LookingForPlayer;
    private bool IsDistracted;

    private void Awake()
    {
        IA = GetComponent<NavMeshAgent>();

        playerS = FindObjectOfType<FPSController>();

        waypoints = new(FindObjectsOfType<PatrolPoint1>());
        waypoints.Sort((a, b) => { return a.name.CompareTo(b.name); });
        transform.position = waypoints[currentPoint].transform.position;

        InitialPos = transform.position;

        currentPoint++;
    }
     
    void Update()
    {
        Bullet = FindObjectOfType<bullet>();
        if (Bullet == null)
        {
            AlertLight.SetActive(false);
            IsDistracted = false;
        }
        if (IsChasingPlayer)
        {
            ChasingLight.SetActive(true);
        }
        else
        {
            ChasingLight.SetActive(false);
        }

        //Debug.Log(IsDistracted);

        Vector3 TargetDir = Player.transform.position - transform.position;

        Physics.Raycast(transform.position, Player.transform.position);
        //float Angle = Vector3.SignedAngle(, Vector3.forward);

        if (!IsChasingPlayer && !IsDistracted)
        {
            Patrol();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsDistracted)
        {
            //Debug.Log("Veo al player");
            if (other.gameObject.tag == "Player")
            {
                switch (playerS.WalkingSound)
                {
                    case 5:
                        ChasePlayer();
                        break;
                    //case 3:
                    //    //Debug.Log("Veo al player mal");
                    //    LookAround();
                    //    break;
                    default:
                        break;
                }
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Bullet"))
    //    {
    //        IsDistracted = false;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            IA.speed = alertSpeed;
            IsDistracted = true;
            AlertLight.SetActive(true);
            IA.SetDestination(other.transform.position);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            IsChasingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            IsDistracted = false;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            IsChasingPlayer = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = InitialPos;
            IsChasingPlayer = false;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 InitPos = new Vector3(transform.position.x, 1f, transform.position.z);
    //    Vector3 FinalPos = new Vector3(transform.position.x, 1f, 20f);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(InitPos, FinalPos);
    //}

    private void Patrol() {
        IA.speed = normalSpeed;
        //RaycastHit hit;
        //Vector3 InitPos = new Vector3(transform.position.x, 1f, transform.position.z);
        //Vector3 FinalPos = new Vector3(transform.position.x, 1f, 20f);
        //if (Physics.Linecast(InitPos, Player.transform.position, out hit))
        //{
        //    if (hit.collider.gameObject.CompareTag("Player") && Vector3.Distance(Player.transform.position, InitPos) <= 10f)
        //    {
        //        //Miro si se encuentra en mi angulo de visi�n
        //        Vector3 vectorPlayerSelf = Player.transform.position - transform.position;
        //        vectorPlayerSelf.Normalize();
        //        if (Vector3.Angle(FinalPos, vectorPlayerSelf) <= 45f) {
        //            Debug.Log("veo al player");
        //        }
        //    }
        //}

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
        if (!LookingForPlayer)
        {
            transform.LookAt(waypoints[currentPoint].transform.position);
            IA.SetDestination(waypoints[currentPoint].transform.position);
        }
    }

private void ChasePlayer()
    {
        if (!IsDistracted)
        {
            IA.speed = chaseSpeed;
            IsChasingPlayer = true;
            transform.LookAt(Player.transform.position);
            IA.SetDestination(Player.transform.position);
        }
    }
}
