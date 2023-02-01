using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoP : MonoBehaviour
{
    public AudioClip fuego;
    public AudioSource fuenteAudio;
   
   
   
   
    private void Start() {
        fuenteAudio = GetComponent<AudioSource> ();
    }


    void Update()
    {
    
    }


    private void OnTriggerEnter2D(Collider2D Altar)
    {
        if(Altar.CompareTag("Player")){

                fuenteAudio.clip = fuego;
                fuenteAudio.Play();

        }



    }
}