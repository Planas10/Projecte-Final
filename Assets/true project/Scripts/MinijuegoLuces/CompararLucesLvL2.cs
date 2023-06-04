using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompararLucesLvL2 : MonoBehaviour
{
    static public List<LuzAleatoria> lucesArriba;

    static public List<LuzPcMinijuego> lucesAbajo;

    private bool Luz1Activada = false;
    private bool Luz2Activada = false;
    private bool Luz3Activada = false;
    private bool Luz4Activada = false;

    private bool LucesBien = false;

    public GameObject lasersPuerta;
    public GameObject LPuerta;
    public GameObject RPuerta;

    public CineamticaPuertaLaser cinemachineCamera;


    private void Awake()
    {
        lucesAbajo = new(FindObjectsOfType<LuzPcMinijuego>());
        lucesAbajo.Sort((a, b) => { return a.name.CompareTo(b.name); });

        lucesArriba = new(FindObjectsOfType<LuzAleatoria>());
        lucesArriba.Sort((a, b) => { return a.name.CompareTo(b.name); });
    }

    private void Update()
    {

        if (lucesAbajo[0].gameObject.GetComponent<Light>().color == lucesArriba[0].gameObject.GetComponent<Light>().color)
        {
            Luz1Activada = true;
        }

        if (lucesAbajo[1].gameObject.GetComponent<Light>().color == lucesArriba[1].gameObject.GetComponent<Light>().color)
        {
            Luz2Activada = true;
        }

        if (lucesAbajo[2].gameObject.GetComponent<Light>().color == lucesArriba[2].gameObject.GetComponent<Light>().color)
        {
            Luz3Activada = true;
        }

        if (lucesAbajo[3].gameObject.GetComponent<Light>().color == lucesArriba[3].gameObject.GetComponent<Light>().color)
        {
            Luz4Activada = true;
        }

        if (Luz1Activada && Luz2Activada && Luz3Activada && Luz4Activada)
        {
            StartCoroutine(CineamticaLuces());    

        }

    }

    public IEnumerator CineamticaLuces()
    {
        cinemachineCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(6f);

        lasersPuerta.SetActive(false);
        RPuerta.SetActive(false);
        LPuerta.SetActive(false);

        yield return new WaitForSeconds(2f);

        cinemachineCamera.gameObject.SetActive(false);
    }
}
