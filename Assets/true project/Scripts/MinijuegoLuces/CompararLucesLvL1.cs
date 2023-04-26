using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompararLucesLvL1 : MonoBehaviour
{
    static public List<LuzAleatoria> lucesArriba;

    static public List<LuzPcMinijuego> lucesAbajo;

    private bool Luz1Activada = false;
    private bool Luz2Activada = false;
    private bool Luz3Activada = false;
    private bool Luz4Activada = false;

    private bool LucesBien = false;

    public GameObject lasersPuerta;


    private void Awake()
    {
        lucesAbajo = new(FindObjectsOfType<LuzPcMinijuego>());
        lucesAbajo.Sort((a, b) => { return a.name.CompareTo(b.name); });

        lucesArriba = new(FindObjectsOfType<LuzAleatoria>());
        lucesArriba.Sort((a, b) => { return a.name.CompareTo(b.name); });
        for (int i = 0; i < lucesAbajo.ToArray().Length; i++)
        {
            Debug.Log(lucesAbajo[i].gameObject.name); 
        }

        for (int i = 0; i < lucesArriba.ToArray().Length; i++)
        {
            Debug.Log(lucesArriba[i].gameObject.name);
        }
    }

    private void Update()
    {

        if (lucesAbajo[0].gameObject.GetComponent<Light>().color == lucesAbajo[0].gameObject.GetComponent<Light>().color)
        {
            Luz1Activada = true;
        }

        if (lucesAbajo[1].gameObject.GetComponent<Light>().color == lucesAbajo[1].gameObject.GetComponent<Light>().color)
        {
            Luz2Activada = true;
        }

        if (lucesAbajo[2].gameObject.GetComponent<Light>().color == lucesAbajo[2].gameObject.GetComponent<Light>().color)
        {
            Luz3Activada = true;
        }

        if (lucesAbajo[3].gameObject.GetComponent<Light>().color == lucesAbajo[3].gameObject.GetComponent<Light>().color)
        {
            Luz4Activada = true;    
        }

        if (Luz1Activada && Luz2Activada && Luz3Activada && Luz4Activada)
        {
            SceneManager.LoadScene(5);
        }

    }
}
