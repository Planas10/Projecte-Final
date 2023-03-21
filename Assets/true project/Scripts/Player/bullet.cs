using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float lifeTime;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("colisión");
            //transform.parent = collision.transform;
            rb.isKinematic = true;
            SC.enabled = true;
            StartCoroutine(Countdown());
        }
    }
}