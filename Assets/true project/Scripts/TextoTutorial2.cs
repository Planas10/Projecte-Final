using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoTutorial2 : MonoBehaviour
{

    public Text TextTutorial;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextTutorial.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextTutorial.gameObject.SetActive(false);
        }
    }
}
