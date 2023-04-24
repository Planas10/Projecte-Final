using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class invisibilityManager : MonoBehaviour
{
    public Material newMaterial;
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _renderer.material = newMaterial;
        }
    }
}

