using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float speed = 10f;
    public float lifeTime;

    private bool CanMove;

    private FPSController PlayerScript;
    
    private Vector3 direction;

    //protected Rigidbody rb;
    void Awake()
    {
        CanMove = true;
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        //rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = transform.position;
        //Fire(transform.forward);
        direction = PlayerScript.boquilla.transform.right;
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        if (CanMove)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    //public void Fire(Vector3 direction)
    //{
    //    rb.AddForce(direction * force, ForceMode.Impulse);
    //    StartCoroutine(Countdown());
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo")
        {
            CanMove = false;
        }
    }

    IEnumerator Countdown()
    {
        Debug.Log("Me voy a destruir");
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }

}
