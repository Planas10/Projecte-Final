using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosLine : MonoBehaviour
{
    [SerializeField] private GameObject RayLimit;

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, RayLimit.transform.position);

    }
}
