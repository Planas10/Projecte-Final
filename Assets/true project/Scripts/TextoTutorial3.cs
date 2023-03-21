using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoTutorial3 : MonoBehaviour
{

    public Text textTutorial;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textTutorial.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textTutorial.gameObject.SetActive(false);
        }
    }
}
