using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoSound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Play the sound
            audioSource.Play();
        }   
    }
}   




