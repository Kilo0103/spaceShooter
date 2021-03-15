using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public float radius;
    public Color myColor;

    private void OnDrawGizmos()
    {
        Gizmos.color = myColor;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
