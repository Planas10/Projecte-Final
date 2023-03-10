using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float force = 3f;
    public float lifeTime;

    private FPSController PlayerScript;

    protected Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.position;
        Fire(Vector3.forward);
    }

    public virtual void Fire(Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        Debug.Log("Me voy a destruir");
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }

}
