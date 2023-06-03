using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSOund : MonoBehaviour
{
 // Reference to the AudioSource component


    public Animator animator;
    private bool Walking = false;
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject

    }

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetKey(KeyCode.W))  
            {
            animator.SetBool("IsWalking", true);

            Debug.Log("walk");
            // Play the sound
            }
   
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("IsWalking", false) ;

        }
    }
}

