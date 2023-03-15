using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        BoxCollider Size = GetComponent<BoxCollider>();
        Gizmos.DrawCube(transform.position, Size.size);
    }
}
