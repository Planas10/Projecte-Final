using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    static public List<PatrolPoint> waypoints;
    public GameObject Player;

    public FPSController playerS;
    private bullet Bullet;

    public GameObject ChasingLight;
    public GameObject AlertLight;

    [SerializeField] private int currentPoint = 0;

    [SerializeField] private float SearchTime = 4f;

    public float speed = 5f;

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

        waypoints = new(FindObjectsOfType<PatrolPoint>());
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

        Debug.Log(IsDistracted);

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
                        //Debug.Log("Veo al player bien");
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
            IsDistracted = true;
            AlertLight.SetActive(true);
            IA.SetDestination(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsChasingPlayer = false;
        }
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
        if (!LookingForPlayer)
        {
            transform.LookAt(waypoints[currentPoint].transform.position);
            IA.SetDestination(waypoints[currentPoint].transform.position);
        }
    }

    //private void LookAround()
    //{
    //    ChangingSearchTime = SearchTime;
    //    Vector3 playerPos = Player.transform.position;
    //    IA.SetDestination(playerPos);
    //    if (transform.position == playerPos)
    //    {
    //        ChangingSearchTime -= Time.deltaTime;
    //        switch (transform.rotation.y)
    //        {
    //            case >= -45:
    //                transform.Rotate(0, (transform.rotation.y + 5f) * Time.deltaTime, 0);
    //                break;
    //            case <= 45:
    //                transform.Rotate(0, (transform.rotation.y - 5f) * Time.deltaTime, 0);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    private void ChasePlayer()
    {
        if (!IsDistracted)
        {
            IsChasingPlayer = true;
            transform.LookAt(Player.transform.position);
            IA.SetDestination(Player.transform.position);
        }

    }

    //IEnumerator Distracted() {
    //    yield return new WaitForSeconds(DistractionTime);
    //}
}
