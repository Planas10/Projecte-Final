using Cinemachine;
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

    public GameObject Player;
    public GameObject camaras;

    public GameObject LPuerta;
    public GameObject LPuertaLimit;
    public GameObject RPuerta;
    public GameObject RPuertaLimit;

    public CinemachineBrain camarasLvl2;

    [SerializeField] private bool CanDoCinematic;


    private void Awake()
    {
        CanDoCinematic = true;
        lucesAbajo = new(FindObjectsOfType<LuzPcMinijuego>());
        lucesAbajo.Sort((a, b) => { return a.name.CompareTo(b.name); });

        lucesArriba = new(FindObjectsOfType<LuzAleatoria>());
        lucesArriba.Sort((a, b) => { return a.name.CompareTo(b.name); });
    }

    private void Update()
    {

        if (lucesAbajo[0].gameObject.GetComponent<Light>().color == lucesArriba[0].gameObject.GetComponent<Light>().color)
            Luz1Activada = true;
        else
            Luz1Activada = false;

        if (lucesAbajo[1].gameObject.GetComponent<Light>().color == lucesArriba[1].gameObject.GetComponent<Light>().color)
            Luz2Activada = true;
        else
            Luz2Activada = false;

        if (lucesAbajo[2].gameObject.GetComponent<Light>().color == lucesArriba[2].gameObject.GetComponent<Light>().color)
            Luz3Activada = true;
        else
            Luz3Activada = false;

        if (lucesAbajo[3].gameObject.GetComponent<Light>().color == lucesArriba[3].gameObject.GetComponent<Light>().color)
            Luz4Activada = true;
        else
            Luz4Activada = false;

        if (Luz1Activada && Luz2Activada && Luz3Activada && Luz4Activada)
        {           
            
            if(CanDoCinematic)
            {
                StartCoroutine(CineamticaLuces());
            }

        }

    }

    private void AbrirPuertaNormal() {
        if (Vector3.Distance(LPuerta.transform.position, LPuertaLimit.transform.position) > 0.5f)
        {
            Debug.Log("HOLA");
            LPuerta.transform.position -= LPuerta.transform.right * 1f * Time.deltaTime;
        }
        if (Vector3.Distance(RPuerta.transform.position, RPuertaLimit.transform.position) > 0.5f)
        {
            RPuerta.transform.position -= RPuerta.transform.right * 1f * Time.deltaTime;
        }
    }

    public IEnumerator CineamticaLuces()
    {
        camarasLvl2.gameObject.SetActive(true);
        Player.gameObject.SetActive(false);
        camaras.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        AbrirPuertaNormal();

        yield return new WaitForSeconds(2f);

        lasersPuerta.SetActive(false);        

        yield return new WaitForSeconds(1.5f);     

        camarasLvl2.gameObject.SetActive(false);
        Player.gameObject.SetActive(true);
        camaras.gameObject.SetActive(false);
        CanDoCinematic = false;
    }
}
