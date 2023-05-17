using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public AudioSource audioSource;
    private float force = 10f;
    private float lifeTime = 6f;

    public bool CanActivate = false;

    public GameObject DistractionCollider;
    private SphereCollider SC;

    private FPSController PlayerScript;
    public BulletDistraction BulletDistractionS;

    private Rigidbody rb;

    void Awake()
    {
        
        SC = DistractionCollider.GetComponent<SphereCollider>();
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        rb = GetComponent<Rigidbody>();
        Vector3 direction = PlayerScript.boquilla.transform.right;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void Update()
    {
        if (BulletDistractionS.CanDie)
        {
            StartCoroutine(Countdown());
        }
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
            //transform.parent = collision.transform;
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            SC.enabled = true;
            CanActivate = true;
            audioSource.Play();
        }
    }
}