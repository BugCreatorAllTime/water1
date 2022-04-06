using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField]
    float additionalSpace = 1f;
    [SerializeField]
    float thickness = 1.0f;

    [Space]
    [SerializeField]
    Transform topWall;
    [SerializeField]
    Transform bottomWall;
    [SerializeField]
    Transform leftWall;
    [SerializeField]
    Transform rightWall;

    public void Awake()
    {
        Entry.Instance.screenInfo.DoAfterCalculation(PlaceWalls);
    }

    private void PlaceWalls()
    {
        var screenInfo = Entry.Instance.screenInfo;

        ///
        float wallWidth = screenInfo.WorldWidth + additionalSpace * 2;
        float wallHeight = screenInfo.WorldHeight + additionalSpace * 2;

        ///
        var halfThickness = thickness / 2.0f;

        // Top
        topWall.localScale = new Vector3(wallWidth, thickness, 1);
        topWall.position = new Vector3(0, screenInfo.PlayFieldMaxY + halfThickness + additionalSpace, 0);

        // Bottom
        bottomWall.localScale = new Vector3(wallWidth, thickness, 1);
        bottomWall.position = new Vector3(0, screenInfo.PlayFieldMinY - halfThickness - additionalSpace, 0);

        // Left
        leftWall.localScale = new Vector3(thickness, wallHeight, 1);
        leftWall.position = new Vector3(-screenInfo.WorldHalfWidth - halfThickness - additionalSpace, 0, 0);

        // Right
        rightWall.localScale = new Vector3(thickness, wallHeight, 1);
        rightWall.position = new Vector3(screenInfo.WorldHalfWidth + halfThickness + additionalSpace, 0, 0);
    }
}
