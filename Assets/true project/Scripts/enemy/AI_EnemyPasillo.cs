using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI_EnemyPasillo : MonoBehaviour
{
    public GameObject jugador;
    public float velocidadEnemigo = 5f;

    private Vector3 initPos;
    private Quaternion initRot;
    private Vector3 raycastD;

    private NavMeshAgent IA;

    public float viewDistance = 10f;
    public float fieldOfView = 90f;
    public float AttackDistance = 5f;

    public LayerMask layerMask;

    private bool persiguiendoPlayer;

    private Animator animator;


    private void Awake()
    {
        raycastD = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * 20f;
        IA = GetComponent<NavMeshAgent>();
        initPos = transform.position;
        initRot = transform.rotation;

        animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.up * 1f, raycastD);
    }

    private void Update()
    {

        //transform.LookAt(jugador.transform.position);

        //Vector3 directionToPlayer = jugador.transform.position;
        //float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        raycastD = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * 20f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 1f, transform.forward, out hit, viewDistance,layerMask))
        {
            Debug.Log("VICTOR");
            
            persiguiendoPlayer = true;
        }

        if(persiguiendoPlayer==true)
        {
            IA.SetDestination(jugador.transform.position);
        }

        if (persiguiendoPlayer)
        {
            IA.SetDestination(jugador.transform.position);
            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);
        }
        //else
        //{
        //    IA.SetDestination(transform.position); // Detiene el enemigo
        //    animator.SetBool("Run", false);
        //    animator.SetBool("Attack", true);
        //}

        float distanceToPlayer = Vector3.Distance(transform.position, jugador.transform.position);

        if (distanceToPlayer <= AttackDistance)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }



    }




private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLISION PASILLO");

            //IA.SetDestination(initPos);
            IA.Warp(initPos);
            IA.ResetPath();
            
            persiguiendoPlayer = false;
            transform.position = initPos;
            transform.rotation = initRot;
        }
    }



}

