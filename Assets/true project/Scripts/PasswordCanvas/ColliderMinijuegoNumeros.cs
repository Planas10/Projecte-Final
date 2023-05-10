using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMinijuegoNumeros : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel;

    private bool isInside = false;

    private void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            panel.gameObject.SetActive(true);

        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
        }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
            if (panel.gameObject.activeSelf)
            {
                panel.gameObject.SetActive(false);

            }
        }

    }

}
