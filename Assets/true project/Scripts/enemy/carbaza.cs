using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carbaza : MonoBehaviour
{
    public float intervalLanzamiento = 10f;
    private float Timer;

    private bool CanThrow = false;

    public Transform Spawner1;
    public Transform Spawner2;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //void Start()
    //{
    //    Vector2 force = new Vector2(1f, 1f).normalized * intervalLanzamiento;
    //    rb.velocity = force;
    //}

    void Update()
    {
        Timer = intervalLanzamiento;
        Timer -= Time.deltaTime;
        Debug.Log(Timer);

        if (intervalLanzamiento >= 0)
        {
            CanThrow = true;
        }

        if (CanThrow)
        {
            rb.AddForce(Vector3.up * intervalLanzamiento, ForceMode.Impulse);
            CanThrow = false;
            Timer = intervalLanzamiento;
        }

        Vector3 force = new Vector3(0, intervalLanzamiento, 0);
        rb.useGravity = true;
        rb.AddForce(force);
    }

}
