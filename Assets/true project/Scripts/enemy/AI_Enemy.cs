using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
   
    public Transform Objetivo;
    public float Velocidad;
    public NavMeshAgent IA;

    public Animation Anim;

    void Update()
    {
        IA.speed = Velocidad;
        IA.SetDestination(Objetivo.position);
    }

}
