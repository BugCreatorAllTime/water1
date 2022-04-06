using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleArranger : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private List<Transform> transforms;

#if UNITY_EDITOR

    [ContextMenu("GetAllChildren")]
    public void GetAllChildren()
    {
        ///
        UnityEditor.Undo.RecordObject(this, "GetAllChildren");

        ///
        transforms = new List<Transform>(GetComponentsInChildren<Transform>());
        transforms.RemoveAt(0);
    }

    [ContextMenu("Arrange")]
    public void Arrange()
    {
        ///
        if (transforms.Count == 0)
        {
            return;
        }

        ///
        UnityEditor.Undo.RecordObjects(transforms.ToArray(), "Arrange");

        ///
        float angularInterval = 360.0f / transforms.Count;
        float currentAngle = 0;
        for (int i = 0; i < transforms.Count; i++)
        {
            var currentAngleInRad = currentAngle * Mathf.Deg2Rad;
            Vector3 pos = transform.position + ((Vector3)new Vector2(Mathf.Cos(currentAngleInRad), Mathf.Sin(currentAngleInRad))) * radius;
            transforms[i].position = pos;
            transforms[i].up = pos - transform.position;

            ///
            currentAngle += angularInterval;
        }
    }
#endif
}
