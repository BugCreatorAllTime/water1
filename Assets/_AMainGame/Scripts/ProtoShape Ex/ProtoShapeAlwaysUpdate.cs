using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProtoShape2D))]
public class ProtoShapeAlwaysUpdate : MonoBehaviour
{
    private ProtoShape2D protoShape2D;

    public void Awake()
    {
        protoShape2D = GetComponent<ProtoShape2D>();
    }

    public void LateUpdate()
    {
        protoShape2D.UpdateMaterialSettings();
        protoShape2D.UpdateMesh();
    }
}
