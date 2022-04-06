#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    Camera refCamera;

    public void LateUpdate()
    {
        ///
        var savedZ = transform.position.z;

        ///
        var mousePos = Input.mousePosition;
        var worldPoint = refCamera.ScreenToWorldPoint(mousePos);

        ///
        worldPoint.z = savedZ;
        transform.position = worldPoint;
    }
}

#endif