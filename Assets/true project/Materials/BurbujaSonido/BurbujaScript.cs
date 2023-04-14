using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaScript : MonoBehaviour
{

    Camera _cam;
    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = _cam.transform.position - transform.position;
    }
}
