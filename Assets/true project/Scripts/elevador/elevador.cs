using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevador : MonoBehaviour
{
    public Transform topPosition; // The position of the top floor
    public Transform bottomPosition; // The position of the bottom floor
    public float speed = 2.0f; // The speed of the elevator
    private bool isMoving = false; // Flag indicating whether the elevator is currently moving
    private bool atTop = false; // Flag indicating whether the elevator is currently at the top position



    void Update()
    {

            // Check if the E key is pressed and the elevator is not moving
            if (Input.GetKeyDown(KeyCode.E) && !isMoving)
            {
                // Start the elevator moving in the direction of the next floor
                if (atTop)
                {
                    StartCoroutine(MoveElevator(bottomPosition));
                }
                else
                {
                    StartCoroutine(MoveElevator(topPosition));
                }
            }

    }

    IEnumerator MoveElevator(Transform targetPosition)
    {
        isMoving = true;
        // Calculate the distance and direction to move
        float distance = Vector3.Distance(transform.position, targetPosition.position);
        Vector3 direction = (targetPosition.position - transform.position).normalized;

        // Move the elevator until it reaches the target position
        while (distance > 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, targetPosition.position);
            yield return null;
        }

        // Update flags and wait for the next input
        isMoving = false;
        atTop = !atTop;
    }

}

