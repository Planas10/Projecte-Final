using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float lifeTime;

    private FPSController PlayerScript;

    private Rigidbody rb;

    void Awake()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        rb = GetComponent<Rigidbody>();
        Vector3 direction = PlayerScript.boquilla.transform.right;
        rb.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        Debug.Log("Me voy a destruir");
        yield return new WaitForSeconds(lifeTime);
        if (lifeTime == 0)
        {
            PlayerScript.MaxShoots++;
        }
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("colisión");
            //transform.parent = collision.transform;
            rb.isKinematic = true;
            StartCoroutine(Countdown());
        }
    }
}
