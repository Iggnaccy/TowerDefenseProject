using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderScript : MonoBehaviour
{
    LineRenderer lr;
    EdgeCollider2D edge;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        edge = GetComponent<EdgeCollider2D>();
        lr.positionCount = edge.pointCount;
        for(int i = 0; i < edge.pointCount; i++)
        {
            lr.SetPosition(i, edge.points[i]);
        }
    }
}
