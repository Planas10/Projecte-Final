using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float force = 10f;
    private float lifeTime = 6f;

    public GameObject DistractionCollider;
    private SphereCollider SC;

    private FPSController PlayerScript;

    private Rigidbody rb;

    void Awake()
    {
        SC = DistractionCollider.GetComponent<SphereCollider>();
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        rb = GetComponent<Rigidbody>();
        Vector3 direction = PlayerScript.boquilla.transform.right;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    IEnumerator Countdown()
    {
        //Debug.Log("Me voy a destruir");
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("colisi�n");
            //transform.parent = collision.transform;
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            SC.enabled = true;
            StartCoroutine(Countdown());
        }
    }
}