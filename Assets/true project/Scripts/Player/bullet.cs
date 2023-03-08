using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float force = 4f;
    public float lifeTime = 1000f;

    private FPSController PlayerScript;

    protected Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.position;
        force = 100 * Random.Range(1.3f, 1.7f);
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
        PlayerScript.CurrentShoots--;
        Destroy(gameObject);

    }

}
