using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedDirectionTransform : MonoBehaviour
{
    [SerializeField]
    private Vector3 upVector = Vector3.up;

    public void LateUpdate()
    {
        transform.up = upVector;
    }
}
