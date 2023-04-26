using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public Animator animator;
    private bool reloading = false;
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("IsReloading", true);

            // Play the sound
            audioSource.Play();
        }



        if(Input.GetKeyUp(KeyCode.R)) 
        {
            animator.SetBool("IsReloading", false);

        }
    }
}






