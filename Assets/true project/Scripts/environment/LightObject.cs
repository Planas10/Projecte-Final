using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsActivated = false;

    public void ActivateLight(bool state)
    {
        GetComponent<Light>().enabled = state;
        IsActivated = state;
    }
}
