using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fantasma : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;

    public float speed = 1.0f;
    private int currentTarget = 0;
    private Vector3[] targets;

    void Start()
    {
        // Define the targets array
        targets = new Vector3[] { pointA.position, pointB.position, pointC.position, pointD.position };
    }

    void Update()
    {
        // Move the object towards the current target
        transform.position = Vector3.MoveTowards(transform.position, targets[currentTarget], speed * Time.deltaTime);

        // Check if the object has reached the current target
        if (transform.position == targets[currentTarget])
        {
            // Switch to the next target
            currentTarget = (currentTarget + 1) % targets.Length;
        }
    }
}
