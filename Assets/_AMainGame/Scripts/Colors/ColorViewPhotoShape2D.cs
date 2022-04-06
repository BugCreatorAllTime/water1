using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProtoShape2D))]
public class ColorViewPhotoShape2D : ColorView
{
    protected override void SetColor(Color color)
    {
        var protoShape = GetComponent<ProtoShape2D>();
        protoShape.color1 = protoShape.color2 = protoShape.outlineColor = color;
        protoShape.UpdateMaterialSettings();
        protoShape.UpdateMesh();
    }
}
