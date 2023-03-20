using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float lifeTime;

    private bool CanMove;

    private FPSController PlayerScript;

    private Rigidbody rb;

    void Awake()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        rb = GetComponent<Rigidbody>();
        CanMove = true;
        Vector3 direction = PlayerScript.boquilla.transform.right;
        rb.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(Countdown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo")
        {
            Debug.Log("colisión");
            CanMove = false;
            transform.parent = collision.transform;
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        Debug.Log("Me voy a destruir");
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }

}
