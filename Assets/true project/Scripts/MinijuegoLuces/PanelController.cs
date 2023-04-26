using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    Color[] lightColors = { Color.red, Color.green, Color.blue, Color.yellow };

    public int panelIndex; // index of the panel (0 to 3)
    public Light panelLight; // reference to the light component of the panel
    public KeyCode interactionKey = KeyCode.E; // key to trigger the interaction

    private int currentColorIndex = 0; // current index of the color array

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            panelLight.color = lightColors[currentColorIndex];

            currentColorIndex = (currentColorIndex + 1) % lightColors.Length;
        }
    }
}

