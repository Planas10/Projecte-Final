using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField] private PatrolPoint[] waypoints;
    private GameObject player;
    private FPSController playerS;
    private bullet Bullet;

    public GameObject ChasingLight;
    public GameObject AlertLight;

    [SerializeField] private int currentPoint = 0;

    [SerializeField] private float SearchTime = 4f;

    private float normalSpeed = 5f;
    private float alertSpeed = 6.5f;
    private float chaseSpeed = 8f;

    private NavMeshAgent IA;

    public Vector3 InitialPos;
    public bool playerChased = false;

    public bool IsChasingPlayer;
    private bool LookingForPlayer;
    public bool IsDistracted;

    public bool Trapped = false;
    bullet bulletDetected = null;
    private void Awake()
    {
        IA = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<FPSController>().gameObject;
        playerS = player.GetComponent<FPSController>();

        /*
        waypoints = new(FindObjectsOfType<PatrolPoint1>());
        waypoints.Sort((a, b) => { return a.name.CompareTo(b.name); });
        transform.position = waypoints[currentPoint].transform.position;
        */
        InitialPos = gameObject.transform.position;

        currentPoint++;
    }
     
    void Update()
    {
        //if (gameObject.name == "EnemyChaser1")
        //{
        //    Debug.LogError(gameObject.name + "Trapped: " + Trapped);
        //}
        if(IsDistracted && bulletDetected == null){
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

        Vector3 TargetDir = player.transform.position - transform.position;

        Physics.Raycast(transform.position, player.transform.position);
        //float Angle = Vector3.SignedAngle(, Vector3.forward);

        if (!IsChasingPlayer && !IsDistracted && !Trapped)
        {
            IA.isStopped = false;
            Patrol();
        }
        else if(Trapped)
        {
            IA.isStopped = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FPSController>())
        {
            playerChased = true;
            transform.position = InitialPos;
            IsChasingPlayer = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<BulletDistraction>() && other.gameObject.GetComponent<bullet>().CanActivate)
        {
            if (IsChasingPlayer)
            {
                return;
            }
            bulletDetected = other.gameObject.GetComponent<bullet>();
            Bulleted(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<bullet>())
        {
            AlertLight.SetActive(false);
            IsDistracted = false;
        }
    }
    private void Patrol() {
        if(waypoints.Length == 0) {
            Debug.LogError("No has ficat el patrolpoints a un enemic: " + this.gameObject.name);
            return; 
        }

        IA.speed = normalSpeed;
        //RaycastHit hit;
        //Vector3 InitPos = new Vector3(transform.position.x, 1f, transform.position.z);
        //Vector3 FinalPos = new Vector3(transform.position.x, 1f, 20f);
        //if (Physics.Linecast(InitPos, Player.transform.position, out hit))
        //{
        //    if (hit.collider.gameObject.CompareTag("Player") && Vector3.Distance(Player.transform.position, InitPos) <= 10f)
        //    {
        //        //Miro si se encuentra en mi angulo de visión
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
        if (currentPoint >= waypoints.Length)
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
    public void ChasePlayer()
    {
        if (!IsDistracted)
        {
            IA.speed = chaseSpeed;
            IsChasingPlayer = true;
            transform.LookAt(player.transform.position);
            IA.SetDestination(player.transform.position);
        }
    }
    public void Bulleted(Vector3 position)
    {
        IA.speed = alertSpeed;
        IsDistracted = true;
        AlertLight.SetActive(true);
        IA.SetDestination(position);
    }

}
