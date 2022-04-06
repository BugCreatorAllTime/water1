using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorViewMesh : ColorView
{
    protected override void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
