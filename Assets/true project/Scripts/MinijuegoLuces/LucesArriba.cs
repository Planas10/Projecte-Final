using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucesArriba : MonoBehaviour
{
    public Light lightToChange; // referencia a la luz a la que se le va a cambiar el color
    public Color[] colorOptions; // opciones de color para elegir al azar

    // Start se llama al inicio del juego
    void Start()
    {
        // elegir un color al azar de entre las opciones
        int randomIndex = Random.Range(0, colorOptions.Length);
        Color randomColor = colorOptions[randomIndex];

        // cambiar el color de la luz
        lightToChange.color = randomColor;
    }
}

