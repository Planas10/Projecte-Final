using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSoundEffect : MonoBehaviour
{

    public AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SONIDO");
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }


}
