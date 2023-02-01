using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    public Transform miCasa;
    public float speed = 4f;
    Rigidbody rig;
    public bool PlayerCerca;
    public NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerCerca == true)
        {
            transform.LookAt(target);
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            rig.MovePosition(pos);
            agent.SetDestination(pos);
            
        }
        else
        {
            Vector3 MiCasa = Vector3.MoveTowards(transform.position, miCasa.position, speed * Time.fixedDeltaTime);
            agent.SetDestination(MiCasa);
        }
    }

    public void playerfollow()

    {
        PlayerCerca = true;
    }

    public void noFollow()
    {
        PlayerCerca= false;
        Vector3 pos = Vector3.MoveTowards(transform.position, miCasa.position, speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision col){
   if (col.gameObject.tag == "Explosion"){
        Destroy(gameObject);
   }
 }



}
