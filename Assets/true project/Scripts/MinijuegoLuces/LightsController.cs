using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{

    Color[] lightColors = { Color.red, Color.green, Color.blue, Color.yellow };

    public int panelIndex; // index of the panel (0 to 3)
    public Light luz1; // reference to the light component of the panel
    public Light luz2;
    public Light luz3;
    public Light luz4;


    private int currentColorIndex = 0; // current index of the color array

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PCluz1"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // change the color of the panel light
                luz1.color = lightColors[currentColorIndex];

                // increment the current color index, wrapping around if necessary
                currentColorIndex = (currentColorIndex + 1) % lightColors.Length;
            }
        }

        if (other.gameObject.CompareTag("PCluz2"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // change the color of the panel light
                luz2.color = lightColors[currentColorIndex];

                // increment the current color index, wrapping around if necessary
                currentColorIndex = (currentColorIndex + 1) % lightColors.Length;
            }
        }

        if (other.gameObject.CompareTag("PCluz3"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // change the color of the panel light
                luz3.color = lightColors[currentColorIndex];

                // increment the current color index, wrapping around if necessary
                currentColorIndex = (currentColorIndex + 1) % lightColors.Length;
            }
        }

        if (other.gameObject.CompareTag("PCluz4"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // change the color of the panel light
                luz4.color = lightColors[currentColorIndex];

                // increment the current color index, wrapping around if necessary
                currentColorIndex = (currentColorIndex + 1) % lightColors.Length;
            }
        }
    }
}
